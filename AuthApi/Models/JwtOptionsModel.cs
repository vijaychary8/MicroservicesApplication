﻿namespace AuthApi.Models
{
    public class JwtOptionsModel
    {
        public string? Issuer{ get; set; }=string.Empty;
        public string? Audience { get; set; } = string.Empty; 
        public string? Secret { get; set; } = string.Empty;
    }
}
