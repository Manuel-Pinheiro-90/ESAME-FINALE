namespace Api_Finale.Service
{
    public interface IpasswordEncoder
    {
        string Encode(string password);
        bool Verify(string password, string encodedPassword);

    }
}
