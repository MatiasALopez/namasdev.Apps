--====
insert into dbo.Parametros (nombre, valor) values
('ErroresMensajeDefault','Ha ocurrido un error. Por favor, intente nuevamente mas tarde.'),
('SitioUrl', 'https://namasdev-admin.azurewebsites.net'),
('CloudStorageAccountConnectionString', 'UseDevelopmentStorage=true'),
('ServidorCorreosHost', 'smtp.sendgrid.net'),
('ServidorCorreosPuerto', null),
('ServidorCorreosHabilitarSSL', null),
('ServidorCorreosCredencialesPassword', 'XXXX'),
('ServidorCorreosCredencialesUsuario', 'XXXX'),
('ServidorCorreosRemitente', 'namasdev Admin <info-adm@namasdev.ccc>'),
('ServidorCorreosCopiaOculta', null),
('ServidorCorreos','{"Host":"smtp.host.ccc","Puerto":487,"Credenciales":{"UserName":"user","Password":"pwd"},"HabilitarSsl":true,"Remitente":"sender@mail.ccc","CopiaOculta":"bcc1@mail.ccc","Headers":{{"Header1","Value1"},{"Header2","Value2"}}}')
go
--====


--====
insert into dbo.CorreosParametros (Id, Asunto, Contenido) values
(1,'Activa tu cuenta','<p>Hola {{NombreYApellido}},</p><p>Para activar tu cuenta haz clic <a href="{{ActivarCuentaUrl}}">aquí</a> o accede al siguiente enlace en tu navegador:<br/>{{ActivarCuentaUrl}}</p><p>Este link será válido por 24 horas.</p>'),
(2,'Restablece tu password','<p>Hola {{NombreYApellido}},</p><p>Para restablecer tu contraseña haz clic <a href="{{RestablecerPasswordUrl}}">aquí</a> o accede al siguiente enlace en tu navegador:<br/>{{RestablecerPasswordUrl}}</p><p>Este link será válido por 24 horas.</p>')
go
--====


--====
insert into dbo.AspNetRoles (Id,Name) values
('1212CE75-E3CA-4838-B083-DCB5753CB657','Administrador')
go
--====

--====
insert into dbo.AuditoriaTipos (AuditoriaTipoId,Nombre) values
(1,'Creación'),
(2,'Modificación'),
(3,'Borrado')
go
--====

--====
insert into dbo.Idiomas (IdiomaId,Nombre,UsaArticulos) values
('es','Español',1),
('en','Inglés',0)
go
--====

--====
insert into dbo.IdiomasArticulos (IdiomaArticuloId,IdiomaId,Nombre) values
('es1','es','El'),
('es2','es','La'),
('es3','es','Los'),
('es4','es','Las')
go
--====

--====
insert into dbo.IdiomasTextos (IdiomaId,CreadoPorNombre,CreadoPorEtiqueta,CreadoFechaNombre,CreadoFechaEtiqueta,UltimaModificacionPorNombre,UltimaModificacionPorEtiqueta,UltimaModificacionFechaNombre,UltimaModificacionFechaEtiqueta,BorradoPorNombre,BorradoPorEtiqueta,BorradoFechaNombre,BorradoFechaEtiqueta,BorradoNombre,BorradoEtiqueta,BDNombre,EntidadesNombre,DatosNombre,NegocioNombre,WebPortalNombre,RepositorioNombre,Agregar,Actualizar,Eliminar,MarcarComoBorrado,DesmarcarComoBorrado,Parametros,Propiedades) values
('es','CreadoPor','Usuario creación','CreadoFecha','Fecha/hora creación','UltimaModificacionPor','Usuario última modificación','UltimaModificacionFecha','Fecha/hora última modificación','BorradoPor','Usuario borrado','BorradoFecha','Fecha/hora borrado','Borrado','Borrado','BD','Entidades','Datos','Negocio','Web.Portal','Repositorio','Agregar','Actualizar','Eliminar','MarcarComoBorrado','DesmarcarComoBorrado','Parametros','Propiedades'),
('en','CreatedBy','Created by','CreatedAt','Created at','ModifiedBy','Modified by','ModifiedAt','Modified at','DeletedBy','Deleted by','DeletedAt','Deleted at','Deleted','Deleted','DB','Entities','Data','Business','Web.Portal','Repository','Add','Update','Delete','SetAsDeleted','UnsetAsDeleted','Parameters','Properties')
go
--====

--====
insert into dbo.BajaTipos (BajaTipoId,Nombre) values
(1,'Lógica'),
(2,'Física'),
(3,'Ninguna')
go
--====

--====
insert into dbo.PropiedadTipos (PropiedadTipoId,Nombre,CLRType,TSQLType) values
(1,'Texto','string','nvarchar'),
(2,'GUID','Guid','uniqueidentifier'),
(3,'Entero','int','int'),
(4,'Entero corto','short','smallint'),
(5,'Entero largo','long','bigint'),
(6,'Decimal','decimal','decimal'),
(7,'Decimal flotante','double','float'),
(8,'Importe','decimal','money'),
(9,'Fecha/Hora','DateTime','datetime'),
(10,'Fecha','DateTime','date'),
(11,'Hora','TimeSpan','time'),
(12,'Booleano','bool','bit')
go
--====

--====
insert into dbo.PropiedadCategorias (PropiedadCategoriaId,Nombre) values
(1,'General'),
(2,'ID'),
(3,'Auditoría creado'),
(4,'Auditoría modificado'),
(5,'Auditoría borrado')
go
--====

--====
insert into dbo.AsociacionMultiplicidades (AsociacionMultiplicidadId,Nombre) values
(1,'0..1'),
(2,'1'),
(3,'*')
go
--====

--====
insert into dbo.AsociacionReglas (AsociacionReglaId,Nombre) values
(1,'No action'),
(2,'Cascade'),
(3,'Set Null'),
(4,'Set Default')
go
--====
