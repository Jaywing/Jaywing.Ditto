using Jaywing.Ditto.Extensions;
using Our.Umbraco.Ditto;


namespace Jaywing.Ditto.Processors
{
    public class RemoveSpacesAttribute : DittoProcessorAttribute
    {
        public override object ProcessValue()
        {
            return Value.ToString().RemoveSpaces(); 
        }
    }
}