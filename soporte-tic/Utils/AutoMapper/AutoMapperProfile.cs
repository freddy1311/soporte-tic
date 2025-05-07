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

            #region usuario 
            CreateMap<Usuario, VMUsuario>().
                ForMember(dest =>
                    dest.SucuCodigo,
                    opt => opt.MapFrom(ori => ori.SucuCodigoNavigation.SucuCodigo)
                );

            CreateMap<VMUsuario, Usuario>().
                ForMember(dest =>
                    dest.SucuCodigoNavigation,
                    opt => opt.Ignore()
                );
            #endregion

            #region maquinarias
            CreateMap<Maquinaria, VMMaquinaria>().
                ForMember(dest =>
                    dest.MaquCodigoFK,
                    opt => opt.MapFrom(ori => ori.MaquCodigoFkNavigation!.MaquCodigo)
                ).
                ForMember(dest =>
                    dest.MaquNombreFK,
                    opt => opt.MapFrom(ori => ori.MaquCodigoFkNavigation!.MaquNombre)
                );

            CreateMap<VMMaquinaria, Maquinaria>().
                ForMember(dest =>
                    dest.MaquCodigoFkNavigation,
                    opt => opt.Ignore()
                );
            #endregion

            #region tareas maquinaria
            CreateMap<TareasMaquinaria, VMMaquinariaTarea>().
               ForMember(dest =>
                   dest.MaquCodigo,
                   opt => opt.MapFrom(ori => ori.MaquCodigoNavigation.MaquCodigo)
               ).
               ForMember(dest =>
                   dest.MaquNombre,
                   opt => opt.MapFrom(ori => ori.MaquCodigoNavigation.MaquNombre)
               );

            CreateMap<VMMaquinariaTarea, TareasMaquinaria>().
                ForMember(dest =>
                    dest.MaquCodigoNavigation,
                    opt => opt.Ignore()
                );
            #endregion
        }
    }
}
