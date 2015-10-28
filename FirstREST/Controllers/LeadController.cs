using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FirstREST.Controllers
{
    public class LeadController : ApiController
    {
        [Route("api/leads")]
        [HttpGet]
        public IEnumerable<Lib_Primavera.Model.Lead> Get()
        {
            return Lib_Primavera.PriIntegration.ListaLeads();
        }

        [Route("api/leads/{id}")]
        [HttpGet]
        public Lib_Primavera.Model.Lead Get(string id)
        {
            return Lib_Primavera.PriIntegration.GetLead(id);
        }

        [Route("api/leads/{id}/tasks")]
        [HttpGet]
        public IEnumerable<Lib_Primavera.Model.Tarefa> GetTarefas(string id)
        {
            return Lib_Primavera.PriIntegration.GetTarefasLead(id);
        }
    }
}
