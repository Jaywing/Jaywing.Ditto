using System;
using System.Collections.Generic;
using Jaywing.Ditto.Extensions;
using Jaywing.Ditto.Shared.Models;
using Our.Umbraco.Ditto;
using Umbraco.Core.Models;
using Umbraco.Web;
using System.Linq;

using StaticValues = Jaywing.Ditto.Shared.StaticValues;

namespace Jaywing.Ditto.Processors
{
    public class ChildrenAttribute : DittoProcessorAttribute
    {
        private readonly Func<IPublishedContent, bool> _contentIsVisible = x => x.IsVisible();
        private readonly UmbracoHelper _helper;

        private bool IncludeGrandChildren { get; set; }

        public ChildrenAttribute(bool includeGrandChildren)
        {
            _helper = new UmbracoHelper(UmbracoContext.Current);
            IncludeGrandChildren = includeGrandChildren;
        }

        public override object ProcessValue()
        {
            return Context.Content?
                .Children
                .Where(_contentIsVisible)
                .Select(x => new TreeNode
                {
                    Name = x.GetTitleOrName(StaticValues.Properties.Title),
                    Url = x.GetUrl(StaticValues.DocumentTypes.Redirect, StaticValues.Properties.RedirectUrl),
                    HasTemplate = x.TemplateId > 0,
                    Description = x.GetPropertyValue<string>(StaticValues.Properties.Description),
                    ImageUrl = _helper.GetImageUrl(x, StaticValues.Properties.ListingImage),
                    Children = IncludeGrandChildren ? GetChildren(x) : null,
                    NewWindow = x.DocumentTypeAlias == StaticValues.DocumentTypes.Redirect,
                });
        }

        private IEnumerable<TreeNode> GetChildren(IPublishedContent parent)
        {
            return parent
                .Children
                .Where(_contentIsVisible)
                .Select(x => new TreeNode
                {
                    Name = x.GetTitleOrName(StaticValues.Properties.Title),
                    Url = x.GetUrl(StaticValues.DocumentTypes.Redirect, StaticValues.Properties.RedirectUrl),
                    ImageUrl = _helper.GetImageUrl(x, StaticValues.Properties.ListingImage),
                    HasTemplate = x.TemplateId > 0,
                    NewWindow = x.DocumentTypeAlias == StaticValues.DocumentTypes.Redirect,
                });
        }
    }
}