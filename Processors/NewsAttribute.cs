using System;
using System.Linq;
using System.Web.Mvc;
using Jaywing.Ditto.Processors.Contexts;
using Jaywing.Ditto.Processors.Models;
using Jaywing.Ditto.Shared.Models;
using Our.Umbraco.Ditto;
using Umbraco.Web;

using StaticValues = Jaywing.Ditto.Shared.StaticValues;

namespace Jaywing.Ditto.Processors
{
    [DittoProcessorMetaData(ContextType = typeof(PaginationContext))]
    public class NewsAttribute : BaseNewsAttribute
    {
        private readonly UmbracoHelper _helper;

        public NewsAttribute()
        {
            _helper = DependencyResolver.Current.GetService<UmbracoHelper>();
        }

        public override object ProcessValue()
        {
            var pageContext = (PaginationContext)Context;
            var pageNumber = pageContext.PageNumber;
            var pageSize = pageContext.PageSize;
            var items = GetNews().ToList();
            var totalItems = items.Count;
            var totalPages = (long)Math.Ceiling(totalItems / (decimal)pageSize);

            pageNumber = Math.Max(1, Math.Min(pageNumber, totalPages));

            var pagedItems = items
                .Skip((int)(pageNumber - 1) * pageSize)
                .Take(pageSize)
                .As<NewsExtract>();

            return new PagedCollection<NewsExtract>
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages,
                Items = pagedItems,
                PageLabel = _helper.GetDictionaryValue(StaticValues.Dictionary.News.PageLabel)
            };
        }
    }
}