namespace dotnet_wos_abm_reglas_auditoria_api.Domain.Dtos.Locations
{
    public class LocationResponse
    {
        public string localidad { get; set; }

        public string partido { get; set; }

        public string provincia { get; set; }

        public string[] codigosPostales { get; set; }
    }
}
