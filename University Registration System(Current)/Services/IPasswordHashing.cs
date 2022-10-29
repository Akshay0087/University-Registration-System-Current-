namespace UniversitySystemRegistration.Services
{
    public interface IPasswordHashing
    {
        string Hash(string value);
    }
}