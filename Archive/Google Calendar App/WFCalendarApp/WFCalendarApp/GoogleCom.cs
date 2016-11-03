using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Linq;
using System.Threading;
using Google.Apis.Admin.Directory.directory_v1;
using Google.Apis.Admin.Directory.directory_v1.Data;

namespace WFCalendarApp {
    static class GoogleComm {
        const string ApplicationName = "Google Calendar API .NET Quickstart";
        const string TEMPLATE_PATH = "O:\\Richard Bethune\\Calendar\\CalendarTemplate.xltx";
        const string FILE_NAME = "Calendar Spreadsheet";
        const int EXCEL_OFFSET = 3;

        static string[] Scopes = { CalendarService.Scope.CalendarReadonly, DirectoryService.Scope.AdminDirectoryUserReadonly };

        public static List<CalendarEvent> RetrieveData() {
            UserCredential credential;

            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream("GoogleCalendarApp.client_secret.json")) {
                string credPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/calendar-dotnet-quickstart.json", ".credentials/admin-directory_v1-dotnet-quickstart.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }

            // Create Google Calendar API service.
            var service = new CalendarService(new BaseClientService.Initializer() {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            var UserService = new DirectoryService(new BaseClientService.Initializer() {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            //parameters of information obtained
            UsersResource.ListRequest question = UserService.Users.List();
            question.Domain = "fas-atlanta.com";
            question.MaxResults = 100;
            question.OrderBy = UsersResource.ListRequest.OrderByEnum.FamilyName;
            question.ViewType = UsersResource.ListRequest.ViewTypeEnum.DomainPublic;

            DateTime tomorrow = DateTime.Today.AddDays(1.0);

            List<CalendarEvent> AllData = new List<CalendarEvent>();

            //Accessing google calender information
            IList<User> users = question.Execute().UsersValue;

            if (users != null && users.Count > 0) {
                Console.WriteLine("Retrieving data from Google Calendar...");

                foreach (var userItem in users) {
                    //Parameters
                    EventsResource.ListRequest request = service.Events.List(userItem.PrimaryEmail);
                    request.TimeMin = DateTime.Today;
                    request.TimeMax = tomorrow;
                    request.ShowDeleted = false;
                    request.SingleEvents = true;
                    request.MaxResults = 1000;
                    request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

                    //Adding information to Calendar
                    Events events = request.Execute();
                    if (events.Items != null && events.Items.Count > 0) {
                        foreach (var eventItem in events.Items) {
                            string when = eventItem.Start.DateTime.ToString();

                            if (String.IsNullOrEmpty(when)) {
                                when = eventItem.Start.Date;
                            }

                            AllData.Add(new CalendarEvent(userItem.PrimaryEmail, userItem.Name.FullName, eventItem.Summary, when));
                        }
                    } else {
                        AllData.Add(new CalendarEvent(userItem.PrimaryEmail, userItem.Name.FullName, "No Events:", DateTime.Today.ToString()));
                    }
                }
            }

            return AllData;
        }
    }
}
