using Our.Umbraco.Ditto;
using Umbraco.Web;

using StaticValues = Jaywing.Ditto.Shared.StaticValues;

namespace Jaywing.Ditto.Processors
{
    public class SearchAttribute : DittoProcessorAttribute
    {
        public override object ProcessValue()
        {
            return Context
                .Content
                .AncestorOrSelf(StaticValues.DocumentTypes.Homepage)?
                .FirstChild(x => x.DocumentTypeAlias == StaticValues.DocumentTypes.Search);
        }
    }
}