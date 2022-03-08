namespace GG.Portafolio.Site.Generic.Interfaces
{
    public interface ISerialize
    {
        string Serialize<T>(T value);

        T Deserialize<T>(string value);
    }
}
