
namespace Models.Response;

public class UserToken
{
    public UserToken(string token, DateTime expirationDate, Guid userId)
    {
        Token = token;
        ExpirationDate = expirationDate;
        UserId = userId;
    }

    public string Token { get; set; }
    public DateTime ExpirationDate { get; set; }
    public Guid UserId { get; set; }
}
