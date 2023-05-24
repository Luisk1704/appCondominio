using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    interface IRepositoryEstadoCuenta
    {
        IEnumerable<EstadoCuenta> GetEstados();
        EstadoCuenta GetEstadoCuentaByID(int ID);
        EstadoCuenta GetEstadoCuentaByFecha(DateTime fecha, int UsuarioID);

        Residencia GetEstadoByUser(int UsuarioID);
        IEnumerable<EstadoCuenta> GetPagados(int ID);
        IEnumerable<EstadoCuenta> GetPendientes(int ID);
        IEnumerable<EstadoCuenta> GetPendientes();
        void GetOrdenCountDate(out string etiquetas, out string valores);
        IEnumerable<EstadoCuenta> GetEstadosByMes(int Mes);
        IEnumerable<EstadoCuenta> GetEstadosByMesPendientes(int Mes);
        IEnumerable<EstadoCuenta> GetEstadosByResid(int Resid);
        EstadoCuenta getDetallePagado(int ID);
        EstadoCuenta getDetallePendiente(int ID);

        void Guardar(EstadoCuenta estadoCuenta); 
    }
}
