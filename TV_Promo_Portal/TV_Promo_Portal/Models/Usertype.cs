using System;
using System.Collections.Generic;

#nullable disable

namespace TV_Promo_Portal.Models
{
    public partial class Usertype
    {
        public Usertype()
        {
            Usermasters = new HashSet<Usermaster>();
        }

        public int UserTypeId { get; set; }
        public string UserType1 { get; set; }

        public virtual ICollection<Usermaster> Usermasters { get; set; }
    }
}
