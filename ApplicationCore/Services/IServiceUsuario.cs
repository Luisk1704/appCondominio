using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceUsuario
    {
        IEnumerable<Usuario> GetUsuarios();
        Usuario GetUsuarioByID(int ID);
        void Save(Usuario usuario);
        Usuario GetUsuario(string usuario, string contrasenna);
    }
}
