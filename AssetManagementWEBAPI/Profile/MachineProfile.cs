using AssetManagementWEBAPI.Entity;
using AssetManagementWEBAPI.Models;
using AutoMapper;

namespace AssetManagementWEBAPI.Profiles
{
    public class MachineProfile:Profile
    {
        public MachineProfile()
        {
            CreateMap<Machine, MachineModel>();
            CreateMap<AssetModel, Asset>();
            CreateMap<MachineModel, Machine>().ForMember(
                dest=>dest.MachineName,
                opt=>opt.MapFrom(src=>src.MachineName)
                ).ForMember(
                dest=>dest.Asset,
                opt=>opt.MapFrom(src=>src.Asset)
                );
 
        }
    }
}
