namespace JKEY_INTERNAL.Models.CustomModel
{
    public class TokenResponse
    {
        public string? AccessToken { get; set; }
        public string? TokenType { get; set; }
        public int ExpiresIn { get; set; }
        public string? RefreshToken { get; set; }
        public int RefreshTokenExpiresIn { get; set; }
    }
}
