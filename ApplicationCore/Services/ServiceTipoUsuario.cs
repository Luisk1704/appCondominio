using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceTipoUsuario : IServiceTipoUsuario
    {
        public IEnumerable<TipoUsuario> GetRoles()
        {
            return new RepositoryTipoUsuario().GetRoles();
        }

        public TipoUsuario GetTipoUsuarioByID(int ID)
        {
            return new RepositoryTipoUsuario().GetRolByID(ID);
        }
    }
}
