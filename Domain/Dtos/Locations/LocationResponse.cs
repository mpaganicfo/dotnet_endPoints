namespace Domain.Dtos.Locations
{
    public class LocationResponse
    {
        public string localidad { get; set; }

        public string partido { get; set; }

        public string provincia { get; set; }

        public string[] codigosPostales { get; set; }
    }
}
