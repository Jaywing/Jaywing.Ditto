using Jaywing.Ditto.Shared.Models;
using Our.Umbraco.Ditto;
using Umbraco.Core.Models;
using Umbraco.Web;
using System.Linq;
using Jaywing.Ditto.Extensions;
using StaticValues = Jaywing.Ditto.Shared.StaticValues;

namespace Jaywing.Ditto.Processors
{
    public class BreadcrumbNavigationAttribute : DittoProcessorAttribute
    {
        public override object ProcessValue()
        {
            IPublishedContent currentPage = Context.Content;

            return currentPage?
                .AncestorsOrSelf()?
                .Where(x => x.DocumentTypeAlias != StaticValues.DocumentTypes.GlobalHomepage)
                .Select(x => new TreeNode
                {
                    Id = x.Id,
                    Name = x.GetTitleOrName(StaticValues.Properties.Title),
                    Url = x.TemplateId > 0 ? x.Url : string.Empty,
                    Current = currentPage.Id == x.Id,
                    HasTemplate = x.TemplateId > 0
                })
                .Reverse();
        }
    }
}