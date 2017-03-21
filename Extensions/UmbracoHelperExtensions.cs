using Umbraco.Core.Models;
using Umbraco.Web;

namespace Jaywing.Ditto.Extensions
{
    public static class UmbracoHelperExtensions
    {
        public static string GetImageUrl(this UmbracoHelper helper, IPublishedContent content, string propertyAlias)
        {
            if (content == null || !content.HasValue(propertyAlias)) return null;
            IPublishedContent image = helper?.TypedMedia(content.GetPropertyValue<int>(propertyAlias));
            return image?.Url;
        }
    }
}
