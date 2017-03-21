using System.Collections.Generic;
using Jaywing.Ditto.Extensions;
using Our.Umbraco.Ditto;
using Umbraco.Core;
using Umbraco.Core.Models;

namespace Jaywing.Ditto.Processors
{
    public class DelimitedStringAttribute : DittoProcessorAttribute
    {
        private string Delimiter { get; set; }

        public DelimitedStringAttribute(string delimiter = ",")
        {
            Delimiter = delimiter;
        }

        public override object ProcessValue()
        {
            var content = Value as IPublishedContent;
            if (content == null) return false;

            var value = content.Get<string>(Context.PropertyDescriptor?.Name ?? string.Empty);

            if (string.IsNullOrWhiteSpace(value))
                return null;

            return value.Contains(Delimiter) ? value.ToDelimitedList(Delimiter) :  new List<string> { value };
        }
    }
}