using System;

namespace UserAPI.Models.Users
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Diagnosis { get; set; }
        public string Treatment { get; set; }
        public string Adress { get; set; }
        public string Money { get; set; }
        public DateTimeOffset BirthDate { get; set; }
    }
}