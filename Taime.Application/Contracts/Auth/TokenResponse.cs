﻿namespace Taime.Application.Contracts.Auth
{
    public class TokenResponse
    {
        public string AccessToken { get; set; }

        public int ExpiresIn { get; set; }

        public string TokenType { get; set; }
    }
}
