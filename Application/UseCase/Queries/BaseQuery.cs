using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet_wos_abm_reglas_auditoria_api.Application.UseCase.V2.Queries
{
    public abstract class BaseQuery : BaseCommand
    {
        public string Token { get; set; }
    }
}
