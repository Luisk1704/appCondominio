using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceRubroCobro
    {
        IEnumerable<RubroCobro> GetRubros();
        IEnumerable<RubroCobro> GetRubrosByLetra(char letra);
        RubroCobro GetRubroByID(int ID);
        void Guardar(RubroCobro rubro);
    }
}
