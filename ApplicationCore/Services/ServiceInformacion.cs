using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceInformacion : IServiceInformacion
    {
        public IEnumerable<Informacion> GetAvisos()
        {
            return new RepositoryInformacion().GetAvisos();
        }

        public IEnumerable<Informacion> GetDocumentos()
        {
            return new RepositoryInformacion().GetDocumentos();
        }

        public IEnumerable<Informacion> GetInformacion()
        {
            return new RepositoryInformacion().GetInformacion();
        }

        public Informacion GetInformacionByID(int ID)
        {
            return new RepositoryInformacion().GetInformacionByID(ID);
        }

        public IEnumerable<Informacion> GetNoticias()
        {
            return new RepositoryInformacion().GetNoticias();
        }

        public void Guardar(Informacion info)
        {
            new RepositoryInformacion().Guardar(info);
        }
    }
}
