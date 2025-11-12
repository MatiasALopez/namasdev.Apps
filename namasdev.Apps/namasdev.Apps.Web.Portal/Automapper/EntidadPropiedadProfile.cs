using System.Linq;

using AutoMapper;

using namasdev.Apps.Entidades;
using namasdev.Apps.Negocio.DTO.EntidadesPropiedades;
using namasdev.Apps.Web.Portal.Models.EntidadesPropiedades;
using namasdev.Apps.Web.Portal.ViewModels.EntidadesPropiedades;

namespace namasdev.Apps.Web.Portal.Automapper
{
    public class EntidadPropiedadProfile : Profile
    {
        public EntidadPropiedadProfile()
        {
            CreateMap<EntidadPropiedad, EntidadPropiedadViewModel>()
                .ForMember(m => m.EspecificacionesEntero, opt => opt.MapFrom(m => (IPropiedadTipoEspecificaciones)m.EspecificacionesEntero ?? (IPropiedadTipoEspecificaciones)m.EspecificacionesEnteroCorto ?? (IPropiedadTipoEspecificaciones)m.EspecificacionesEnteroLargo))
                .ForMember(m => m.EspecificacionesDecimal, opt => opt.MapFrom(m => (IPropiedadTipoEspecificaciones)m.EspecificacionesDecimal ?? (IPropiedadTipoEspecificaciones)m.EspecificacionesDecimalFlotante))
                .ForMember(m => m.EspecificacionesImporte, opt => opt.MapFrom(m => (IPropiedadTipoEspecificaciones)m.EspecificacionesImporte ?? (IPropiedadTipoEspecificaciones)m.EspecificacionesImporte))
                .ReverseMap();

            CreateMap<AgregarParametros, EntidadPropiedadViewModel>()
                .ReverseMap();

            CreateMap<ActualizarParametros, EntidadPropiedadViewModel>()
                .ReverseMap();

            CreateMap<EntidadPropiedad, EntidadPropiedadItemModel>()
                .ForMember(m => m.EsClave, opt => opt.MapFrom(ep => ep.Entidad.Claves.Any(c => c.EntidadPropiedadId == ep.Id)));
        }
    }
}
