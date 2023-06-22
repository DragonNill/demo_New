using System;
using System.Collections.Generic;

namespace ООО_Руль.Models.Entity
{
    public partial class Order
    {
        public Order()
        {
            OrderProducts = new HashSet<OrderProduct>();
        }

        public int Id { get; set; }
        public DateOnly OrderDate { get; set; }
        public DateOnly DeliveryDate { get; set; }
        public int? UserId { get; set; }
        public int PickuppointId { get; set; }
        public int PickupCode { get; set; }

        public virtual PickupPoint Pickuppoint { get; set; } = null!;
        public virtual User? User { get; set; }
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
