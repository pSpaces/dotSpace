using dotSpace.Enumerations;
using System;
using System.Text.RegularExpressions;

namespace dotSpace.Objects
{
    public class GateInfo
    {
        public GateInfo(string uri)
        {
            Match match = new Regex(@"^(.+://){0,1}(.+)(:[0-9]+)(\/[a-zA-Z]+){0,1}(\?[a-zA-Z]+){0,1}$").Match(uri);
            if (match.Success && match.Groups.Count == 6)
            {
                this.Protocol = string.IsNullOrEmpty(match.Groups[1].Value) ? "tcp" : match.Groups[1].Value.TrimEnd(':', '/').ToLower();
                this.Host = match.Groups[2].Value;
                this.Port = string.IsNullOrEmpty(match.Groups[3].Value) ? 31415 : int.Parse(match.Groups[3].Value.TrimStart(':'));
                this.Target = string.IsNullOrEmpty(match.Groups[5].Value) ? string.Empty : match.Groups[4].Value.TrimStart('/');
                string mode = string.IsNullOrEmpty(match.Groups[5].Value) ? "KEEP" : match.Groups[5].Value.TrimStart('?');
                this.Mode = (ConnectionMode)Enum.Parse(typeof(ConnectionMode), mode);
            }
        }

        public string Protocol { get; private set; }
        public string Host { get; private set; }
        public int Port { get; private set; }
        public string Target { get; private set; }
        public ConnectionMode Mode { get; private set; }
    }
}
