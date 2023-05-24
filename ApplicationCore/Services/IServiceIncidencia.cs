using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceIncidencia
    {
        IEnumerable<Incidencia> GetIncidencias();
        Incidencia GetIncidenciaByID(int ID);
        void Guardar(Incidencia incidencia);
    }
}
