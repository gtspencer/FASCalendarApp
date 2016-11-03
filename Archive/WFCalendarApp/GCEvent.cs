using System;
using Google.Apis.Calendar.v3.Data;

namespace WFCalendarApp {

    /// <summary>
    /// Wrapper class around Google's Event class. Makes a lot of code much
    /// simpler due to the Event class not having very useful fields.
    /// </summary>
    public class GCEvent {

        private Event e;
        private DateTime start;
        private DateTime end;
        private double duration;
        private bool isAllDay;
        private string summary;
        private string link;
        private bool isPrivate;

        public DateTime Start {
            get {
                return start;
            }
        }

        public DateTime End {
            get {
                return end;
            }
        }

        public double Duration {
            get {
                return duration;
            }
        }

        public bool IsAllDay {
            get {
                return isAllDay;
            }
        }

        public string Summary {
            get {
                return summary;
            }
        }

        public string HtmlLink {
            get {
                return link;
            }
        }

        public bool IsPrivate {
            get {
                return isPrivate;
            }
        }

        /// <summary>
        /// Constructs a GCEvent and determines all fields from the given Event.
        /// </summary>
        /// <param name="e">The Event</param>
        public GCEvent(Event e) {
            this.e = e;
            start = e.Start.DateTime ?? DateTime.Parse(e.Start.Date);
            end = e.End.DateTime ?? DateTime.Parse(e.End.Date);
            duration = (end - start).TotalHours;
            isAllDay = e.Start.DateTime == null && e.End.DateTime == null;
            summary = e.Summary ?? "REDACTED"; // private event
            link = e.HtmlLink;
            isPrivate = e.Visibility == "private";
        }
    }
}
