using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TV_Promo_Portal.Models;

namespace TV_Promo_Portal.Repository
{
    public interface ITvPromoRepository
    {
        Task<IEnumerable<TvPromo>> GetTvPromos();
        Task<TvPromo> GetTvPromoByPromoCode(int PromoID);
        Task<TvPromo> InsertTvPromo(TvPromo objTvPromo);
        Task<TvPromo> UpdateTvPromo(TvPromo objTvPromo);
        Task<TvPromo> DeleteTvPromo(int PromoID);
        Task<IEnumerable<Country>> getcountries();
        Task<Country> GetcountrybyID(int contid);
    }
}
