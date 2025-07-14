using AutoMapper;

using namasdev.Apps.Entidades;
using namasdev.Apps.Negocio.DTO.EntidadesChecks;
using namasdev.Apps.Web.Portal.Models.EntidadesChecks;
using namasdev.Apps.Web.Portal.ViewModels.EntidadesChecks;

namespace namasdev.Apps.Web.Portal.Automapper
{
    public class EntidadesChecksProfile : Profile
    {
        public EntidadesChecksProfile()
        {
            CreateMap<EntidadCheck, EntidadCheckViewModel>();
            CreateMap<EntidadCheckViewModel, AgregarParametros>();
            CreateMap<EntidadCheckViewModel, ActualizarParametros>();
            CreateMap<EntidadCheck, EntidadCheckItemModel>();
        }
    }
}
