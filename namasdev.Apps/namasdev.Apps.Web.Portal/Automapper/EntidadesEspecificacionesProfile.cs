using System.Linq;

using AutoMapper;

using namasdev.Apps.Entidades;
using namasdev.Apps.Negocio.DTO.EntidadesEspecificaciones;
using namasdev.Apps.Web.Portal.ViewModels.EntidadesEspecificaciones;

namespace namasdev.Apps.Web.Portal.Automapper
{
    public class EntidadesEspecificacionesProfile : Profile
    {
        public EntidadesEspecificacionesProfile()
        {
            CreateMap<EntidadEspecificaciones, EntidadEspecificacionesViewModel>();
            CreateMap<EntidadEspecificacionesViewModel, ActualizarParametros>();
        }
    }
}
