using System.Linq;
using Our.Umbraco.Ditto;

using StaticValues = Jaywing.Ditto.Shared.StaticValues;

namespace Jaywing.Ditto.Processors.MultiProcessors
{
    public class PublishDateAttribute : DittoMultiProcessorAttribute
    {
        private string PublishDateAttr { get; set; }

        public PublishDateAttribute() : base(Enumerable.Empty<DittoProcessorAttribute>())
        {
            base.Attributes.AddRange(new[] {
                new UmbracoPropertyAttribute(PublishDateAttr),
                new AltUmbracoPropertyAttribute(StaticValues.Properties.CreateDate)
            });
        }
    }
}