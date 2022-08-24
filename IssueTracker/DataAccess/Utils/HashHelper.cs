using System.Security.Cryptography;
using System.Text;

namespace DataAccess.Utils;
public class HashHelper
{
    private readonly SHA256 _hasher;
    public HashHelper()
    {
        _hasher = SHA256.Create();
    }
    public string GetHash(string text)
    {
        byte[] bytes = _hasher.ComputeHash(Encoding.UTF8.GetBytes(text));
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < bytes.Length; i++)
        {
            sb.Append(bytes[i].ToString("x2"));
        }
        return sb.ToString();
    }
}
