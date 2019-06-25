using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TorrefactorApi.Context;
using TorrefactorApi.Filters;
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

    public void ConfigureServices(IServiceCollection services)
    {
      var mappingConfig = new AutoMapper.MapperConfiguration(cfg =>
      {
        cfg.AddProfile(new MapperProfile());
      });
      var mapper = mappingConfig.CreateMapper();

      services.AddMvc(options =>
      {
        options.Filters.Add(typeof(ApiFilter), 10);
        options.Filters.Add(typeof(TokenFilter), 20);
      }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

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

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
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
