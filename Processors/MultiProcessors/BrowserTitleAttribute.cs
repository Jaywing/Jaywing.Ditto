using System.Linq;
using Our.Umbraco.Ditto;

using StaticValues = Jaywing.Ditto.Shared.StaticValues;

namespace Jaywing.Ditto.Processors.MultiProcessors
{
    public class BrowserTitleAttribute : DittoMultiProcessorAttribute
    {
        private string BrowserTitleAttrib { get; set; }

        public BrowserTitleAttribute() : base(Enumerable.Empty<DittoProcessorAttribute>())
        {
            base.Attributes.AddRange(new[] {
                new UmbracoPropertyAttribute(BrowserTitleAttrib),
                new AltUmbracoPropertyAttribute(StaticValues.Properties.Title),
                new AltUmbracoPropertyAttribute(StaticValues.Properties.Name)
            });
        }
    }
}