using System;
using System.Collections.Generic;
using System.Linq;
using Jaywing.Ditto.Extensions;
using Our.Umbraco.Ditto;
using Umbraco.Core.Models;
using Umbraco.Web;

using StaticValues = Jaywing.Ditto.Shared.StaticValues;

namespace Jaywing.Ditto.Processors
{
    public abstract class BaseNewsAttribute : DittoProcessorAttribute
    {
        protected IEnumerable<IPublishedContent> GetNews()
        {
            var content = Value as IPublishedContent;
            if (content == null) return Enumerable.Empty<IPublishedContent>();

            IPublishedContent newsArchive = content
                .AncestorsOrSelf(StaticValues.DocumentTypes.Homepage)
                .FirstOrDefault()?
                .Children
                .FirstOrDefault(x => x.DocumentTypeAlias == StaticValues.DocumentTypes.News && x.IsVisible());

            if (newsArchive == null) return Enumerable.Empty<IPublishedContent>();

            return newsArchive.Children
                .Where(x => x.DocumentTypeAlias == StaticValues.DocumentTypes.Article && x.IsVisible())
                .OrderByDescending(x => x.Get<DateTime>(StaticValues.Properties.PublishDate))
                .ThenByDescending(x => x.CreateDate);
        }

    }
}