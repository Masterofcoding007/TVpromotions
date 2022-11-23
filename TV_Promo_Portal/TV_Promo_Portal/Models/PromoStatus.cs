using System;
using System.Collections.Generic;

#nullable disable

namespace TV_Promo_Portal.Models
{
    public partial class PromoStatus
    {
        public PromoStatus()
        {
            TvPromos = new HashSet<TvPromo>();
        }

        public int StatusId { get; set; }
        public string StatusType { get; set; }

        public virtual ICollection<TvPromo> TvPromos { get; set; }
    }
}
