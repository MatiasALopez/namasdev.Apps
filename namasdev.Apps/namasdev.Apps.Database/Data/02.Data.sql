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
