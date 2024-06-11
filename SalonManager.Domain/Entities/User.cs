﻿namespace SalonManager.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string? Email { get; set; }
        public string PasswordHash { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime DateCreated { get; set; } 
        public DateTime DateUpdated { get; set; }
    }
}
