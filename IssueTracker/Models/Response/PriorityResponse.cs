namespace Models.Response
{
    public class PriorityResponse
    {
        public PriorityResponse()
        {
            Type = String.Empty;
        }
        public int Id { get; set; }
        public string? Type { get; set; }
    }
}
