﻿namespace TrialP.Auth.DTO
{
    public class RegisterDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string RepeatPassword { get; set; }
        public string? Phone { get; set; }
    }
}
