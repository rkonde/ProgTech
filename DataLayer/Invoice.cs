using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    public class Invoice
    {
        public ICollection<Product> ListOfGoods { get; set; }
    }
}
