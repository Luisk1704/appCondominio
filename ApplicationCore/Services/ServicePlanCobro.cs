using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServicePlanCobro : IServicePlanCobro
    {
        public PlanCobro GetPlanCobroByID(int ID)
        {
            PlanCobro planCobro = new RepositoryPlanCobro().GetPlanCobroByID(ID);
            return planCobro;
        }

        public IEnumerable<PlanCobro> GetPlanCobros()
        {
            IEnumerable<PlanCobro> lista = new RepositoryPlanCobro().GetplanCobro();
            return lista;
        }

        public void Guardar(PlanCobro plan, string[] listaRubros)
        {
            new RepositoryPlanCobro().Guardar(plan, listaRubros);
        }
    }
}
