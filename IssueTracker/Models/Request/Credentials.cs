namespace Models.Request;

public class Credentials
{
    public Credentials(string nameEmail, string password)
    {
        NameEmail = nameEmail;
        Password = password;
    }

    public string NameEmail { get; set; }
    public string Password { get; set; }
}
