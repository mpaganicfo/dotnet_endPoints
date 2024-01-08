using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCase.Queries
{
    public abstract class BaseQuery : BaseCommand
    {
        public string Token { get; set; }
    }
}
