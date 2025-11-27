namespace Penitenciaria.Modelos.Configuraciones
{
    public class ConfiguracionJwt
    {
        public string Clave { get; set; } = string.Empty;
        public string Emisor { get; set; } = string.Empty;
        public string Audiencia { get; set; } = string.Empty;
        public int DuracionTokenMinutos { get; set; } 
    }
}