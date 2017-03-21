using System;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Jaywing.Ditto.Shared.Models;
using Umbraco.Core;
using Umbraco.Core.Cache;
using Umbraco.Core.Models;
using Umbraco.Web;
using Umbraco.Web.Models;

namespace Jaywing.Ditto.Extensions
{
    public static class PublishedContentExtensions
    { 
        public static T Get<T>(this IPublishedContent content, string propertyAlias, bool recursive = false, T defaultValue = default(T))
        {
            return content == null ? defaultValue : content.GetPropertyValue<T>(propertyAlias, recursive, defaultValue);
        }

        public static string GetTitleOrName(this IPublishedContent content, string titleAttribute)
        {
            return !string.IsNullOrEmpty(content.GetPropertyValue<string>(titleAttribute)) ?
                 content.GetPropertyValue<string>(titleAttribute) : content.Name;
        }

        public static string GetUrl(this IPublishedContent content, string redirectDocTypeAlias, string redirectUrlAlias)
        {
            return content.DocumentTypeAlias == redirectDocTypeAlias ? content.GetPropertyValue<string>(redirectUrlAlias) : content.Url;
        }

        public static string GetCropUrl(
            this IPublishedContent mediaItem,
            int? width = null,
            int? height = null,
            string propertyAlias = Constants.Conventions.Media.File,
            string cropAlias = null,
            int? quality = null,
            ImageCropMode? imageCropMode = null,
            ImageCropAnchor? imageCropAnchor = null,
            bool preferFocalPoint = false,
            bool useCropDimensions = false,
            bool cacheBuster = true,
            string furtherOptions = null,
            ImageCropRatioMode? ratioMode = null,
            bool upScale = true,
            bool resolveCdnPath = false)
        {
            string cropUrl = ImageCropperTemplateExtensions.GetCropUrl(
                mediaItem, width, height, propertyAlias, cropAlias, quality, imageCropMode, imageCropAnchor, 
                preferFocalPoint, useCropDimensions, cacheBuster, furtherOptions, ratioMode, upScale);

            if (!resolveCdnPath) return cropUrl;
            
            string cachePrefix = "Jaywing-AzureBlobCache";
            string cacheKey = $"{cachePrefix}{cropUrl}";
            string currentDomain = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
            string absoluteCropPath = $"{currentDomain}{cropUrl}";

            IRuntimeCacheProvider runtimeCache = DependencyResolver.Current.GetService<ApplicationContext>()?.ApplicationCache?.RuntimeCache;
            if (runtimeCache == null) return cropUrl;

            var cachedItem = runtimeCache.GetCacheItem<CachedImage>(cacheKey);
            if (cachedItem != null) return cachedItem.CacheUrl;

            var newCachedImage = new CachedImage { WebUrl = cropUrl };

            try
            {
                var request = (HttpWebRequest) WebRequest.Create(absoluteCropPath);
                request.Method = WebRequestMethods.Http.Head;
                using (var response = (HttpWebResponse) request.GetResponse())
                {
                    HttpStatusCode responseCode = response.StatusCode;
                    if (!responseCode.Equals(HttpStatusCode.OK)) return cachedItem.CacheUrl;
                    newCachedImage.CacheUrl = response.ResponseUri.AbsoluteUri;
                    runtimeCache.InsertCacheItem<CachedImage>(cacheKey, () => newCachedImage);
                    return response.ResponseUri.AbsoluteUri;
                }
            }
            catch
            {
                return cropUrl;
            }
        }

    }
}