﻿using System.ComponentModel.DataAnnotations;

namespace salemanagementApp.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }
    }
}
