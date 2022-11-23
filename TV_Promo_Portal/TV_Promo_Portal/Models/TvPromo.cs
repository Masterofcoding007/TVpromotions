using System;
using System.Collections.Generic;

#nullable disable

namespace TV_Promo_Portal.Models
{
    public partial class TvPromo
    {
        public int PromoId { get; set; }
        public int PromoCode { get; set; }
        public DateTime? PromoDate { get; set; }
        public string EventName { get; set; }
        public DateTime? EventDate { get; set; }
        public DateTime? EventTime { get; set; }
        public string VenueName { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Description { get; set; }
        public string TicketUrl { get; set; }
        public int? Status { get; set; }
        public int? CreatedBy { get; set; }

        public virtual PromoStatus StatusNavigation { get; set; }
    }
}
