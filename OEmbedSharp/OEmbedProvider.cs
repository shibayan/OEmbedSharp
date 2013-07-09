using System.Linq;
using System.Text.RegularExpressions;

namespace OEmbedSharp
{
    public class OEmbedProvider
    {
        public string[] Schemes { get; set; }

        public string Endpoint { get; set; }

        public bool CanHandleUrl(string url)
        {
            return Schemes.Any(p => Regex.IsMatch(url, p));
        }
    }
}
