using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFCalendarApp {
    
    /// <summary>
    /// Represents a continuous timespan of the same EventType, e.g. 4 hours
    /// searching for work, 2 days out of office, etc.
    /// </summary>
    public struct TimePeriod {
        public TimeSpan Duration;
        public EventType Type;
    }
}
