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
insert into dbo.Idiomas (IdiomaId,Nombre) values
('es','Español'),
('en','Inglés')
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
insert into dbo.Articulos (ArticuloId,Nombre) values
(1,'El'),
(2,'La'),
(3,'Los'),
(4,'Las')
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
