using Fiorello.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fiorello.ViewModels
{
    public class HomeViewModel
    {
        public List<Slide> Slides { get; set; }

        public SlideSummary SlideSummary { get; set; }
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }
        public List<Settings> Settings { get; set; }
    }
}
