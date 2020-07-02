using System;
using System.Runtime.Serialization;
using AtlasEngine.Modelling.C5.Core.View;
using AtlasEngine.Modelling.C5.Util;

namespace AtlasEngine.Modelling.C5
{

    /// <summary>
    /// The configuration associated with a set of views.
    /// </summary>
    [DataContract]
    public sealed class ViewConfiguration
    {

        internal ViewConfiguration()
        {
            this.Styles = new Styles();
            this.Branding = new Branding();
            this.Terminology = new Terminology();
        }

        [DataMember(Name = "styles", EmitDefaultValue = false)]
        public Styles Styles { get; internal set; }

        private String _theme;
        
        [DataMember(Name = "theme", EmitDefaultValue = false)]
        public string Theme
        {
            get { return _theme; }
            set
            {
                if (value != null && value.Trim().Length > 0)
                {
                    if (Url.IsUrl(value))
                    {
                        _theme = value.Trim();
                    }
                    else {
                        throw new ArgumentException(value + " is not a valid URL.");
                    }
                }
            }
        }
        
        [DataMember(Name = "branding", EmitDefaultValue = false)]
        public Branding Branding { get; internal set; }

        [DataMember(Name = "terminology", EmitDefaultValue = false)]
        public Terminology Terminology { get; internal set; }

        /// <summary>
        /// The type of symbols to use when rendering metadata.
        /// </summary>
        [DataMember(Name = "metadataSymbols", EmitDefaultValue = false)]
        public MetadataSymbols? MetadataSymbols { get; set; }

        [DataMember(Name = "defaultView", EmitDefaultValue = false)]
        public string DefaultView { get; private set; }

        /// <summary>
        /// Sets the view that should be shown by default.
        /// </summary>
        /// <param name="view">A View object</param>
        public void SetDefaultView(View view)
        {
            if (view != null)
            {
                this.DefaultView = view.Key;
            }
        }

        [DataMember(Name = "lastSavedView", EmitDefaultValue = false)]
        internal string LastSavedView { get; set; }

        public void CopyConfigurationFrom(ViewConfiguration configuration)
        {
            LastSavedView = configuration.LastSavedView;
        }

        [DataMember(Name = "viewSortOrder", EmitDefaultValue = true)]
        public ViewSortOrder ViewSortOrder { get; set; }

    }
}
