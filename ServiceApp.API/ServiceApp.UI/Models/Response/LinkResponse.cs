using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceApp.UI.Models.Response
{
    public class LinkResponse
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string StoreUrl { get; set; }
        public string ImageUrl { get; set; }
    }
}
