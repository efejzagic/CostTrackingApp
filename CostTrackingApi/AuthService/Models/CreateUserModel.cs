﻿namespace AuthService.Models
{
    public class CreateUserModel
    {
        public string username { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string accessToken { get; set; }
    }
}