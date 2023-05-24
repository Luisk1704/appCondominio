using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceResidencia : IServiceResidencia
    {
        public Residencia GetResidenciaByID(int ID)
        {
            Residencia residencia = new RepositoryResidencia().GetResidenciaByID(ID);
            return residencia;
        }

        public IEnumerable<Residencia> GetResidencias()
        {
            IEnumerable<Residencia> lista = new RepositoryResidencia().GetResidencias();
            return lista;
        }

        public void Guardar(Residencia res)
        {
            new RepositoryResidencia().Guardar(res);
        }
    }
}
