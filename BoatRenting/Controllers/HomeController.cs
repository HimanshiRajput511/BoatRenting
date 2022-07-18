using BusinessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelLayer;
using System;
using System.Linq;

namespace BoatRenting.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _dbContext;
        private readonly IBusiness _business;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext, IBusiness business)
        {
            _logger = logger;
            _dbContext = dbContext;
            _business = business;
        }

        public IActionResult Index()
        {
            try
            {
                return View();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IActionResult BoatList()
        {
            try
            {
                ViewBag.Message = TempData["Message"];
                return View(_business.BoatList());
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public IActionResult BoatRegister()
        {
            try
            {
                return View();
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public IActionResult BoatRegister(BoatRegister boatRegister)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _dbContext.BoatRegister.Add(_business.BoatRegister(boatRegister));
                    _dbContext.SaveChanges();
                    TempData["Message"] = "Registered Successfully with Boat Number = "+ boatRegister.BoatNumber;
                    return RedirectToAction("BoatList");
                }
                return View(boatRegister);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IActionResult RentingBusiness()
        {
            try
            {
                return View();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IActionResult BoatsForRent()
        {
            try
            {
                ViewBag.Message = TempData["Message"];
                return View(_business.BoatList().Where(x => x.IsReturned == true));
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IActionResult RentedBoatsList()
        {
            try
            {
                ViewBag.Message = TempData["Message"];
                return View(_business.RentedBoatsList());
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public IActionResult RentOutBoats(int Id)
        {
            try
            {
                return View(_business.RentOutBoats(Id));
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public IActionResult RentOutBoats(RentingBusiness rentingBusiness)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _dbContext.RentingBusiness.Add(_business.RentOutBoats(rentingBusiness));
                    _dbContext.BoatRegister.Update(_business.UpdateBoatData(rentingBusiness.BoatNumber));
                    _dbContext.SaveChanges();
                    TempData["Message"] = rentingBusiness.BoatNumber + " is rented out";
                    return RedirectToAction("RentedBoatsList");
                }
                return View(rentingBusiness);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IActionResult ReturnedBoatsList(int Id)
        {
            try
            {
                _dbContext.RentingBusiness.Update(_business.ReturnedBoatsList(Id));
                RentingBusiness rentingBusiness = _business.ReturnedBoatsList(Id);
                _dbContext.BoatRegister.Update(_business.UpdateBoat(rentingBusiness.BoatNumber));
                _dbContext.SaveChanges();
                TempData["Message"] = rentingBusiness.BoatNumber + " is returned back successfully";
                return RedirectToAction("RentedBoatsList");
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IActionResult IsAlreadyExist(string BoatName)
        {
            BoatRegister RegBoatName = _dbContext.BoatRegister.Where(x => x.BoatName == BoatName).FirstOrDefault();
            bool status;
            if (RegBoatName != null)
            {
                //Already registered  
                status = false;
            }
            else
            {
                //Available to use  
                status = true;
            }
            return Json(status);
        }
        public IActionResult DeleteBoat(int Id)
        {
            BoatRegister boatRegister = _business.DeleteBoat(Id);
            TempData["Message"] = boatRegister.BoatNumber + " is deleted successfully";
            return RedirectToAction("BoatList");
        }
    }
}
