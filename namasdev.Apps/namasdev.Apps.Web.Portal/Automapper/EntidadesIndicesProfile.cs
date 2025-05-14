using System.Linq;

using AutoMapper;

using namasdev.Apps.Entidades;
using namasdev.Apps.Negocio.DTO.EntidadesIndices;
using namasdev.Apps.Web.Portal.Models.EntidadesIndices;
using namasdev.Apps.Web.Portal.ViewModels.EntidadesIndices;

namespace namasdev.Apps.Web.Portal.Automapper
{
    public class EntidadIndiceProfile : Profile
    {
        public EntidadIndiceProfile()
        {
            CreateMap<EntidadIndice, EntidadIndiceViewModel>()
                .ForMember(m => m.Propiedades, opt => opt.Ignore());

            CreateMap<EntidadIndiceViewModel, AgregarParametros>()
                .ForMember(m => m.EntidadesPropiedadesIds, opt => opt.MapFrom(vm => vm.PropiedadesIdsSeleccionadas));

            CreateMap<EntidadIndiceViewModel, ActualizarParametros>()
                .ForMember(m => m.EntidadesPropiedadesIds, opt => opt.MapFrom(vm => vm.PropiedadesIdsSeleccionadas));

            CreateMap<EntidadIndice, EntidadIndiceItemModel>();
        }
    }
}
