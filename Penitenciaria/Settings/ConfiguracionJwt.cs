namespace Penitenciaria.Modelos.Configuraciones
{
    public class ConfiguracionJwt
    {
        public string Clave { get; set; } = string.Empty; // Key
        public string Emisor { get; set; } = string.Empty; // Issuer
        public string Audiencia { get; set; } = string.Empty; // Audience
        public int DuracionTokenMinutos { get; set; } // TokenValidityInMinutes
    }
}