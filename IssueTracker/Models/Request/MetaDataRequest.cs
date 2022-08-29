

namespace Models.Request;
public class MetaDataRequest
{
    public MetaDataRequest(string group, string name, string type, double syze)
    {
        Group = group;
        Name = name;
        Type = type;
        SizeKb = syze;
    }
    public string Group { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public double SizeKb { get; set; }
}
