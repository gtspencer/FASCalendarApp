namespace WFCalendarApp {
    
    /// <summary>
    /// Represents a type of event (or lack of events). There should be a
    /// better name for this, because it is used to represent both events and
    /// gaps between events.
    /// </summary>
    public enum EventType {
        OFFICE,
        OOO,
        NO_EVENTS,
        SEARCHING,
        OVERNIGHT,
        WEEKEND
    }
}
