using System.Linq;
using Our.Umbraco.Ditto;
using Umbraco.Web;

using StaticValues = Jaywing.Ditto.Shared.StaticValues;

namespace Jaywing.Ditto.Processors
{
    public class ContactUsAttribute : DittoProcessorAttribute
    {
        public override object ProcessValue()
        {
            return Context.Content
                .AncestorOrSelf(StaticValues.DocumentTypes.Homepage)?
                .Children
                .FirstOrDefault(x => x.DocumentTypeAlias == StaticValues.DocumentTypes.ContactUs);
        }
    }
}