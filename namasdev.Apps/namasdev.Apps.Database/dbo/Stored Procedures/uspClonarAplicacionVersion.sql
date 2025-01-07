CREATE PROCEDURE [dbo].[uspClonarAplicacionVersion]
	@AplicacionVersionIdOrigen uniqueidentifier,
	@AplicacionVersionIdDestino uniqueidentifier,
	@UsuarioId nvarchar(128),
	@FechaHora datetime
AS
BEGIN
	insert into dbo.Entidades (EntidadId,AplicacionVersionId,Nombre,NombrePlural,Etiqueta,EtiquetaPlural,CreadoPor,CreadoFecha,UltimaModificacionPor,UltimaModificacionFecha)
		select newid(),@AplicacionVersionIdDestino,Nombre,NombrePlural,Etiqueta,EtiquetaPlural,@UsuarioId,@FechaHora,@UsuarioId,@FechaHora
		from dbo.Entidades 
		where AplicacionVersionId = @AplicacionVersionIdOrigen

	insert into dbo.EntidadesPropiedades (EntidadPropiedadId,EntidadId,Nombre,Etiqueta,PropiedadTipoId,PropiedadTipoEspecificaciones,PermiteNull,Orden,CalculadaFormula,GeneradaAlCrear,Editable,CreadoPor,CreadoFecha,UltimaModificacionPor,UltimaModificacionFecha)
		select 
			newid(),ed.EntidadId,eop.Nombre,eop.Etiqueta,eop.PropiedadTipoId,eop.PropiedadTipoEspecificaciones,eop.PermiteNull,eop.Orden,eop.CalculadaFormula,eop.GeneradaAlCrear,eop.Editable,@UsuarioId,@FechaHora,@UsuarioId,@FechaHora
		from 
			dbo.Entidades eo 
			join dbo.Entidades ed on eo.Nombre = ed.Nombre
			join dbo.EntidadesPropiedades eop on eo.EntidadId = eop.EntidadId
		where 
			eo.AplicacionVersionId = @AplicacionVersionIdOrigen
			and ed.AplicacionVersionId = @AplicacionVersionIdDestino
	
	return 0
END
go
