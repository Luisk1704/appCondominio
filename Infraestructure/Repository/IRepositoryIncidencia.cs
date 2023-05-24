using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryIncidencia
    {
        IEnumerable<Incidencia> GetIncidencias();
        Incidencia GetIncidenciaByID(int ID);
        void Guardar(Incidencia incidencia);
    }
}
