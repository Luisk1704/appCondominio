using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    interface IRepositoryPlanCobro
    {
        IEnumerable<PlanCobro> GetplanCobro();

        PlanCobro GetPlanCobroByID(int ID);

        void Guardar(PlanCobro plan, string[] listaRubros);
    }
}
