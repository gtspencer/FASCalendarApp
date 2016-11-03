using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFCalendarApp {

    /// <summary>
    /// Hold a list of employees that is populated by the data from Google.
    /// </summary>
    public static class Employees {

        public static List<Employee> List { get; set; } = new List<Employee>();
    }
}
