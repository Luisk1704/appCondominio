using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceReservacion
    {
        IEnumerable<Reservacion> GetReservacionesByMonth(int mes, int id);
        Reservacion GetReservacionesByHour(int hora, int id);
        IEnumerable<Reservacion> GetReservaciones();
        Reservacion GetReservacionByID(int UsuarioID, int AreaComunID);
        Reservacion GetReservacionByDate(DateTime fecha);
        void Guardar(Reservacion reservacion);
        IEnumerable<Reservacion> GetReservacionByEstado(string estado);
        IEnumerable<Reservacion> GetReservaciones(int UsuarioID);
        Reservacion GetReservacion(DateTime fecha, int idArea, int idUsuario);
    }
}
