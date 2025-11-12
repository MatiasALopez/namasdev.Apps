using AutoMapper;

using namasdev.Apps.Entidades;
using namasdev.Apps.Web.Portal.ViewModels.EntidadesPropiedades;

namespace namasdev.Apps.Web.Portal.Automapper
{
    public class PropiedadTipoEspecificacionesProfile : Profile
    {
        public PropiedadTipoEspecificacionesProfile()
        {
            CreateMap<PropiedadTipoEspecificacionesTextoViewModel, PropiedadTipoEspecificacionesTexto>()
                .ReverseMap();

            CreateMap<PropiedadTipoEspecificacionesEnteroViewModel, PropiedadTipoEspecificacionesEntero>()
                .ReverseMap();

            CreateMap<PropiedadTipoEspecificacionesEnteroViewModel, PropiedadTipoEspecificacionesEnteroCorto>()
                .ReverseMap();

            CreateMap<PropiedadTipoEspecificacionesEnteroViewModel, PropiedadTipoEspecificacionesEnteroLargo>()
                .ReverseMap();

            CreateMap<PropiedadTipoEspecificacionesDecimalViewModel, PropiedadTipoEspecificacionesDecimal>()
                .ReverseMap();

            CreateMap<PropiedadTipoEspecificacionesDecimalViewModel, PropiedadTipoEspecificacionesDecimalFlotante>()
                .ReverseMap();

            CreateMap<PropiedadTipoEspecificacionesImporteViewModel, PropiedadTipoEspecificacionesImporte>()
                .ReverseMap();
        }
    }
}
