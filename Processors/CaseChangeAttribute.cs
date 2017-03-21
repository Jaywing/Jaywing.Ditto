using Our.Umbraco.Ditto;

namespace Jaywing.Ditto.Processors
{
    public class CaseChangeAttribute : DittoProcessorAttribute
    {
        private Case TextCase { get; set; }

        public CaseChangeAttribute(Case textCase)
        {
            TextCase = textCase;
        }

        public override object ProcessValue()
        {
            string strVal = Value.ToString();
            return TextCase == Case.Lowercase ? strVal.ToLower() : strVal.ToUpper();
        }

        public enum Case
        {
            Uppercase,
            Lowercase
        }
    }
}