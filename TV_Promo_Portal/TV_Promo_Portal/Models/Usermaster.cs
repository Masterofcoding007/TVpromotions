using System;
using System.Collections.Generic;

#nullable disable

namespace TV_Promo_Portal.Models
{
    public partial class Usermaster
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int UsertypeId { get; set; }

        public virtual Usertype Usertype { get; set; }
    }
}
