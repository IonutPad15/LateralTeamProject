
namespace Models.Response
{
    public class UserToken
    {
        public string Token { get; set; } = string.Empty;
        public DateTime ExpirationDate { get; set; }
        public Guid UserId { get; set; }
    }
}
