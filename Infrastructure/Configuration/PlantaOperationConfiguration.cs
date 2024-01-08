using System;

namespace Infrastructure.Configuration
{
    public class PlantaOperationConfiguration
    {
        public PlantaOperationConfiguration(string getById, string authorizationSchema, string getAll)
        {
            GetByIdTemplate = getById 
                ?? throw new ArgumentNullException(nameof(getById), "Se debe proporcionar un valor para getById");

            AuthorizationSchema = authorizationSchema
                ?? throw new ArgumentNullException(nameof(authorizationSchema), "Se debe proporcionar un valor para authorizationSchema");

            GetAll = getAll
                ?? throw new ArgumentNullException(nameof(getAll), "Se debe proporcionar un valor para getAll");
        }

        public string GetByIdTemplate { get; private set; }

        public string GetAll { get; private set; }

        public string AuthorizationSchema { get; private set; }
    }
}
