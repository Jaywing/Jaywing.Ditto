using Our.Umbraco.Ditto;

namespace Jaywing.Ditto.Processors.Contexts
{
    public class PaginationContext : DittoProcessorContext
    {
        public long PageNumber { get; set; }
        public int PageSize { get; set; } = 10;
    }
}