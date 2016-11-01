using System;
using System.Collections.Generic;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.Calendar;
using System.IO;
using System.Reflection;
using System.Net.Http;
using System.Threading;
using System.Windows.Forms;

using Google.Apis.Admin.Directory.directory_v1;
using Google.Apis.Admin.Directory.directory_v1.Data;

namespace WFCalendarApp {

    /// <summary>
    /// This class connects to Google and retrives the information from Google Calendar.
    /// </summary>
    class GoogleComm {

        const string APPLICATION_NAME = "WFCalendar";
        static string[] Scopes = { CalendarService.Scope.CalendarReadonly, DirectoryService.Scope.AdminDirectoryUserReadonly };
        public static DirectoryService userService = new DirectoryService();
        public static CalendarService service = new CalendarService();

        /// <summary>
        /// Gets the data from Google Calendar within the given range of dates.
        /// </summary>
        /// <param name="start">The starting date</param>
        /// <param name="end">The ending date</param>
        /// <returns>A dictionary mapping employees to all their events occuring
        ///     within the range</returns>
        public static Dictionary<Employee, IList<GCEvent>> RetrieveData(DateTime start, DateTime end) {
            UserCredential credential;

            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream("WFCalendarApp.client_secret.json")) {
                string credPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/calendar-dotnet-quickstart.json", ".credentials/admin-directory_v1-dotnet-quickstart.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }

            // Create Google Calendar API services
            service = new CalendarService(new BaseClientService.Initializer() {
                HttpClientInitializer = credential,
                ApplicationName = APPLICATION_NAME,
            });

            userService = new DirectoryService(new BaseClientService.Initializer() {
                HttpClientInitializer = credential,
                ApplicationName = APPLICATION_NAME,
            });
            
            // Get users
            var question = userService.Users.List();
            question.Domain = "fas-atlanta.com";
            question.MaxResults = 100;
            question.OrderBy = UsersResource.ListRequest.OrderByEnum.GivenName;
            question.ViewType = UsersResource.ListRequest.ViewTypeEnum.DomainPublic;

            var data = new Dictionary<Employee, IList<GCEvent>>();

            // Get events for each user
            try {
                IList<User> users = question.Execute().UsersValue;

                if (users == null) {
                    return data;
                }

                foreach (var userItem in users) {
                    var request = service.Events.List(userItem.PrimaryEmail);
                    request.TimeMin = start;
                    request.TimeMax = end;
                    request.ShowDeleted = false;
                    request.SingleEvents = true;
                    request.MaxResults = 1000;
                    request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

                    var employee = new Employee(userItem.Name.FullName, userItem.PrimaryEmail);
                    var googleEvents = request.Execute().Items;
                    var events = new List<GCEvent>(googleEvents.Count);

                    foreach (Event e in googleEvents) {
                        events.Add(new GCEvent(e));
                    }

                    Employees.List.Add(employee);
                    data.Add(employee, events);
                }
            } catch (HttpRequestException e) {
                throw e;
            }

            return data;
        }

        // To add new event to google calendar
        public static bool CreateNewEvent(CalendarService service, string title, string location, string description,
        DateTime startdate, DateTime enddate)
        {
            try
            {
                Event eventdata = new Event();
                eventdata.Summary = title;
                eventdata.Location = location;
                eventdata.Description = description;

                EventDateTime start = new EventDateTime();
                start.DateTime = startdate;
                eventdata.Start = start;
                EventDateTime end = new EventDateTime();
                end.DateTime = enddate;
                eventdata.End = end;


                var insertevent = service.Events.Insert(eventdata, "primary");
                Event createdevent = insertevent.Execute();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong...");
                //CustomFunctionToLogException(ex);
                return false;
            }
        }
    }
}
