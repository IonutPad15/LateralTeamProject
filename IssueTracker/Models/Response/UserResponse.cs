namespace Models.Response
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; }= string.Empty;
        public IEnumerable<CommentInfo> Comments { get; set; } = Enumerable.Empty<CommentInfo>();
    }
}
