using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceAreaComun
    {
        IEnumerable<AreaComun> GetAreasComunes();
        AreaComun GetAreaComunByID(int ID);
        IEnumerable<Reservacion> GetReservacionesByID(int areaComunID);
    }
}
