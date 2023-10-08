﻿using System.ComponentModel.DataAnnotations;

namespace CateringOrderWebApplication.Models.DomainModels
{
    public class BlogPost
    {
        [Key]
        public Guid Id { get; set; }

        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string Contet { get; set; }
        public string ShortDescription { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Author { get; set; }
        public bool IsVisible { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
}