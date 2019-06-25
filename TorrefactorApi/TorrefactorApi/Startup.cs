using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using TorrefactorApi.Context;
using TorrefactorApi.Repository.Profile;
using TorrefactorApi.Repository.Repos;
using TorrefactorApi.Service;
using TorrefactorApi.Service.Implementation;

namespace TorrefactorApi
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
      var mappingConfig = new AutoMapper.MapperConfiguration(cfg =>
      {
        cfg.AddProfile(new MapperProfile());
      });
      var mapper = mappingConfig.CreateMapper();

      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
      services.AddDbContext<UserDbContext>(item => item.UseSqlServer(Configuration.GetConnectionString("myconn")));
      services.AddTransient<UserDbContext>();
      services.AddTransient<IUserRepo, UserRepo>();
      services.AddTransient<IUserListener, UserListennerA>();
      services.AddTransient<IUserListener, UserListennerC>();
      services.AddTransient<IUserListener, UserListennerB>();
      services.AddTransient<IUserService, UserService>();
      services.AddSingleton(mapper);

      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info {
          Version = "v1",
          Title = "Torrefactor API"
        });
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }

      app.UseHttpsRedirection();
      app.UseSwagger();
      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
      });


      app.UseMvc();

      
    }
  }
}
