using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Domain
{
    public  class OrderItem : BaseEntity
    {
       public Product Product { get; set; } = new Product();
        public decimal Quantity { get; set; }
    }
}
