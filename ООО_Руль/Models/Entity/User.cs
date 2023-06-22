using System;
using System.Collections.Generic;

namespace ООО_Руль.Models.Entity
{
    public partial class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string UserSurname { get; set; } = null!;
        public string UserPatronomyc { get; set; } = null!;
        public string UserPassword { get; set; } = null!;
        public string UserLogin { get; set; } = null!;
        public int RoleId { get; set; }

        public virtual UserRole Role { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; }
    }
}
