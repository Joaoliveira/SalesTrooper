using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace FirstREST.Controllers
{
    [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
    public class VendedorController : ApiController
    {
        [Route("api/salesmen")]
        [HttpGet]
        public IEnumerable<Lib_Primavera.Model.Vendedor> Get()
        {
            return Lib_Primavera.PriIntegration.ListaVendedores();
        }

        [Route("api/salesmen/{id}")]
        [HttpGet]
        public Lib_Primavera.Model.Vendedor Get(string id)
        {
            return Lib_Primavera.PriIntegration.GetVendedor(id);
        }

        [Route("api/salesmen/{id}/leads")]
        [HttpGet]
        public IEnumerable<Lib_Primavera.Model.Lead> GetL(string id)
        {
            return Lib_Primavera.PriIntegration.LeadsVendedor(id);
        }

        [Route("api/salesmen/{id}/clients")]
        [HttpGet]
        public  IEnumerable<Lib_Primavera.Model.Cliente> GetVendedorClientes(string id)
        {
            return Lib_Primavera.PriIntegration.GetVendedorClientes(id);
        }

        [Route("api/salesmen/{id}/tasks")]
        [HttpGet]
        public IEnumerable<Lib_Primavera.Model.Tarefa> GetVendedorTarefas(string id, [FromUri] string dataInicio, [FromUri] string dataFim)
        {
            return Lib_Primavera.PriIntegration.GetVendedorTarefas(id, dataInicio, dataFim);
        }
    }
}
