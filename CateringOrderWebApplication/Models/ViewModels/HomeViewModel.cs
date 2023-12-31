﻿using CateringOrderWebApplication.Models.DomainModels.BlogPosts;
using CateringOrderWebApplication.Models.DomainModels.Caterings;
using CateringOrderWebApplication.Models.DomainModels.Tags;

namespace CateringOrderWebApplication.Models.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<BlogPost> BlogPosts { get; set; }
        public IEnumerable<Catering> Caterings { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
    }
}