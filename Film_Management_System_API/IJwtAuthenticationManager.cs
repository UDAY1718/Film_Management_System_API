namespace Film_Management_System_API
{
    public interface IJwtAuthenticationManager
    {
        string Authenticate(string AdminUsername, string AdminPassword);
    }
}
