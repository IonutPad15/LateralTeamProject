namespace IssueTracker.FileSystem.Models;
public class MetaDataResp
{
    public MetaDataResp(string id, string group, string name, string type, double syze)
    {
        Id = id;
        Group = group;
        Name = name;
        Type = type;
        SizeKb = syze;
    }
    public string Id { get; set; }
    public string Group { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public double SizeKb { get; set; }
}
