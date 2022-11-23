using System;
using System.Collections.Generic;

#nullable disable

namespace TV_Promo_Portal.Models
{
    public partial class Country
    {
        public Country()
        {
            States = new HashSet<State>();
        }

        public int CountryId { get; set; }
        public string ShortName { get; set; }
        public string CountryName { get; set; }
        public int PhoneCode { get; set; }

        public virtual ICollection<State> States { get; set; }
    }
}
