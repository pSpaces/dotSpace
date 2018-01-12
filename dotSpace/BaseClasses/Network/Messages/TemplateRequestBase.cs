using dotSpace.Enumerations;
using dotSpace.Objects.Json;

namespace dotSpace.BaseClasses.Network.Messages
{
    /// <summary>
    /// Abstract entity containing the minimal properties of any template type message.
    /// </summary>
    public abstract class TemplateRequestBase : RequestBase
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        /// <summary>
        /// Initializes a new instances of the TemplateRequestBase class.
        /// </summary>
        public TemplateRequestBase()
        {
        }

        /// <summary>
        /// Initializes a new instances of the TemplateRequestBase class.
        /// </summary>
        public TemplateRequestBase(ActionType action, string source, string session, string target, object[] template) : base(action, source, session, target)
        {
            this.Template = template;
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Properties

        /// <summary>
        /// Gets or sets the underlying array of values constituting the template pattern.
        /// </summary>
        public object[] Template { get; set; }

        #endregion
    }
}
