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
                    opt => opt.MapFrom(ori => ori.MaquCodigoFkNavigation!.MaquCodigoFkNavigation.MaquNombre)
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

            #region odt
            CreateMap<OrdenTrabajo, VMOrdenTrabajo>().
                ForMember(dest =>
                    dest.MaquCodigoF,
                    opt => opt.MapFrom(ori => ori.MaquCodigoNavigation.MaquCodigoFkNavigation!.MaquCodigo)
                ).
                ForMember(dest =>
                    dest.MaquNombreF,
                    opt => opt.MapFrom(ori => ori.MaquCodigoNavigation.MaquCodigoFkNavigation!.MaquNombre)
                ).
                ForMember(dest =>
                   dest.MaquCodigo,
                   opt => opt.MapFrom(ori => ori.MaquCodigoNavigation.MaquCodigo)
                ).
                ForMember(dest =>
                    dest.MaquNombre,
                    opt => opt.MapFrom(ori => ori.MaquCodigoNavigation.MaquNombre)
                ).
                ForMember(dest =>
                   dest.UsuaResponsable,
                   opt => opt.MapFrom(ori => ori.UsuaResponsableNavigation!.UsuaCodigo)
                ).
                ForMember(dest =>
                    dest.UsuaResponsableNombre,
                    opt => opt.MapFrom(ori => ori.UsuaResponsableNavigation!.UsuaNombre)
                ).
                ForMember(dest =>
                   dest.UsuaRevisa,
                   opt => opt.MapFrom(ori => ori.UsuaRevisaNavigation!.UsuaCodigo)
                ).
                ForMember(dest =>
                    dest.UsuaRevisaNombre,
                    opt => opt.MapFrom(ori => ori.UsuaRevisaNavigation!.UsuaNombre)
                ).
                ForMember(dest =>
                   dest.CodtCodigo,
                   opt => opt.MapFrom(ori => ori.CodtCodigoNavigation.CodtCodigo)
                );

            CreateMap<VMOrdenTrabajo, OrdenTrabajo>().
               ForMember(dest =>
                   dest.MaquCodigoNavigation,
                   opt => opt.Ignore()
               ).
               ForMember(dest =>
                   dest.UsuaResponsableNavigation,
                   opt => opt.Ignore()
               ).
               ForMember(dest =>
                   dest.UsuaRevisaNavigation,
                   opt => opt.Ignore()
               ).
               ForMember(dest =>
                   dest.CodtCodigoNavigation,
                   opt => opt.Ignore()
               );
            #endregion

            #region detalle odt
            CreateMap<DetalleOdt, VMDetalleODT>().
               ForMember(dest =>
                   dest.OrtrCodigo,
                   opt => opt.MapFrom(ori => ori.OrtrCodigoNavigation!.OrtrCodigo)
               ).
               ForMember(dest =>
                   dest.TamaCodigo,
                   opt => opt.MapFrom(ori => ori.TamaCodigoNavigation!.TamaCodigo)
               ).
               ForMember(dest =>
                   dest.TamaNombre,
                   opt => opt.MapFrom(ori => ori.TamaCodigoNavigation!.TamaNombre)
               );

            CreateMap<VMDetalleODT, DetalleOdt>().
                ForMember(dest =>
                    dest.OrtrCodigoNavigation,
                    opt => opt.Ignore()
                ).
                ForMember(dest =>
                    dest.TamaCodigoNavigation,
                    opt => opt.Ignore()
                );
            #endregion

            #region configuración odt
            CreateMap<ConfiguracionOdt, VMConfiguracionODT>().ReverseMap();
            #endregion
        }
    }
}
