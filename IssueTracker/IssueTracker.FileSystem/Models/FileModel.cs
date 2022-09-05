namespace IssueTracker.FileSystem.Models;
public class FileModel
{
    public FileModel()
    {
    }
    public FileModel(string id, string group, string name, string type, double sizeKb)
    {
        Id = id;
        Name = name;
        Group = group;
        Type = type;
        SizeKb = sizeKb;
    }

    public string Id { get; set; } = string.Empty;
    public string? Name { get; set; }
    public string? Extension { get; set; }
    public Stream? Content { get; set; }
    public string? Link { get; set; }
    public string Group { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public double SizeKb { get; set; }
}
