using AutoMapper;
using Infrastructure.Models;
using soporte_tic.Models.ViewModels;

namespace soporte_tic.Utils.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region empresa
            CreateMap<Empresa, VMEmpresa>().ReverseMap();
            #endregion

            #region sucursal
            CreateMap<Sucursal, VMSucursal>().
                ForMember(dest =>
                    dest.EmprCodigo,
                    opt => opt.MapFrom(ori => ori.EmprCodigoNavigation.EmprCodigo)
                );

            CreateMap<VMSucursal, Sucursal>().
                ForMember(dest =>
                    dest.EmprCodigoNavigation,
                    opt => opt.Ignore()
                );
            #endregion
        }
    }
}
