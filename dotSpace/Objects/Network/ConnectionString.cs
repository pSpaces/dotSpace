using dotSpace.Enumerations;
using System;
using System.Text.RegularExpressions;

namespace dotSpace.Objects.Network
{
    /// <summary>
    /// This entity maps a valid connection string to a property based representation.
    /// </summary>
    public class ConnectionString
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        /// <summary>
        /// Initializes a new instances of the ConnectionString class.
        /// </summary>
        public ConnectionString(string uri)
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

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Properties

        /// <summary>
        /// Gets the specified protocol. This property is optional. If no value was defined, it defaults to TCP.
        /// </summary>
        public Protocol Protocol { get; private set; }

        /// <summary>
        /// Gets the specified host. 
        /// </summary>
        public string Host { get; private set; }

        /// <summary>
        /// Gets the specified port. This property is optional. If no value was defined, it defaults to 31415.
        /// </summary>
        public int Port { get; private set; }

        /// <summary>
        /// Gets the specified target space. This property is optional depending on usage.
        /// </summary>
        public string Target { get; private set; }

        /// <summary>
        /// Gets the specified connection scheme. This property is optional. If no value was defined, it defaults to KEEP.
        /// </summary>
        public ConnectionMode Mode { get; private set; }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Methods

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        /// <summary>
        /// Returns true if the values representing the connection string are equal.
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is ConnectionString other)
            {
                return this.Protocol == other.Protocol && this.Host.Equals(other.Host)
                       && this.Port == other.Port && this.Target.Equals(other.Target) && this.Mode == other.Mode;
            }
            return false;
        }
        
        #endregion
    }
}
