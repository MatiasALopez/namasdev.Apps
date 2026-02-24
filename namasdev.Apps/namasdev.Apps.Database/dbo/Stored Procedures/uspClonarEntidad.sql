CREATE PROCEDURE [dbo].[uspClonarEntidad]
	@EntidadIdOrigen uniqueidentifier,
	@EntidadIdDestino uniqueidentifier,
	@UsuarioId nvarchar(128),
	@FechaHora datetime
AS
BEGIN
	declare @NombreAReemplazar nvarchar(100) = concat('_',(select NombrePlural from dbo.Entidades where EntidadId = @EntidadIdOrigen),'_'),
		@NombreReemplazo nvarchar(100) = concat('_',(select NombrePlural from dbo.Entidades where EntidadId = @EntidadIdDestino),'_')

	if exists(select * from dbo.EntidadesEspecificaciones where EntidadId = @EntidadIdDestino)
	begin
		update eed
		set IdiomaArticuloId = eeo.IdiomaArticuloId,
			IDPropiedadTipoId = eeo.IDPropiedadTipoId,
			EsSoloLectura = eeo.EsSoloLectura,
			BajaTipoId = eeo.BajaTipoId,
			AuditoriaCreado = eeo.AuditoriaCreado,
			AuditoriaUltimaModificacion = eeo.AuditoriaUltimaModificacion
		from 
			dbo.EntidadesEspecificaciones eed
			left join dbo.EntidadesEspecificaciones eeo on eeo.EntidadId = @EntidadIdOrigen
		where 
			eed.EntidadId = @EntidadIdDestino
	end
	else
	begin
		insert into dbo.EntidadesEspecificaciones (EntidadId,IdiomaArticuloId,IDPropiedadTipoId,EsSoloLectura,BajaTipoId,AuditoriaCreado,AuditoriaUltimaModificacion)
			select @EntidadIdDestino,IdiomaArticuloId,IDPropiedadTipoId,EsSoloLectura,BajaTipoId,AuditoriaCreado,AuditoriaUltimaModificacion
			from dbo.EntidadesEspecificaciones 
			where EntidadId = @EntidadIdOrigen
	end

	select EntidadPropiedadId EntidadPropiedadIdOrigen,newid() as EntidadPropiedadId,@EntidadIdDestino as EntidadId,Nombre,Etiqueta,PropiedadTipoId,PropiedadTipoEspecificaciones,PermiteNull,Orden,CalculadaFormula,GeneradaAlCrear,Editable,PropiedadCategoriaId
	into #tmpEntidadesPropiedades
	from dbo.EntidadesPropiedades
	where EntidadId = @EntidadIdOrigen

	insert into dbo.EntidadesPropiedades (EntidadPropiedadId,EntidadId,Nombre,Etiqueta,PropiedadTipoId,PropiedadTipoEspecificaciones,PermiteNull,Orden,CalculadaFormula,GeneradaAlCrear,Editable,PropiedadCategoriaId)
		select EntidadPropiedadId,EntidadId,Nombre,Etiqueta,PropiedadTipoId,PropiedadTipoEspecificaciones,PermiteNull,Orden,CalculadaFormula,GeneradaAlCrear,Editable,PropiedadCategoriaId
		from #tmpEntidadesPropiedades

	insert into dbo.EntidadesAsociaciones (EntidadAsociacionId,Nombre,OrigenEntidadId,OrigenEntidadPropiedadId,OrigenEntidadPropiedadNavegacionNombre,OrigenAsociacionMultiplicidadId,DestinoEntidadId,DestinoEntidadPropiedadId,DestinoEntidadPropiedadNavegacionNombre,DestinoAsociacionMultiplicidadId,TablaAuxiliarNombre,DeleteAsociacionReglaId,UpdateAsociacionReglaId)
		select newid(),replace(ea.Nombre,@NombreAReemplazar,@NombreReemplazo),@EntidadIdDestino,ep.EntidadPropiedadId,OrigenEntidadPropiedadNavegacionNombre,OrigenAsociacionMultiplicidadId,DestinoEntidadId,DestinoEntidadPropiedadId,DestinoEntidadPropiedadNavegacionNombre,DestinoAsociacionMultiplicidadId,TablaAuxiliarNombre,DeleteAsociacionReglaId,UpdateAsociacionReglaId
		from 
			dbo.EntidadesAsociaciones ea
			join #tmpEntidadesPropiedades ep on ea.OrigenEntidadPropiedadId = ep.EntidadPropiedadIdOrigen
		where OrigenEntidadId = @EntidadIdOrigen

	insert into dbo.EntidadesClaves (EntidadClaveId,EntidadId,EntidadPropiedadId)
		select newid(),@EntidadIdDestino,ep.EntidadPropiedadId
		from 
			dbo.EntidadesClaves ec
			join #tmpEntidadesPropiedades ep on ec.EntidadPropiedadId = ep.EntidadPropiedadIdOrigen
		where ec.EntidadId = @EntidadIdOrigen

	select EntidadIndiceId as EntidadIndiceIdOrigen, newid() as EntidadIndiceId,@EntidadIdDestino as EntidadId,replace(Nombre,@NombreAReemplazar,@NombreReemplazo) as Nombre,EsUnique,Condiciones
	into #tmpEntidadesIndices
	from dbo.EntidadesIndices
	where EntidadId = @EntidadIdOrigen

	insert into dbo.EntidadesIndices (EntidadIndiceId,EntidadId,Nombre,EsUnique,Condiciones)
		select EntidadIndiceId,EntidadId,Nombre,EsUnique,Condiciones
		from #tmpEntidadesIndices

	insert into dbo.EntidadesIndicesPropiedades (EntidadIndicePropiedadId,EntidadIndiceId,EntidadPropiedadId)
		select newid(),ei.EntidadIndiceId,ep.EntidadPropiedadId
		from 
			dbo.EntidadesIndicesPropiedades eip
			join #tmpEntidadesIndices ei on eip.EntidadIndiceId = ei.EntidadIndiceIdOrigen
			join #tmpEntidadesPropiedades ep on eip.EntidadPropiedadId = ep.EntidadPropiedadIdOrigen
	
	return 0
END
go
