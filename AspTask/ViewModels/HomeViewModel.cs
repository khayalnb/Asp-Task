using AspTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspTask.ViewModels
{
    public class HomeViewModel
    {
        public List<Slide> Slides { get; set; }
       
        public List<Category> Categories{ get; set; }

        public Introduction Introduction  { get; set; }
        
    }
}
