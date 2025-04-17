using PARKE_Landing_Page.Services;
using PARKE_Landing_Page.Services.Interfaces;

namespace PARKE_Landing_Page.Services

{
    public class HashingService : IHashingService
    {
        public string Hash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool Verify(string password, string hash )
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}
