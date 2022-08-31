namespace Models.Request;
public class FileDeleteRequest
{
    public FileDeleteRequest(string fileid, string groupid)
    {
        FileId = fileid;
        GroupId = groupid;
    }
    public string FileId { get; set; }
    public string GroupId { get; set; }
}
