using Application.User.Common;

namespace Application.Calendar
{

    public class MyEvent
    {
        public string Id { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public List<UserEventResult> Attendees { get; set; }
        public EventExtendedProperties ExtendedProperties { get; set; }
    }


    public class MyAddedEvent
    {
        /*        public string Id { get; set; }
        */
        public string Summary { get; set; }
        public string Description { get; set; }
        public string location { get; set; }
        public string TreeId { get; set; } //List of tree, seprate with comma
        public string DepartmentEmail { get; set; } 
        public EventDateTime Start { get; set; }
        public EventDateTime End { get; set; }
        public List<User> Attendees { get; set; }
    }

    public class MyUpdatedEvent
    {
        public string Summary { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public EventDateTime Start { get; set; }
        public EventDateTime End { get; set; }
        public List<User> Attendees { get; set; }
    }

    public class MyUpdatedJobStatus
    {
        public class EventExtendedProperties
        {
            Dictionary<string, string> PrivateProperties { get; set; }
        }
    }

    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class EventDateTime
    {
        public string DateTime { get; set; }
    }

    public class EventExtendedProperties
    {
        public static readonly string JobWorkingStatus = "JobWorkingStatus";
        public static readonly string Tree = "Tree";
        public static readonly string DepartmentId = "DepartmentId";
        public static readonly string DepartmentEmail = "DepartmentEmail";

        private Dictionary<string, string> privateProperties;

        public Dictionary<string, string> PrivateProperties
        {
            get { return privateProperties; }
            set
            {
                // Ensure privateProperties is initialized
                privateProperties = privateProperties ?? new Dictionary<string, string>();

                // Set or update specific keys
                privateProperties[JobWorkingStatus] = value.ContainsKey(JobWorkingStatus) ? value[JobWorkingStatus] : "";
                privateProperties[Tree] = value.ContainsKey(Tree) ? value[Tree] : "";
                privateProperties[DepartmentEmail] = value.ContainsKey(DepartmentEmail) ? value[DepartmentEmail] : "";
            }
        }
    }
}
