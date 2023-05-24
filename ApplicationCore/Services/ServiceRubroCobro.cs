using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceRubroCobro : IServiceRubroCobro
    {
        public RubroCobro GetRubroByID(int ID)
        {
            return new RepositoryRubroCobro().GetRubroByID(ID);
        }

        public IEnumerable<RubroCobro> GetRubros()
        {
            return new RepositoryRubroCobro().GetRubros();
        }

        public IEnumerable<RubroCobro> GetRubrosByLetra(char letra)
        {
            return new RepositoryRubroCobro().GetRubrosByLetra(letra);
        }

        public void Guardar(RubroCobro rubro)
        {
            new RepositoryRubroCobro().Guardar(rubro);
        }


    }
}
