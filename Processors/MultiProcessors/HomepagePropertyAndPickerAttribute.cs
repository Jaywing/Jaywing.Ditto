using System.Linq;
using Our.Umbraco.Ditto;

namespace Jaywing.Ditto.Processors.MultiProcessors
{
    public class HomepagePropertyAndPickerAttribute : DittoMultiProcessorAttribute
    {
        public HomepagePropertyAndPickerAttribute(
            string propertyName = null,
            string altPropertyName = null,
            bool recursive = false,
            object defaultValue = null)
           : base(Enumerable.Empty<DittoProcessorAttribute>())
        {
            base.Attributes.AddRange(new DittoProcessorAttribute[]
            {
                new HomepageAttribute(),
                new UmbracoPropertyAndPickerAttribute(propertyName, altPropertyName, recursive, defaultValue)
            });
        }
    }
}