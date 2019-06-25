using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TorrefactorApi.Context;
using TorrefactorApi.Repository.Model;

namespace TorrefactorApi.Repository.Repos
{
  public class UserRepo : IUserRepo
  {
    private readonly UserDbContext _context;
    private readonly IMapper _mapper;

    public UserRepo(UserDbContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    public async Task CreateUser(Model.User user)
    {
      var dbUser = _mapper.Map<Context.User>(user);
      _context.Users.Add(dbUser);
      await _context.SaveChangesAsync();

      user.Id = dbUser.Id;
    }

    public Model.User GetUserByEmail(string email)
    {
      var user = _context.Users.FirstOrDefault(u => u.Email == email);
      return _mapper.Map<Model.User>(user);
    }

    public Model.User GetUserByUsername(string username)
    {
      var user = _context.Users.FirstOrDefault(u => u.Username == username);
      return _mapper.Map<Model.User>(user);
    }

    public IEnumerable<Model.User> GetUsers()
    {
      var users = _context.Users.ToList();
      return _mapper.Map<IEnumerable<Model.User>>(users);
    }
  }
}
