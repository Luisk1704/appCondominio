using ApplicationCore.Utils;
using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceUsuario : IServiceUsuario
    {
        public Usuario GetUsuario(string usuario, string contrasenna)
        {
            string password = Cryptography.EncrypthAES(contrasenna);
            return new RepositoryUsuario().GetUsuario(usuario,password);
        }

        public Usuario GetUsuarioByID(int ID)
        {
            return new RepositoryUsuario().GetUsuarioByID(ID);
        }

        public IEnumerable<Usuario> GetUsuarios()
        {
            return new RepositoryUsuario().GetUsuarios();
        }

        public void Save(Usuario usuario)
        {
            usuario.Contrasenna = Cryptography.EncrypthAES(usuario.Contrasenna);
            new RepositoryUsuario().Save(usuario);
        }
    }
}
