using Jaywing.Ditto.Processors.MultiProcessors;
using Our.Umbraco.Ditto;

using StaticValues = Jaywing.Ditto.Shared.StaticValues;

namespace Jaywing.Ditto.Processors.Models
{
    public class Link 
    {
        [UmbracoProperty(StaticValues.Properties.Name)]
        public string Title { get; set; }

        [UrlOrRedirectUrl]
        public string Url { get; set; }

        [UrlTarget]
        public bool NewWindow { get; set; }

        public string NewWindowAttribute => NewWindow ? StaticValues.Attributes.TargetBlank : string.Empty;

        [UmbracoPropertyAndPicker]
        public Image ListingImage { get; set; }

        [Extract(300)]
        public string Extract { get; set; }

        public bool HasImage => ListingImage != null;

        [UmbracoDictionary(StaticValues.Dictionary.Generic.LoadingLabel)]
        public string LoadingLabel { get; set; }

        [UmbracoDictionary(StaticValues.Dictionary.Generic.OpensInANewWindowLabel)]
        public string OpensInANewWindowLabel { get; set; }
    }
}