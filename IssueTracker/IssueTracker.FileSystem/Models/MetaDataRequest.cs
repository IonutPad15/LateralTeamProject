﻿namespace IssueTracker.FileSystem.Models;
public class MetaDataRequest
{
    public string Id { get; set; } = string.Empty;
    public string Group { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public double SizeKb { get; set; }
}
