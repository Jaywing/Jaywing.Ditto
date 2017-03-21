using Jaywing.Ditto.Extensions;
using Our.Umbraco.Ditto;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Web;

using StaticValues = Jaywing.Ditto.Shared.StaticValues;

namespace Jaywing.Ditto.Processors
{
    public class ExtractAttribute : DittoProcessorAttribute
    {
        private int TruncateLimit { get; set; }

        public ExtractAttribute(int truncateLimit = 150)
        {
            TruncateLimit = truncateLimit;
        }
        public override object ProcessValue()
        {
            var content = Value as IPublishedContent;
            if (content == null) return null;

            if (content.HasValue(StaticValues.Properties.Extract)) return content.Get<string>(StaticValues.Properties.Extract);
            if (content.HasValue(StaticValues.Properties.BodyText)) return content.Get<string>(StaticValues.Properties.BodyText)?.StripHtml()?.Truncate(TruncateLimit);
            return null;
        }
    }
}