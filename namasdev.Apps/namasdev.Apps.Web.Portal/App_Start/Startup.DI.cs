using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web;

using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using namasdev.Net.Correos;

using namasdev.Apps.Entidades.Valores;
using namasdev.Apps.Datos;
using namasdev.Apps.Datos.Sql;
using namasdev.Apps.Negocio;
using namasdev.Apps.Web.Portal.Controllers;

namespace namasdev.Apps.Web.Portal
{
    public partial class Startup
    {
        public void ConfigureServices()
        {
            var services = new ServiceCollection();
            RegisterServices(services);

            var resolver = new DefaultDependencyResolver(services.BuildServiceProvider());
            DependencyResolver.SetResolver(resolver);
        }

        private void RegisterServices(ServiceCollection services)
        {
            RegisterUtils(services);
            RegisterRepositorios(services);
            RegisterNegocios(services);
            RegisterControllers(services);
        }

        private void RegisterUtils(ServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup).Assembly, typeof(UsuariosNegocio).Assembly);
        }

        private void RegisterRepositorios(ServiceCollection services)
        {
            services.AddScoped<SqlContext>();

            services.AddScoped<IParametrosRepositorio, ParametrosRepositorio>();
            services.AddScoped<IErroresRepositorio, ErroresRepositorio>();
            services.AddScoped<ICorreosParametrosRepositorio, CorreosParametrosRepositorio>();
            services.AddScoped<IIdiomasRepositorio, IdiomasRepositorio>();
            services.AddScoped<IUsuariosRepositorio, UsuariosRepositorio>();
            services.AddScoped<IAplicacionesRepositorio, AplicacionesRepositorio>();
            services.AddScoped<IAplicacionesVersionesRepositorio, AplicacionesVersionesRepositorio>();
            services.AddScoped<IEntidadesRepositorio, EntidadesRepositorio>();
            services.AddScoped<IEntidadesEspecificacionesRepositorio, EntidadesEspecificacionesRepositorio>();
            services.AddScoped<IEntidadesPropiedadesRepositorio, EntidadesPropiedadesRepositorio>();
            services.AddScoped<IEntidadesClavesRepositorio, EntidadesClavesRepositorio>();
            services.AddScoped<IEntidadesAsociacionesRepositorio, EntidadesAsociacionesRepositorio>();
            services.AddScoped<IEntidadesIndicesRepositorio, EntidadesIndicesRepositorio>();
            services.AddScoped<IEntidadesIndicesPropiedadesRepositorio, EntidadesIndicesPropiedadesRepositorio>();
            services.AddScoped<IEntidadesChecksRepositorio, EntidadesChecksRepositorio>();
        }

        private void RegisterNegocios(ServiceCollection services)
        {
            services.AddSingleton<ServidorDeCorreosParametros>((f) => JsonConvert.DeserializeObject<ServidorDeCorreosParametros>(f.GetService<IParametrosRepositorio>().Obtener(Parametros.SERVIDOR_CORREOS)));
            services.AddSingleton<IGeneradorArchivos>((f) => new GeneradorArchivos(HttpContext.Current.Server.MapPath("~/Views/Templates")));

            services.AddScoped<IServidorDeCorreos, ServidorDeCorreos>();
            services.AddScoped<IErroresNegocio, ErroresNegocio>();
            services.AddScoped<ICorreosNegocio, CorreosNegocio>();
            services.AddScoped<IUsuariosNegocio, UsuariosNegocio>();
            services.AddScoped<IAplicacionesNegocio, AplicacionesNegocio>();
            services.AddScoped<IAplicacionesVersionesNegocio, AplicacionesVersionesNegocio>();
            services.AddScoped<IEntidadesNegocio, EntidadesNegocio>();
            services.AddScoped<IEntidadesEspecificacionesNegocio, EntidadesEspecificacionesNegocio>();
            services.AddScoped<IEntidadesPropiedadesNegocio, EntidadesPropiedadesNegocio>();
            services.AddScoped<IEntidadesAsociacionesNegocio, EntidadesAsociacionesNegocio>();
            services.AddScoped<IEntidadesIndicesNegocio, EntidadesIndicesNegocio>();
            services.AddScoped<IEntidadesChecksNegocio, EntidadesChecksNegocio>();
        }

        private void RegisterControllers(ServiceCollection services)
        {
            services.AddTransient<ListasController>();
            services.AddTransient<AccountController>();
            services.AddTransient<HomeController>();
            services.AddTransient<UsuariosController>();
            services.AddTransient<AplicacionesController>();
            services.AddTransient<VersionesController>();
            services.AddTransient<EntidadesController>();
            services.AddTransient<EntidadesEspecificacionesController>();
            services.AddTransient<EntidadesPropiedadesController>();
            services.AddTransient<EntidadesAsociacionesController>();
            services.AddTransient<EntidadesIndicesController>();
            services.AddTransient<EntidadesChecksController>();
            services.AddTransient<TemplatesController>();
        }
    }

    public class DefaultDependencyResolver : IDependencyResolver
    {
        protected IServiceProvider serviceProvider;

        public DefaultDependencyResolver(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public object GetService(Type serviceType)
        {
            return this.serviceProvider.GetService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return this.serviceProvider.GetServices(serviceType);
        }
    }
}