namespace EventService.Options
{
    public class IdentityServerOptions
    {
        public string Address { get; set; } = null!;
        public string ClientId { get; set; } = null!;
        public string ClientSecret { get; set; } = null!;
        public string Scope { get; set; } = null!;
    }
}
