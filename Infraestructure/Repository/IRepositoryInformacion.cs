using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    interface IRepositoryInformacion
    {
        IEnumerable<Informacion> GetInformacion();
        Informacion GetInformacionByID(int ID);

        IEnumerable<Informacion> GetNoticias();

        IEnumerable<Informacion> GetAvisos();

        IEnumerable<Informacion> GetDocumentos();
        void Guardar(Informacion info);
    }
}
