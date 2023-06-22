using System;
using System.Collections.Generic;

namespace ООО_Руль.Models.Entity
{
    public partial class Street
    {
        public Street()
        {
            PickupPoints = new HashSet<PickupPoint>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<PickupPoint> PickupPoints { get; set; }
    }
}
