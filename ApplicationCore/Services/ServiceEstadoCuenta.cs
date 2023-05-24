using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceEstadoCuenta : IServiceEstadoCuenta
    {
        public EstadoCuenta GetdetallePagados(int ID)
        {
            return new RepositoryEstadoCuenta().getDetallePagado(ID);
        }

        public EstadoCuenta GetdetallePendientes(int ID)
        {
            return new RepositoryEstadoCuenta().getDetallePendiente(ID);
        }

        public Residencia GetEstadoByUser(int UsuarioID)
        {
            return new RepositoryEstadoCuenta().GetEstadoByUser(UsuarioID);
        }

        public EstadoCuenta GetEstadoCuentaByFecha(DateTime fecha, int UsuarioID)
        {
            return new RepositoryEstadoCuenta().GetEstadoCuentaByFecha(fecha,UsuarioID);
        }

        public EstadoCuenta GetEstadoCuentaByID(int ID)
        {
            return new RepositoryEstadoCuenta().GetEstadoCuentaByID(ID);
        }

        public IEnumerable<EstadoCuenta> GetEstadoCuentas()
        {
            return new RepositoryEstadoCuenta().GetEstados();
        }

        public IEnumerable<EstadoCuenta> GetEstadosByMes(int Mes)
        {
            return new RepositoryEstadoCuenta().GetEstadosByMes(Mes);
        }

        public IEnumerable<EstadoCuenta> GetEstadosByMesPendientes(int Mes)
        {
            return new RepositoryEstadoCuenta().GetEstadosByMesPendientes(Mes);
        }

        public IEnumerable<EstadoCuenta> GetEstadosByResid(int Resid)
        {
            return new RepositoryEstadoCuenta().GetEstadosByResid(Resid);
        }

        public void GetOrdenCountDate(out string etiquetas1, out string valores1)
        {
            new RepositoryEstadoCuenta().GetOrdenCountDate(out string etiquetas, out string valores);
            etiquetas1 = etiquetas;
            valores1 = valores;
        }

        public IEnumerable<EstadoCuenta> GetPagados(int ID)
        {
            return new RepositoryEstadoCuenta().GetPagados(ID);
        }

        public IEnumerable<EstadoCuenta> GetPendientes(int ID)
        {
            return new RepositoryEstadoCuenta().GetPendientes(ID);
        }

        public IEnumerable<EstadoCuenta> GetPendientes()
        {
            return new RepositoryEstadoCuenta().GetPendientes();
        }

        public void Guardar(EstadoCuenta estadoCuenta)
        {
            new RepositoryEstadoCuenta().Guardar(estadoCuenta);
        }
    }
}
