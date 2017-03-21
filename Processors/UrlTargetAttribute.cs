using Our.Umbraco.Ditto;
using Umbraco.Core.Models;

using StaticValues = Jaywing.Ditto.Shared.StaticValues;

namespace Jaywing.Ditto.Processors
{
    public class UrlTargetAttribute : DittoProcessorAttribute
    {
        public override object ProcessValue()
        {
            var content = Value as IPublishedContent;
            return content?.DocumentTypeAlias == StaticValues.DocumentTypes.Redirect || (!content?.Url?.StartsWith("/") ?? false);
        }
    }
}