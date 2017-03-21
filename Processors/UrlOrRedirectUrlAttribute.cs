using Jaywing.Ditto.Extensions;
using Our.Umbraco.Ditto;
using Umbraco.Core.Models;

using StaticValues = Jaywing.Ditto.Shared.StaticValues;

namespace Jaywing.Ditto.Processors
{
    public class UrlOrRedirectUrlAttribute : DittoProcessorAttribute
    {
        public override object ProcessValue()
        {
            var content = Value as IPublishedContent;
            return content.GetUrl(StaticValues.DocumentTypes.Redirect, StaticValues.Properties.RedirectUrl);
        }
    }
}