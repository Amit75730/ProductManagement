﻿using System.ComponentModel.DataAnnotations;

namespace ProductManagementMVC.Models
{
    public class UserLoginDTO
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
