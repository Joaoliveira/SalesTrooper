using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FirstREST.Lib_Primavera.Model;

namespace FirstREST.Controllers
{
    public class OportunidadesDeVendaController : ApiController
    {
        // GET api/oportunidadesdevenda
        public IEnumerable<Lib_Primavera.Model.OportunidadeDeVenda> Get()
        {
            return Lib_Primavera.PriIntegration.ListaOportunidadesDeVenda();
        }

        // GET api/oportunidadesdevenda/5
        public Lib_Primavera.Model.OportunidadeDeVenda Get(string id)
        {
            return Lib_Primavera.PriIntegration.GetOportunidadeDeVenda(id);
        }

        // POST api/oportunidadesdevenda
        public void Post([FromBody]string value)
        {
        }

        // PUT api/oportunidadesdevenda/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/oportunidadesdevenda/5
        public void Delete(int id)
        {
        }
    }
}
