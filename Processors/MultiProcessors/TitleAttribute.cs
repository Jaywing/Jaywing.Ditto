using System.Linq;
using Our.Umbraco.Ditto;

using StaticValues = Jaywing.Ditto.Shared.StaticValues;

namespace Jaywing.Ditto.Processors.MultiProcessors
{
    public class TitleAttribute : DittoMultiProcessorAttribute
    {
        public string TitleAttr { get; set; }

        public TitleAttribute() : base(Enumerable.Empty<DittoProcessorAttribute>())
        {
            base.Attributes.AddRange(new[] {
                new UmbracoPropertyAttribute(TitleAttr),
                new AltUmbracoPropertyAttribute(StaticValues.Properties.Name)
            });
        }
    }
}