using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceEstadoCuenta
    {
        IEnumerable<EstadoCuenta> GetEstadoCuentas();
        IEnumerable<EstadoCuenta> GetEstadosByMes(int Mes);
        IEnumerable<EstadoCuenta> GetEstadosByMesPendientes(int Mes);
        IEnumerable<EstadoCuenta> GetEstadosByResid(int Resid);
        EstadoCuenta GetEstadoCuentaByFecha(DateTime fecha, int UsuarioID);
        EstadoCuenta GetEstadoCuentaByID(int ID);
        Residencia GetEstadoByUser(int UsuarioID);
        IEnumerable<EstadoCuenta> GetPagados(int ID);
        IEnumerable<EstadoCuenta> GetPendientes(int ID);
        IEnumerable<EstadoCuenta> GetPendientes();
        EstadoCuenta GetdetallePagados(int ID);
        EstadoCuenta GetdetallePendientes(int ID);
        void Guardar(EstadoCuenta estadoCuenta);
        void GetOrdenCountDate(out string etiquetas, out string valores);
    }
}
