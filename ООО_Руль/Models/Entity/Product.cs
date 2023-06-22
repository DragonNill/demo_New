using System;
using System.Collections.Generic;

namespace ООО_Руль.Models.Entity
{
    public partial class Product
    {
        public Product()
        {
            OrderProducts = new HashSet<OrderProduct>();
        }

        public int Id { get; set; }
        public int ManafacturerId { get; set; }
        public string Description { get; set; } = null!;
        public sbyte Discount { get; set; }
        public int Cost { get; set; }
        public int Amount { get; set; }
        public string Photo { get; set; } = null!;
        public string Name { get; set; } = null!;

        public virtual Manafacturer Manafacturer { get; set; } = null!;
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
