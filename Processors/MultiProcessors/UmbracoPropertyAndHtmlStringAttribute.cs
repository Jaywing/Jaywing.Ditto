using System.Linq;
using Our.Umbraco.Ditto;

namespace Jaywing.Ditto.Processors.MultiProcessors
{
    public class UmbracoPropertyAndHtmlStringAttribute : DittoMultiProcessorAttribute
    {
        public UmbracoPropertyAndHtmlStringAttribute(
            string propertyName = null,
            string altPropertyName = null, 
            bool recursive = false,
            object defaultValue = null)
           : base(Enumerable.Empty<DittoProcessorAttribute>())
        {
            base.Attributes.AddRange(new DittoProcessorAttribute[]
            {
                new UmbracoPropertyAttribute(propertyName, altPropertyName, recursive, defaultValue),
                new HtmlStringAttribute()
            });
        }
    }
}

