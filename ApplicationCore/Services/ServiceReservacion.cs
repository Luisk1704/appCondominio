using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceReservacion : IServiceReservacion
    {
        public Reservacion GetReservacion(DateTime fecha, int idArea, int idUsuario)
        {
            return new RepositoryReservacion().GetReservacion(fecha,idArea,idUsuario);
        }

        public Reservacion GetReservacionByDate(DateTime fecha)
        {
            return new RepositoryReservacion().GetReservacionByDate(fecha);
        }

        public IEnumerable<Reservacion> GetReservacionByEstado(string estado)
        {
            return new RepositoryReservacion().GetReservacionByEstado(estado);
        }

        public Reservacion GetReservacionByID(int UsuarioID, int AreaComunID)
        {
            return new RepositoryReservacion().GetReservacionByID(UsuarioID,AreaComunID);
        }

        public IEnumerable<Reservacion> GetReservaciones()
        {
            return new RepositoryReservacion().GetReservaciones();
        }

        public IEnumerable<Reservacion> GetReservaciones(int UsuarioID)
        {
            return new RepositoryReservacion().GetReservaciones(UsuarioID);
        }

        public Reservacion GetReservacionesByHour(int hora, int id)
        {
            return new RepositoryReservacion().GetReservacionesByHour(hora,id);
        }

        public IEnumerable<Reservacion> GetReservacionesByMonth(int mes, int id)
        {
            return new RepositoryReservacion().GetReservacionesByMonth(mes,id);
        }

        public void Guardar(Reservacion reservacion)
        {
            new RepositoryReservacion().Guardar(reservacion);
        }
    }
}
