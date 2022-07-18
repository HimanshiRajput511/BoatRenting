using ModelLayer;
using System.Collections.Generic;

namespace BusinessLayer
{
    public interface IBusiness
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
