using System.Web;
using Our.Umbraco.Ditto;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace Jaywing.Ditto.Processors
{
    public class UrlAbsoluteEncodedAttribute : DittoProcessorAttribute
    {
        public override object ProcessValue()
        {
            var content = Value as IPublishedContent;
            return HttpUtility.UrlEncode(content?.UrlAbsolute());
        }
    }
}