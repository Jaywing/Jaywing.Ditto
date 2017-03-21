using Jaywing.Ditto.Extensions;
using Our.Umbraco.Ditto;
using Umbraco.Core.Models;

namespace Jaywing.Ditto.Processors
{
    public class CdnUrlAttribute : DittoProcessorAttribute
    {
        public override object ProcessValue()
        {
            var content = Value as IPublishedContent;
            return content?.GetCropUrl(resolveCdnPath: true);
        }
    }
}