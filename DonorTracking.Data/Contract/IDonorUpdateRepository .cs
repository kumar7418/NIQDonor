using System.Collections.Generic;

namespace DonorTracking.Data
{
    public interface IDonorUpdateRepository
    {
        //IRepositoryConfigurationProvider config;
        string CreateApplication(Application donor);
        //{
        //    DonorUpdateRepository _donor = new DonorUpdateRepository();
        //    return _donor.CreateApplication(donor);
        //}


       List<string> CreateMilkKits(MilkKits _MilkKits);
        //{
        //    DonorUpdateRepository _donor = new DonorUpdateRepository();
        //    return _donor.CreateMilkKits(_MilkKits);
        //}
        
    }
}