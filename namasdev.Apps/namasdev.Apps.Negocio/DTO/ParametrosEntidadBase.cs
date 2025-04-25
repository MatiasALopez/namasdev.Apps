using System;

namespace namasdev.Apps.Negocio.DTO
{
    public class ParametrosEntidadBase<TId> : ParametrosConUsuarioBase
        where TId : IEquatable<TId>
    {
        public TId Id { get; set; }
    }
}
