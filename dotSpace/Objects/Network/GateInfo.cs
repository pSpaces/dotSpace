using dotSpace.Enumerations;
using System;
using System.Text.RegularExpressions;

namespace dotSpace.Objects.Network
{
    public class GateInfo
    {
        public GateInfo(string uri)
        {
            Match match = new Regex(@"^(.+://){0,1}(.[^:\/\?]+)(:[0-9]+){0,1}(\/[a-zA-Z]+){0,1}(\?[a-zA-Z]+){0,1}$").Match(uri);
            if (match.Success && match.Groups.Count == 6)
            {
                string protocol = string.IsNullOrEmpty(match.Groups[1].Value) ? Protocol.TCP.ToString() : match.Groups[1].Value.TrimEnd(':', '/').ToUpper();
                this.Protocol = (Protocol)Enum.Parse(typeof(Protocol), protocol);
                this.Host = match.Groups[2].Value;
                this.Port = string.IsNullOrEmpty(match.Groups[3].Value) ? 31415 : int.Parse(match.Groups[3].Value.TrimStart(':'));
                this.Target = string.IsNullOrEmpty(match.Groups[4].Value) ? string.Empty : match.Groups[4].Value.TrimStart('/');
                string mode = string.IsNullOrEmpty(match.Groups[5].Value) ? ConnectionMode.KEEP.ToString() : match.Groups[5].Value.TrimStart('?');
                this.Mode = (ConnectionMode)Enum.Parse(typeof(ConnectionMode), mode);
            }
        }

        public Protocol Protocol { get; private set; }
        public string Host { get; private set; }
        public int Port { get; private set; }
        public string Target { get; private set; }
        public ConnectionMode Mode { get; private set; }
    }
}
