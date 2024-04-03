﻿namespace Application.User.Common.Add
{
    public class AddGoogleUser
    {
        public string AccessToken { get; set; }
        public string Name { get; set; }
        public string FamilyName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Address {  get; set; }
    }
}