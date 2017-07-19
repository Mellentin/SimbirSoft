using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimbirSoft.Models
{
    public class IndexViewModel
    {
        public IEnumerable<Address> Addressess { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public FilterViewModel FilterViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
    }
}