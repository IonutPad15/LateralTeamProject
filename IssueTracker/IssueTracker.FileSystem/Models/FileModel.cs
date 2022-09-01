namespace IssueTracker.FileSystem.Models;
public class FileModel
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; }
    public string Extension { get; set; }
    public Stream Content { get; set; }
    public string Link { get; set; }
    public string Group { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public double SizeKb { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
