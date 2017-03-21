using System.Collections.Generic;

namespace Jaywing.Ditto.Shared.Models
{
    public class TreeNode
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
        public bool NewWindow { get; set; }
        public bool HasTemplate { get; set; }
        public bool Current { get; set; }
        public virtual IEnumerable<TreeNode> Children { get; set; }
        
        public string IsCurrent(string classAttribute) => Current ? classAttribute : string.Empty;
        public string NewWindowAttribute => NewWindow ? " target='_blank'" : string.Empty;
    }
}