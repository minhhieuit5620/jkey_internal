namespace JKEY_INTERNAL.Models.CustomModel
{
    public class RefreshTokenModel
    {
        public string? RefreshToken { get; set; }
        public DateTime RefreshToken_ExpiryDate { get; set; }
        public string? CreatedByIp { get; set; }
        public TimeSpan RefreshToken_ExpiryDateTimeSpan { get; set; }
        public string? Token { get; set; }
        public DateTime Token_ExpiryDate { get; set; }
        public TimeSpan Token_ExpiryDateTimeSpan { get; set; }
        public DateTime LastUsedDate { get; set; }



    }
}
