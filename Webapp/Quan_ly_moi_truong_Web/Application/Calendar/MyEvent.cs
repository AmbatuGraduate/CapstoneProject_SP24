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
        public List<UserResult> Attendees { get; set; }
        public EventExtendedProperties ExtendedProperties { get; set; }
    }

        public class EventsInfo
        {
            public string Id { get; set; }
            public string Summary { get; set; }
            public string Description { get; set; }
            public string Location { get; set; }
            public DateTime Start { get; set; }
            public DateTime End { get; set; }
            public List<CalendarUser> Attendees { get; set; }
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
        public Dictionary<string, string> PrivateProperties { get; set; }
    }
}
