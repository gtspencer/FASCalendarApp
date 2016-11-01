using System;

namespace WFCalendarApp {
    
    /// <summary>
    /// Represents an emplyee.
    /// </summary>
    public class Employee : IComparable<Employee> {

        private string name;
        private string primaryEmail;

        public string Name {
            get {
                return name;
            }

            set {
                name = value;
            }
        }

        public string PrimaryEmail {
            get {
                return primaryEmail;
            }

            set {
                primaryEmail = value;
            }
        }

        /// <summary>
        /// Constructs an employee.
        /// </summary>
        /// <param name="name">The employee's name</param>
        /// <param name="primaryEmail">The employee's primary email</param>
        public Employee(string name, string primaryEmail) {
            this.name = name;
            this.primaryEmail = primaryEmail;
        }

        public int CompareTo(Employee other) {
            return name.CompareTo(other.name);
        }

        public override string ToString() {
            return name;
        }

        public override bool Equals(object obj) {
            var that = obj as Employee;

            if (that == null) {
                return false;
            }

            return string.Equals(name, that.name)
                && string.Equals(primaryEmail, that.primaryEmail);
        }

        public override int GetHashCode() {
            var result = 17;
            var prime = 31;

            result = result * prime + name.GetHashCode();
            result = result * prime + primaryEmail.GetHashCode();

            return result;
        }
    }
}
