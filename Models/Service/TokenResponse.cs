namespace IUR_Backend.Models.Service
{
    public class TokenResponse
    {
        public string Token { get; set; }
        public DateTime Expired { get; set; }
    }
}
