
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TV_Promo_Portal.Models;

namespace TV_Promo_Portal.Repository
{
    public class TvPromoRepository : ITvPromoRepository
    {
        private readonly TV_PromoContext _appDBContext;
        public TvPromoRepository(TV_PromoContext context)
        {
            _appDBContext = context;
            //throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<TvPromo>> GetTvPromos()
        {
            return await _appDBContext.TvPromos.ToListAsync();
        }
        public async Task<TvPromo> GetTvPromoByPromoCode(int PromoID)
        {
            return await _appDBContext.TvPromos.FindAsync(PromoID);
        }
        public async Task<TvPromo> InsertTvPromo(TvPromo objTvPromo)
        {
            _appDBContext.TvPromos.Add(objTvPromo);
            await _appDBContext.SaveChangesAsync();
            return objTvPromo;
        }

        public async Task<TvPromo> UpdateTvPromo(TvPromo objTvPromo)
        {
            _appDBContext.Entry(objTvPromo).State = EntityState.Modified;
            await _appDBContext.SaveChangesAsync();
            return objTvPromo;
        }
        public async Task<TvPromo> DeleteTvPromo(int PromoID)
        {
            //var PromoToDelete = _appDBContext.TvPromos.FindAsync(PromoID);
            //var PromoToDelete = await GetTvPromoByPromoCode(PromoID); 
            var PromoToDelete = await _appDBContext.TvPromos.FindAsync(PromoID);
            if (PromoToDelete != null)
            {
                _appDBContext.TvPromos.Remove(PromoToDelete);
                //_appDBContext.Entry(PromoToDelete).State = EntityState.Deleted;
                await _appDBContext.SaveChangesAsync();
            }
            return null;
        }

        public async Task<IEnumerable<Country>> getcountries()
        {
            return await _appDBContext.Countries.ToListAsync();
        }
        public async Task<Country> GetcountrybyID(int contid)
        {
            return await _appDBContext.Countries.FindAsync(contid);
        }
    }
}
