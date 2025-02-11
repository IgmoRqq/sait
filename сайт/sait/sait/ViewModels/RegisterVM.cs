﻿using System.ComponentModel.DataAnnotations;

namespace sait.ViewModels
{
    public class RegisterVM
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Compare("Password", ErrorMessage = "Пароли не совпадают.")]
        [Display(Name = "Подтвердите пароль")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }

        [DataType(DataType.MultilineText)]
        public string? Address { get; set; }
    }
}