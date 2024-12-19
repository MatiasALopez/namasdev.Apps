insert into dbo.AspNetUsers (Id, Email, EmailConfirmed, PasswordHash, SecurityStamp, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled, AccessFailedCount, UserName) values
('6b8d28d7-2c9a-44b7-9bc5-8e2b72f5eec8', 'adm@namasdev.ccc',	1, 'APblXneshuQ7dzDBAoUCHrD99sGoNtSZEBOTpyRUlPF6WHpybGqX+FBAIEbCQAq05g==', '224feee5-eb58-4f6c-9978-dd609871754c', 0, 0, 1, 0, 'adm@namasdev.ccc')
go


insert into dbo.AspNetUserRoles (UserId, RoleId) values
('6b8d28d7-2c9a-44b7-9bc5-8e2b72f5eec8', '1212CE75-E3CA-4838-B083-DCB5753CB657')
go


insert into dbo.Usuarios (UsuarioId, Email, nombres, Apellidos, creadopor, creadofecha, ultimamodificacionpor, ultimamodificacionfecha) values
('6b8d28d7-2c9a-44b7-9bc5-8e2b72f5eec8', 'adm@namasdev.ccc', 'Admin', 'Pruebas', '6b8d28d7-2c9a-44b7-9bc5-8e2b72f5eec8', getdate(), '6b8d28d7-2c9a-44b7-9bc5-8e2b72f5eec8', getdate())
go 
