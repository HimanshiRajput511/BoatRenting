using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer
{
    public class DataService : IData
    {
        private readonly ApplicationDbContext _dbContext;
        public DataService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<BoatRegister> BoatList()
        {
            try
            {
                return _dbContext.BoatRegister.ToList();
            }
            catch(Exception)
            {
                throw;
            }
        }
        public BoatRegister BoatRegister(BoatRegister boatRegister)
        {
            try { 
            boatRegister.IsReturned = true;
            boatRegister.CreatedDate = DateTime.Now;
            boatRegister.ModifiedDate = null;
            boatRegister.IsActive = true;
            Random generator = new Random();
            boatRegister.BoatNumber = Convert.ToInt32(generator.Next(0, 10000).ToString("D4"));
            return boatRegister;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<RentingBusiness> RentedBoatsList()
        {
            try
            {
                return _dbContext.RentingBusiness.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public RentingBusiness RentOutBoats(int Id)
        {
            try { 
            BoatRegister boatRegister = _dbContext.BoatRegister.Where(x => x.BoatId == Id).FirstOrDefault();
            RentingBusiness rentingBusiness = new RentingBusiness()
            {
                BoatNumber = boatRegister.BoatNumber,
                HourlyRate = boatRegister.HourlyRate
            };
            return rentingBusiness;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public RentingBusiness RentOutBoats(RentingBusiness rentingBusiness)
        {
            try
            {
                rentingBusiness.CreatedDate = DateTime.Now;
                rentingBusiness.ModifiedDate = null;
                rentingBusiness.RentOutDate = DateTime.Now;
                rentingBusiness.ReturnedDate = null;
                rentingBusiness.TotalTime = null;
                return rentingBusiness;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public BoatRegister UpdateBoatData(int boatNumber)
        {
            try { 
            BoatRegister boatRegister = _dbContext.BoatRegister.Where(x => x.BoatNumber == boatNumber).FirstOrDefault();
            boatRegister.CreatedDate = boatRegister.CreatedDate;
            boatRegister.ModifiedDate = DateTime.Now;
            boatRegister.IsRented = true;
            boatRegister.IsReturned = false;
            return boatRegister;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public RentingBusiness ReturnedBoatsList(int Id)
        {
            try { 
            RentingBusiness rentingBusiness = _dbContext.RentingBusiness.Where(x => x.RentId == Id).FirstOrDefault();
            rentingBusiness.ModifiedDate = DateTime.Now;
            rentingBusiness.ReturnedDate = DateTime.Now;
            rentingBusiness.TotalTime = rentingBusiness.ReturnedDate - rentingBusiness.RentOutDate;
            TimeSpan timeSpan = TimeSpan.Parse(rentingBusiness.TotalTime.ToString());
            int hours = (int)timeSpan.TotalHours;
            int minutes = timeSpan.Minutes;
            rentingBusiness.Price = hours * rentingBusiness.HourlyRate + minutes * (rentingBusiness.HourlyRate / 60);
            return rentingBusiness;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public BoatRegister UpdateBoat(int boatNumber)
        {
            try { 
            BoatRegister boatRegister = _dbContext.BoatRegister.Where(x => x.BoatNumber == boatNumber).FirstOrDefault();
            boatRegister.CreatedDate = boatRegister.CreatedDate;
            boatRegister.ModifiedDate = DateTime.Now;
            boatRegister.IsRented = false;
            boatRegister.IsReturned = true;
            return boatRegister;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public BoatRegister DeleteBoat(int Id)
        {
            try
            {
                BoatRegister boatRegister = _dbContext.BoatRegister.Where(x => x.BoatId == Id).FirstOrDefault();
                _dbContext.BoatRegister.Remove(boatRegister);
                _dbContext.SaveChanges();
                return boatRegister;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
