
namespace TorrefactorApi.Repository.Profile
{
  public class MapperProfile : AutoMapper.Profile
  {
    public MapperProfile()
    {
      CreateMap<Context.User, Repository.Model.User>()
        .ReverseMap();
    }
  }
}
