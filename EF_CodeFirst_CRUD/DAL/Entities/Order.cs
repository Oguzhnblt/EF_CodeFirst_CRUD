using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_CodeFirst_CRUD.DAL.Entities
{
    /// <summary>
    /// Sipariş Tablosu
    /// </summary>
    public class Order
    {
        public Order()
        {
            this.OrderDetails = new List<OrderDetail>();
        }
        public int OrderId { get; set; }
        public string? OrderCode { get; set; }
        public DateTime? OrderDate { get; set; }
        public int? CustomerId { get; set; }
        public Customer Customer { get; set; }

        public virtual List<OrderDetail> OrderDetails { get; set; }
    }
}
