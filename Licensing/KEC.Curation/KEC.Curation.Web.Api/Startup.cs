using KEC.Curation.Data.UnitOfWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace KEC.Curation.Web.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
         
           
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddCors(o => o.AddPolicy("AllowCrossSiteJson", builder =>
                                                                        {
                                                                            builder.AllowAnyHeader()
                                                                                   .AllowAnyMethod()
                                                                                   .AllowAnyOrigin();
                                                                        }));

            services.Configure<MvcOptions>(option =>
                                            {
                                                option.OutputFormatters.RemoveType
                                                <XmlDataContractSerializerOutputFormatter>();
                                            });

            services.AddTransient<IUnitOfWork>(m=>new EFUnitOfWork());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
           app.UseCors("CurationCORS");

            app.UseMvc();

        }
    }
}
