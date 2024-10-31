using System;
using System.Collections.Generic;
using System.Text;

namespace eProdaja.Modeli
{
    public class PagedResults<T>
    {
        public int? count { get; set; }
        public IList<T> ResultsList { get; set; } 
    }
}
