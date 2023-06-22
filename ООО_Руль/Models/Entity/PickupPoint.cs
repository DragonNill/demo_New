using System;
using System.Collections.Generic;

namespace ООО_Руль.Models.Entity
{
    public partial class PickupPoint
    {
        public PickupPoint()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public int CityId { get; set; }
        public int StreetId { get; set; }
        public int House { get; set; }

        public virtual City City { get; set; } = null!;
        public virtual Street Street { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; }
    }
}
