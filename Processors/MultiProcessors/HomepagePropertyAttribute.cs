using System.Linq;
using Our.Umbraco.Ditto;

namespace Jaywing.Ditto.Processors.MultiProcessors
{
    public class HomepagePropertyAttribute : DittoMultiProcessorAttribute
    {
        public HomepagePropertyAttribute(
            string propertyName = null,
            string altPropertyName = null,
            bool recursive = false,
            object defaultValue = null)
           : base(Enumerable.Empty<DittoProcessorAttribute>())
        {
            base.Attributes.AddRange(new DittoProcessorAttribute[]
            {
                new HomepageAttribute(),
                new UmbracoPropertyAttribute(propertyName, altPropertyName, recursive, defaultValue)
            });
        }
    }
}