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
        /// <summary>
        /// This method shows the list of all registered boats.
        /// </summary>
        /// <returns></returns>
        public IActionResult BoatList()
        {
            try
            {
                ViewBag.Message = TempData["Message"];
                _logger.LogInformation("shows the list of all registered boats!!!");
                return View(_business.BoatList());
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Get method to register boat.
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Post method to submit boat details and save it in database.
        /// </summary>
        /// <param name="boatRegister"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// To list all the boats available for rent
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// To list all the boats which are on rent.
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// To rent out the boat based on boat Id.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
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
        /// <summary>
        /// To save customer name who takes the boat on rent.
        /// </summary>
        /// <param name="rentingBusiness"></param>
        /// <returns></returns>
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
        /// <summary>
        /// To upadte the time and cost for having a boat on rent. 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
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
        /// <summary>
        /// To check if boat name already exists in database.
        /// </summary>
        /// <param name="BoatName"></param>
        /// <returns></returns>
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
        /// <summary>
        /// To permanently delete the record from the database based on id.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public IActionResult DeleteBoat(int Id)
        {
            BoatRegister boatRegister = _business.DeleteBoat(Id);
            TempData["Message"] = boatRegister.BoatNumber + " is deleted successfully";
            return RedirectToAction("BoatList");
        }
    }
}
