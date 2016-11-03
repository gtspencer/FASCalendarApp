using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace WFCalendarApp {

    /// <summary>
    /// This class provides utilities for processing event data. This includes
    /// methods that prepare data for the spreadsheet and chart.
    /// </summary>
    static class EventUtils {

        private const string OOO_FLAG = "OOO";
        private const string SEARCHING_FLAG = "searching";

        private static readonly TimeSpan WORK_START = new TimeSpan(8, 0, 0);
        private static readonly TimeSpan WORK_END = new TimeSpan(17, 0, 0);

        private static readonly Color OFFICE_COLOR = Color.FromArgb(19, 143, 17);
        private static readonly Color OOO_COLOR = Color.FromArgb(65, 145, 240);
        private static readonly Color NO_EVENTS_COLOR = Color.FromArgb(209, 38, 33);
        private static readonly Color SEARCHING_COLOR = Color.FromArgb(250, 191, 143);
        private static readonly Color NON_WORK_COLOR = Color.FromArgb(230, 230, 230);

        /// <summary>
        /// Creates a mapping from each day to the total number of hours of
        /// events that are happening on that day. Used to determine if someone
        /// has no events on a certain day.
        /// </summary>
        /// <param name="events">The list of events (will be associated with a
        ///     particular employee</param>
        /// <param name="start">The start date selected by the user</param>
        /// <param name="end">The end date selected by the user</param>
        /// <returns>A dictionary mapping from a day to the total hours of
        ///     events on that day</returns>
        public static Dictionary<DateTime, double> GetHoursPerDay(IList<GCEvent> events, DateTime start, DateTime end) {
            var dict = new Dictionary<DateTime, double>();

            foreach (var e in events) {
                var eventStart = e.Start;
                var eventEnd = e.End;
                var eventStartDate = eventStart.Date;
                var eventEndDate = eventEnd.Date;

                var lo = eventStartDate >= start 
                    ? eventStartDate
                    : start;

                var hi = eventEndDate <= end
                    ? eventEndDate
                    : end;

                for (DateTime d = lo; d <= hi; d = d.AddDays(1.0)) {
                    if (!dict.ContainsKey(d)) {
                        dict.Add(d, 0);
                    }

                    var startToday = eventStart > d
                        ? eventStart
                        : d;

                    var endToday = eventEnd < d.AddDays(1.0)
                        ? eventEnd
                        : d.AddDays(1.0);

                    dict[d] += (endToday - startToday).TotalHours;
                }
            }

            return dict;
        }

        /// <summary>
        /// Takes the original mapping from employees to a list of their events
        /// and converts it into an ordered mapping from employees to a list of
        /// their <code>TimePeriods</code>, e.g. 4 hours in office, then 2.5
        /// hours OOO, then 3 days vacation, etc. The key difference between
        /// this and the list of events is that it covers the entire time from
        /// start to end continuously, accounting for overlapping events and
        /// time without events. We used a <code>LinkedList</code> instead of a
        /// <code>Dictionary</code> because we know we will only add to the end,
        /// and there is no reason to take up the extra time and space of
        /// keeping a <code>Dictionary</code> and a <code>List</code>. 
        /// </summary>
        /// <param name="data">The original data from Google</param>
        /// <param name="start">The start date selected by the user</param>
        /// <param name="end">The end date selected by the user</param>
        /// <returns>A mapping from </returns>
        public static Dictionary<Employee, List<TimePeriod>> TimePeriodDict(
                Dictionary<Employee, IList<GCEvent>> data, DateTime start,
                DateTime end) {
            var result = new Dictionary<Employee, List<TimePeriod>>(data.Count);

            foreach (var pair in data) {
                var employee = pair.Key;
                var events = pair.Value;

                result.Add(employee, GetTimePeriods(events, start, end));
            }

            return result;
        }
        /// <summary>
        /// Takes the original mapping from employees to a list of their events
        /// and converts it into an ordered mapping from employees to a list of
        /// their <code>TimePeriods</code>, e.g. 4 hours in office, then 2.5
        /// hours OOO, then 3 days vacation, etc. 
        /// While converting to this format we filter out all events that do not
        /// contain the desired job codes
        /// <param name="data">The original data from Google</param>
        /// <param name="start">The start date selected by the user</param>
        /// <param name="end">The end date selected by the user</param>
        /// <returns>A mapping from </returns>
        public static Dictionary<Employee, List<TimePeriod>> JobFilteredDict(
                Dictionary<Employee, IList<GCEvent>> data, DateTime start,
                DateTime end, String JobFilter)
        {
            var result = new Dictionary<Employee, List<TimePeriod>>(data.Count);

            foreach (var pair in data)
            {
                var employee = pair.Key;
                IList<GCEvent> events = new List<GCEvent>(pair.Value.Count);
                foreach (GCEvent evnt in pair.Value)
                {
                    if (evnt.Summary.Contains(JobFilter)){
                        events.Add(evnt);
                        }
                }
                if (events.Count > 0){
                    result.Add(employee, GetTimePeriods(events, start, end));
                }
            }

            return result;
        }

        /// <summary>
        /// Given a list of events and a starting and ending time, this method
        /// creates a list of <code>TimePeriods</code> going from start to end.
        /// <see cref="TimePeriodDict"/>
        /// </summary>
        /// <param name="events">The list of events</param>
        /// <param name="start">The start date selected by the user</param>
        /// <param name="end">The end date selected by the user</param>
        /// <returns>A list of <code>TimePeriods</code></returns>
        public static List<TimePeriod> GetTimePeriods(IList<GCEvent> events,
                DateTime start, DateTime end) {
            var list = new List<TimePeriod>();

            DateTime firstGapEnd = events.Count == 0
                    ? end
                    : events[0].Start;

            WriteGap(list, start, firstGapEnd);

            var timeOffset = start;
            for (var i = 0; i < events.Count; i++) {
                var e = events[i];
                var eventStart = e.Start;
                var eventEnd = e.End;

                var lo = eventStart >= timeOffset
                    ? eventStart
                    : timeOffset;

                var hi = eventEnd <= end
                    ? eventEnd
                    : end;

                if (hi >= timeOffset) {
                    if (i > 0 && lo > timeOffset) {
                        WriteGap(list, timeOffset, eventStart);
                    }

                    TimePeriod t;
                    t.Duration = (hi - lo);
                    t.Type = DecideType(e);
                    list.Add(t);

                    timeOffset = hi;
                }
            }

            if (events.Count > 0 && timeOffset < end) {
                WriteGap(list, timeOffset, end);
            }

            return list;
        }

        /// <summary>
        /// Calculates the total number of hours in a list of
        /// timeperiods of a certain type.
        /// </summary>
        /// <param name="list">A list of the employee timeperiods</param>
        /// <param name="type">The type of the eevent be it
        /// No Events, OOO, Searching</param>
        /// <returns>A list</returns>
        public static TimeSpan GetDurationOfType(List<TimePeriod> list, EventType type) {
            return list
                    .Where(t => t.Type == type)
                    .Select(t => t.Duration)
                    .Aggregate(TimeSpan.Zero, (a, b) => a + b);
        }

        /// <summary>
        /// When a person has a gap between events, this method is used to
        /// create the necessary sequence of <code>TimePeriods</code> to fill
        /// that gap. For example, if someone has no events from the end of
        /// work on Thursday to 3:00pm Monday, this method would append to the
        /// list:
        /// 
        ///     - Overnight from Thursday 5:00pm to Friday 8:00am
        ///     - No events from Friday 8:00am to Friday 5:00pm
        ///     - Weekend from Friday 5:00pm to Monday 8:00am
        ///     - No events from Monday 8:00am to Monday 3:00am
        ///     
        /// It would then return writing the next event starting at 3:00pm on
        /// Monday.
        /// </summary>
        /// <param name="list">The list of <code>TimePeriods</code> to append to</param>
        /// <param name="start">The start date selected by the user</param>
        /// <param name="end">The end date selected by the user</param>
        private static void WriteGap(List<TimePeriod> list, DateTime start, DateTime end) {
            var d = start;
            while (d < end) {
                EventType type;
                DateTime next;

                if (IsWeekend(d)) {
                    var daysTillMonday = d.DayOfWeek == DayOfWeek.Saturday
                        ? 2
                        : 1;
                    var monday = d.AddDays(daysTillMonday).Date;
                    next = end < monday ? end : monday;
                    type = EventType.WEEKEND;
                } else if (IsDuringWorkingHours(d)) {
                    var endOfWork = d.Date.Add(WORK_END);
                    next = end < endOfWork ? end : endOfWork;
                    type = EventType.NO_EVENTS;
                } else {
                    if (d.DayOfWeek == DayOfWeek.Friday) {
                        var saturday = d.AddDays(1).Date;
                        next = end < saturday ? end : saturday;
                    } else {
                        var startOfWork = d.Date.Add(WORK_START);

                        if (d.Hour >= 17) {
                            startOfWork = startOfWork.AddDays(1.0);
                        }

                        next = end < startOfWork ? end : startOfWork;
                    }
                    
                    type = EventType.OVERNIGHT;
                }

                TimePeriod t;
                t.Duration = next - d;
                t.Type = type;
                list.Add(t);

                d += t.Duration;
            }
        }

        /// <summary>
        /// Determines the event type each event has using flags in
        /// the text that employees use in their calendars.
        /// </summary>
        /// <param name="e">The employees events</param>
        /// <returns>The eventType</returns>
        private static EventType DecideType(GCEvent e) {
            if (e.Summary.Contains(OOO_FLAG)) {
                return EventType.OOO;
            } else if (e.Summary.Contains(SEARCHING_FLAG)) {
                return EventType.SEARCHING;
            } else {
                return EventType.OFFICE;
            }
        }

        /// <summary>
        /// Decides whether the given DateTime occurs over the weekend.
        /// </summary>
        /// <param name="d">The DateTime to check</param>
        /// <returns>Whether or not the DateTime is over the weekend</returns>
        public static bool IsWeekend(DateTime d) {
            return d.DayOfWeek == DayOfWeek.Saturday
                || d.DayOfWeek == DayOfWeek.Sunday;
        }

        /// <summary>
        /// Determines whether or not the given DateTime is within normal
        /// working hours.
        /// </summary>
        /// <param name="d">The DateTime</param>
        /// <returns>Whether or not the DateTime is within normal working hours</returns>
        private static bool IsDuringWorkingHours(DateTime d) {
            return d.TimeOfDay >= WORK_START
                && d.TimeOfDay < WORK_END;
        }

        /// <summary>
        /// Creates a string that has the amount of time that passed during an event. 
        /// The general format would be 2 days 3 hours 15 minutes.
        /// </summary>
        /// <param name="time">The amount of time that passed during an event.</param>
        /// <returns>A string with the timespan</returns>
        public static string GetDurationString(TimeSpan time) {
            var result = "";

            var days = time.Days;
            if (days > 0) {
                result += $"{days} day{(days == 1 ? "" : "s")} ";
            }

            var hours = time.Hours;
            if (hours > 0) {
                result += $"{hours} hour{(hours == 1 ? "" : "s")} ";
            }

            var mins = time.Minutes;
            if (mins > 0) {
                result += $"{mins} minute{(mins == 1 ? "" : "s")} ";
            }

            return result;
        }

        /// <summary>
        /// Determines the events type and sets
        /// a color based on the type of event.
        /// No Events = Red  OOO = Blue  
        /// Office = Green  Searching = Orange
        /// </summary>
        /// <param name="type">The events type </param>
        /// <returns>The color</returns>
        public static Color DecideColor(EventType type) {
            switch (type) {
                case EventType.OFFICE:
                    return OFFICE_COLOR;
                case EventType.OOO:
                    return OOO_COLOR;
                case EventType.SEARCHING:
                    return SEARCHING_COLOR;
                case EventType.NO_EVENTS:
                    return NO_EVENTS_COLOR;
                case EventType.OVERNIGHT:
                    return NON_WORK_COLOR;
                case EventType.WEEKEND:
                    return NON_WORK_COLOR;
                default:
                    return Color.Transparent;
            }
        }

        /// <summary>
        /// Sets the text that will be used depending on the events type
        /// </summary>
        /// <param name="type">The type of event</param>
        /// <returns>A string that states what the event is</returns>
        public static string TypeString(EventType type) {
            switch (type) {
                case EventType.OFFICE:
                    return "In office";
                case EventType.OOO:
                    return "Out of office";
                case EventType.SEARCHING:
                    return "Searching for work";
                case EventType.NO_EVENTS:
                    return "No events";
                case EventType.OVERNIGHT:
                    return "Non work hours";
                case EventType.WEEKEND:
                    return "Weekend";
                default:
                    return "";
            }
        }
    }
}
