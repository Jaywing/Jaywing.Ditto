using System;
using Jaywing.Ditto.Shared.Models;
using Our.Umbraco.Ditto;
using Umbraco.Core.Models;
using Umbraco.Web;
using System.Linq;
using Jaywing.Ditto.Extensions;
using StaticValues = Jaywing.Ditto.Shared.StaticValues;

namespace Jaywing.Ditto.Processors
{
    public class PrimaryNavigationAttribute : DittoProcessorAttribute
    {
        private readonly Func<IPublishedContent, bool> _contentIsVisible = x =>
            x.TemplateId > 0 &&
            !x.GetPropertyValue<bool>(StaticValues.Properties.HideFromPrimaryNavigation) && 
            x.IsVisible();
        
        public override object ProcessValue()
        {
            IPublishedContent currentPage = Context.Content;

            return currentPage?
                .AncestorOrSelf(StaticValues.DocumentTypes.Homepage)?
                .Children
                .Where(_contentIsVisible)
                .Select(x => new TreeNode
                {
                    Id = x.Id,
                    Name = x.GetTitleOrName(StaticValues.Properties.Title),
                    Url = x.GetUrl(StaticValues.DocumentTypes.Redirect, StaticValues.Properties.RedirectUrl),
                    Current = x.IsAncestorOrSelf(currentPage),
                    NewWindow = x.DocumentTypeAlias == StaticValues.DocumentTypes.Redirect
                });
        }
    }
}