using AutoMapper;

using namasdev.Apps.Entidades;
using namasdev.Apps.Negocio.DTO;
using namasdev.Apps.Web.Portal.Models.EntidadesPropiedades;
using namasdev.Apps.Web.Portal.ViewModels.EntidadesPropiedades;

namespace namasdev.Apps.Web.Portal.Automapper
{
    public class EntidadPropiedadesProfile : Profile
    {
        public EntidadPropiedadesProfile()
        {
            CreateMap<EntidadPropiedad,EntidadPropiedadViewModel>()
                .ReverseMap();

            //CreateMap<AgregarParametros,EntidadPropiedadViewModel>()
            //    .ReverseMap();

            //CreateMap<ActualizarParametros,EntidadPropiedadViewModel>()
            //    .ReverseMap();

            CreateMap<EntidadPropiedad, EntidadPropiedadItemModel>();
        }
    }
}
