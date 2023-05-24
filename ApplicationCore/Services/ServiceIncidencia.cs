using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceIncidencia : IServiceIncidencia
    {
        public Incidencia GetIncidenciaByID(int ID)
        {
            return new RepositoryIncidencia().GetIncidenciaByID(ID);
        }

        public IEnumerable<Incidencia> GetIncidencias()
        {
            return new RepositoryIncidencia().GetIncidencias();
        }

        public void Guardar(Incidencia incidencia)
        {
            new RepositoryIncidencia().Guardar(incidencia);
        }
    }
}
