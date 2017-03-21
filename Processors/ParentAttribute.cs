using Our.Umbraco.Ditto;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace Jaywing.Ditto.Processors
{
    public class ParentAttribute : DittoProcessorAttribute
    {
        public override object ProcessValue()
        {
            IPublishedContent parent = Context.Content.Parent;
            if (parent == null) return null;

            while (parent.TemplateId <= 0 || !parent.IsVisible())
                parent = parent.Parent;

            return parent; 
        }
    }
}