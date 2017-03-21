using System;
using System.Collections.Generic;
using Jaywing.Ditto.Shared.Models;
using Our.Umbraco.Ditto;
using Umbraco.Core.Models;
using Umbraco.Web;
using System.Linq;
using Jaywing.Ditto.Extensions;
using StaticValues = Jaywing.Ditto.Shared.StaticValues;


namespace Jaywing.Ditto.Processors
{
    public class SitemapAttribute : DittoProcessorAttribute
    {
        private readonly Func<IPublishedContent, bool> _contentIsVisibleAndNotData = x => 
            x.IsVisible() &&
            x.DocumentTypeAlias != StaticValues.DocumentTypes.DataRepository;

        public override object ProcessValue()
        {
            return Context.Content?
                .AncestorOrSelf(StaticValues.DocumentTypes.Homepage)?
                .Children
                .Where(_contentIsVisibleAndNotData)
                .Select(x => new TreeNode
                {
                    Id = x.Id,
                    Name = x.GetTitleOrName(StaticValues.Properties.Title),
                    Url = x.GetUrl(StaticValues.DocumentTypes.Redirect, StaticValues.Properties.RedirectUrl),
                    Children = GetChildrenRescursively(x),
                    HasTemplate = x.TemplateId > 0,
                    NewWindow = x.DocumentTypeAlias == StaticValues.DocumentTypes.Redirect
                });
        }

        private IEnumerable<TreeNode> GetChildrenRescursively(IPublishedContent parent)
        {
            return parent?
                .Children
                .Where(_contentIsVisibleAndNotData)
                .Select(x => new TreeNode
                {
                    Name = x.GetTitleOrName(StaticValues.Properties.Title),
                    Url = x.GetUrl(StaticValues.DocumentTypes.Redirect, StaticValues.Properties.RedirectUrl),
                    Children = GetChildrenRescursively(x),
                    HasTemplate = x.TemplateId > 0,
                    NewWindow = x.DocumentTypeAlias == StaticValues.DocumentTypes.Redirect
                });
        }
    }
}