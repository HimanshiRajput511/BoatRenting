using ModelLayer;
using System.Collections.Generic;

namespace DataLayer
{
    public interface IData
    {
        IEnumerable<BoatRegister> BoatList();
        BoatRegister BoatRegister(BoatRegister boatRegister);
        IEnumerable<RentingBusiness> RentedBoatsList();
        RentingBusiness RentOutBoats(int Id);
        RentingBusiness RentOutBoats(RentingBusiness rentingBusiness);
        BoatRegister UpdateBoatData(int boatNumber);
        RentingBusiness ReturnedBoatsList(int Id);
        BoatRegister UpdateBoat(int boatNumber);
        BoatRegister DeleteBoat(int Id);
    }
}
