namespace AuthApi.Models
{
    public class JwtOptionsModel
    {
        public string? Issuer{ get; set; }=string.Empty;
        public string? Ausience { get; set; } = string.Empty; 
        public string? Secert { get; set; } = string.Empty;
    }
}
