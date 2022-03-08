namespace GG.Portafolio.Api.Model
{
    public class OAuthConfiguration
    {
        public string Authority { get; set; }

        public string Audience { get; set; }

        public string[] Scope { get; set; }
    }
}
