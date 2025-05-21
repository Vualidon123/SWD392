using MongoDB.Driver;
using SWD_Clone.Models;

namespace SWD_Clone.Service
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
        public async Task<Users> Login(loginRequest loginRequest)
        {
            var user = await _context.user.Find(x => x.Email == loginRequest.Email && x.Password == loginRequest.Password).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new Exception("User not exits");
            }
            return user;
        }
        public async Task<Users> Register(RegisterRequest registerRequest)
        {
            // Check if user already exists
            var user = await _context.user.Find(x => x.Email == registerRequest.Email).FirstOrDefaultAsync();
            if (user != null)
            {
                throw new Exception("Email already exits");
            }T  

            // Create new student
            var user = new Users
            {
                name = registerDto.name,
                email = registerDto.email,
                phone = registerDto.phone,
                password = HashPassword(registerDto.password)
            };

            _context.Student.Add(student);
            await _context.SaveChangesAsync();

            return student;
        }


    }
}
