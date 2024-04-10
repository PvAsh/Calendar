using System;
namespace MeetingSchedularApi
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string timeZone { get; set; }
    }
}
