namespace PARKE_Landing_Page.Services.Interfaces
{
    public interface IHashingService
    {
        string Hash(string plainPassword);
        bool Verify(string hashedPassword, string plainPassword);




    }
}
