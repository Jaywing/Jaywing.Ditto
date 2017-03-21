using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Jaywing.Ditto.Processors.Models;
using Jaywing.Ditto.Shared.Models;
using Our.Umbraco.Ditto;
using Umbraco.Core.Models;
using Umbraco.Web;

using StaticValues = Jaywing.Ditto.Shared.StaticValues;

namespace Jaywing.Ditto.Processors
{ 
    public class LatestNewsAttribute : BaseNewsAttribute
    {
        private int Count { get; }
        private UmbracoHelper _helper { get; }

        public LatestNewsAttribute(int count = 3)
        {
            Count = count;
            _helper = DependencyResolver.Current.GetService<UmbracoHelper>();
        }

        public override object ProcessValue()
        {
            var items = new List<IPublishedContent>();

            if (Context.Content.GetPropertyValue<bool>(StaticValues.Properties.AutoLatestNews))
                items = GetNews().ToList();

            if (Context.Content.HasValue(StaticValues.Properties.LatestNewsAlternativeContent))
                items = GetAltContentItems().ToList();

            var pageNumber = 0L;
            int totalItems = items.Count;
            int pageSize = Count;
            var totalPages = (long)Math.Ceiling(totalItems / (decimal)pageSize);
            pageNumber = Math.Max(1, Math.Min(pageNumber, totalPages));

            var pagedItems = items
                .Skip((int)(pageNumber - 1) * pageSize)
                .Take(pageSize)
                .As<Link>();

            return new PagedCollection<Link>
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages,
                Items = pagedItems,
                PageLabel = Context.Content.GetPropertyValue<string>(StaticValues.Properties.LatestNewsHeading)
            };
        }

        protected IEnumerable<IPublishedContent> GetAltContentItems()
        {
            var ids = Context.Content.GetPropertyValue<string>(StaticValues.Properties.LatestNewsAlternativeContent)?
                .Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse);

            return ids?
                .Select(x => _helper.TypedContent(x))
                .Where(x => x != null)
                .ToList();
        }
    }
}