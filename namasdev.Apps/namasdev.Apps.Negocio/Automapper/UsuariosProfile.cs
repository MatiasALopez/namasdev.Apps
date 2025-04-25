using AutoMapper;
using namasdev.Apps.Entidades;
using namasdev.Apps.Negocio.DTO.Usuarios;

namespace namasdev.Apps.Negocio.Automapper
{
    public class UsuariosProfile : Profile
    {
        public UsuariosProfile()
        {
            CreateMap<AgregarParametros, Usuario>();
            CreateMap<ActualizarParametros, Usuario>();
            CreateMap<MarcarComoBorradoParametros, Usuario>();
            CreateMap<DesmarcarComoBorradoParametros, Usuario>();
        }
    }
}
