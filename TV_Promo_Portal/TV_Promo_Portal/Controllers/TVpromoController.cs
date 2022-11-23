using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TV_Promo_Portal.Models;
using TV_Promo_Portal.Repository;

namespace TV_Promo_Portal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TVpromoController : ControllerBase
    {
        //TV_PromoContext db = new TV_PromoContext();
        //[HttpPost]
        //[Route("tvpromo")]
        //public IActionResult Main(TvPromo tvpromo)
        //{
        //    IActionResult response = Unauthorized();
        //    db.TvPromos.Add(tvpromo);
        //    db.SaveChanges();
        //    return response = Ok(new { text = "success" });
        //}

        private readonly IWebHostEnvironment _env;
        private readonly ITvPromoRepository _tvPromo;
        public TVpromoController(ITvPromoRepository tvPromo)
        {
            _tvPromo = tvPromo;
            //throw new ArgumentNullException(nameof(tvPromo));
        }


        [HttpGet]
        [Route("GetTvPromo")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _tvPromo.GetTvPromos());
        }


        [HttpGet]
        [Route("GetTvPromoById/{PromoId}")]
        public async Task<IActionResult> GetTvPromoByPromoCode(int PromoId)
        {
            return Ok(await _tvPromo.GetTvPromoByPromoCode(PromoId));
        }


        [HttpPost]
        [Route("AddTvPromo")]
        public async Task<IActionResult> Post(TvPromo tvPromo)
        {
            var result = await _tvPromo.InsertTvPromo(tvPromo);
            if (result.PromoId == 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
            //return CreatedAtAction(nameof(GetTvPromoByPromoCode), new { id = result.PromoId }, result);
            return Ok("Added Successfully");
        }


        [HttpPut]
        [Route("UpdateTvPromo")]
        public async Task<IActionResult> Put(TvPromo tvPromo, int PromoId)
        {
            if (PromoId != tvPromo.PromoId)
            {
                return BadRequest();
            }
            await _tvPromo.UpdateTvPromo(tvPromo);
            return Ok("Updated Successfully");
        }


        [HttpDelete]
        [Route("DeleteTvPromo")]
        public JsonResult Delete(int PromoId)
        {
            var result = _tvPromo.DeleteTvPromo(PromoId);
            return new JsonResult("Deleted Successfully");
        }



        [HttpGet]
        [Route("getcountries")]
        public async Task<IActionResult> Getcont()
        {
            return Ok(await _tvPromo.getcountries());
        }


        [HttpGet]
        [Route("GetcountrybyID/{CountryId}")]
        public async Task<IActionResult> GetcontbyID(int contid)
        {
            return Ok(await _tvPromo.GetcountrybyID(contid));
        }

        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    stream.CopyTo(stream);
                }

                return new JsonResult(filename);
            }
            catch (Exception)
            {
                return new JsonResult("anonymous.png");
            }
        }
    }
}
