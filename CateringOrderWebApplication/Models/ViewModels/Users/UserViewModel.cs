﻿namespace CateringOrderWebApplication.Models.ViewModels.Users
{
    public class UserViewModel
    {
        public List<User> Users { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}