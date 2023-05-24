using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    interface IRepositoryUsuario
    {
        IEnumerable<Usuario> GetUsuarios();
        Usuario GetUsuarioByID(int ID);
        void Save(Usuario user);

        Usuario GetUsuario(string usuario, string contrasenna);
    }
}
