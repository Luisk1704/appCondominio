using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryReservacion
    {                
        Reservacion GetReservacionByID(int UsuarioID,int AreaComunID);
        Reservacion GetReservacionByDate(DateTime fecha);
        void Guardar(Reservacion reservacion);
        IEnumerable<Reservacion> GetReservaciones();
        IEnumerable<Reservacion> GetReservaciones(int UsuarioID);
        IEnumerable<Reservacion> GetReservacionByEstado(string estado);
        Reservacion GetReservacion(DateTime fecha,int idArea,int idUsuario);
    }
}
