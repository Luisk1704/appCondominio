using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceAreaComun : IServiceAreaComun
    {
        public AreaComun GetAreaComunByID(int ID)
        {
            return new RepositoryAreaComun().GetAreaComunByID(ID);
        }

        public IEnumerable<AreaComun> GetAreasComunes()
        {
            return new RepositoryAreaComun().GetAreasComunes();
        }

        public IEnumerable<Reservacion> GetReservacionesByID(int areaComunID)
        {
            return new RepositoryAreaComun().GetReservacionesByID(areaComunID);
        }
    }
}
