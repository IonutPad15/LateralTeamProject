namespace Models.Response;
public class StatusResponse
{
    public StatusResponse()
    {
        Type = String.Empty;
    }
    public int Id { get; set; }
    public string Type { get; set; }
}
