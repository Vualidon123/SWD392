using MongoDB.Driver;
using SPTS_Writer.Models;

namespace SPTS_Writer.Service
{
    public class Authen
    {
        private readonly MongoDbContext _context;
        private readonly IConfiguration _configuration;
        public Authen(MongoDbContext context,IConfiguration configuration)
        {
            _context = context;
            _configuration= configuration;
        }
        public async Task<User> Login(loginRequest loginRequest)
        {
            var user = await _context.Users.Find(x => x.Email == loginRequest.Email && x.Password == loginRequest.Password).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new Exception("User not exits");
            }
            return user;
        }
        public async Task<User> Register(RegisterRequest registerRequest)
        {
            // Check if user already exists
            var user = await _context.Users.Find(x => x.Email == registerRequest.Email).FirstOrDefaultAsync();
            if (user != null)
            {
                throw new Exception("Email already exits");
            }

            // Create new student
            user = new User
            {
                Name = registerRequest.Name,
                Email = registerRequest.Email,
                PhoneNumber = registerRequest.PhoneNumber,
                //Password = HashPassword(registerRequest.Password)
            };

            //_context.Users.Add(user);
            //await _context.SaveChangesAsync();

            return user;
        }


    }
}
