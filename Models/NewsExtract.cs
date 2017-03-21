using System;
using Jaywing.Ditto.Processors;
using Jaywing.Ditto.Processors.MultiProcessors;
using Our.Umbraco.Ditto;
using StaticValues = Jaywing.Ditto.Shared.StaticValues;

namespace Jaywing.Ditto.Models
{
    public class NewsExtract
    {
        [Title]
        public string Title { get; set; }

        [Extract]
        public string Extract { get; set; }

        public string Author { get; set; }

        [PublishDate]
        public DateTime PublishDate { get; set; }

        [UmbracoPropertyAndPicker]
        public virtual Image ListingImage { get; set; }

        public string Url { get; set; }

        [UmbracoDictionary(StaticValues.Dictionary.News.LoadingLabel)]
        public string LoadingLabel { get; set; }

        public bool HasAuthor => !string.IsNullOrEmpty(Author);
        public bool HasPublishDate => PublishDate != DateTime.MinValue;
        public bool HasMeta => HasAuthor || HasPublishDate;

        public bool HasImage => ListingImage != null;
    }
}