using System;
using System.Collections.Generic;
using System.Web.Mvc;

using Microsoft.Extensions.DependencyInjection;

using Newtonsoft.Json;

using namasdev.Net.Correos;
using namasdev.Apps.Entidades;
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
            RegisterRepositorios(services);
            RegisterNegocios(services);
            RegisterControllers(services);
        }

        private void RegisterRepositorios(ServiceCollection services)
        {
            services.AddScoped<SqlContext>();

            services.AddScoped<IParametrosRepositorio, ParametrosRepositorio>();
            services.AddScoped<ICorreosParametrosRepositorio, CorreosParametrosRepositorio>();
            services.AddScoped<IUsuariosRepositorio, UsuariosRepositorio>();
            services.AddScoped<IAplicacionesRepositorio, AplicacionesRepositorio>();
            services.AddScoped<IAplicacionesVersionesRepositorio, AplicacionesVersionesRepositorio>();
        }

        private void RegisterNegocios(ServiceCollection services)
        {
            services.AddSingleton<ServidorDeCorreosParametros>((sp) => JsonConvert.DeserializeObject<ServidorDeCorreosParametros>(sp.GetService<IParametrosRepositorio>().Obtener(Parametros.SERVIDOR_CORREOS)));
            
            services.AddScoped<IServidorDeCorreos, ServidorDeCorreos>();
            services.AddScoped<ICorreosNegocio, CorreosNegocio>();
            services.AddScoped<IUsuariosNegocio, UsuariosNegocio>();
            services.AddScoped<IAplicacionesNegocio, AplicacionesNegocio>();
            services.AddScoped<IAplicacionesVersionesNegocio, AplicacionesVersionesNegocio>();
        }

        private void RegisterControllers(ServiceCollection services)
        {
            services.AddTransient<AccountController>();
            services.AddTransient<UsuariosController>();
            services.AddTransient<AplicacionesController>();
            services.AddTransient<AplicacionesVersionesController>();
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