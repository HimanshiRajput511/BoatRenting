using DataLayer;
using ModelLayer;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class BusinessService : IBusiness
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IData _data;
        public BusinessService(ApplicationDbContext dbContext, IData data)
        {
            _dbContext = dbContext;
            _data = data;
        }
        public IEnumerable<BoatRegister> BoatList()
        {
            return _data.BoatList();
        }
        public BoatRegister BoatRegister(BoatRegister boatRegister)
        {
            return _data.BoatRegister(boatRegister);
        }
        public IEnumerable<RentingBusiness> RentedBoatsList()
        {
            return _data.RentedBoatsList();
        }
        public RentingBusiness RentOutBoats(int Id)
        {
            return _data.RentOutBoats(Id);
        }
        public RentingBusiness RentOutBoats(RentingBusiness rentingBusiness)
        {
            return _data.RentOutBoats(rentingBusiness);
        }
        public BoatRegister UpdateBoatData(int boatNumber)
        {
            return _data.UpdateBoatData(boatNumber);
        }
        public RentingBusiness ReturnedBoatsList(int Id)
        {
            return _data.ReturnedBoatsList(Id);
        }
        public BoatRegister UpdateBoat(int boatNumber)
        {
            return _data.UpdateBoat(boatNumber);
        }
        public BoatRegister DeleteBoat(int Id)
        {
            return _data.DeleteBoat(Id);
        }
    }
}
