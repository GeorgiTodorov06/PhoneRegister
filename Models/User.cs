﻿namespace PhoneRegister.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public List<PhoneList> PhoneLists { get; set; }
    }
}
