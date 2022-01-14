using System;

namespace Consumer.Models
{
    public class Person
    {
        public int UserId { get; set; }
        public string Status { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
