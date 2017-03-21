using System;
using Jaywing.Ditto.Extensions;
using Our.Umbraco.Ditto;
using Umbraco.Core.Models;

using StaticValues = Jaywing.Ditto.Shared.StaticValues;

namespace Jaywing.Ditto.Processors.Models
{
    public class Image : Link
    {

        public string AltText { get; set; }

        [UmbracoProperty(StaticValues.Properties.UmbracoExtension)]
        public string Extension { get; set; }

        public string DocumentTypeAlias { get; set; }

        public bool IsImage => string.Equals(DocumentTypeAlias, StaticValues.DocumentTypes.Image, StringComparison.OrdinalIgnoreCase);

        [CdnUrl]
        public new string Url { get; set; }

        [CurrentContentAs]
        public IPublishedContent PublishedContent { get; set; }

        public string GetCropUrl(int width, int height) => PublishedContent?.GetCropUrl(width, height, resolveCdnPath: true);
    }
}
