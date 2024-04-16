using AutoMapper;
using Sevices.Models;

namespace Sevices
{
    public class MappingConfig
    {

        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<EmployeeDetails, Tblemployee>();
                config.CreateMap<Tblemployee,EmployeeDetails>();
            });
            return mappingConfig;
        }
    }
}
