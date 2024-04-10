namespace MeetingSchedularApi
{
    public class Meeting
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public List<User> Attendees { get; set; }
    }

    public class MeetingModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public Guid UserId { get; set; }
    }
}
