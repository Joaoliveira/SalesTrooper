using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

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

        [Route("api/leads")]
        [HttpPost]
        public HttpResponseMessage Post(Lib_Primavera.Model.Lead lead)
        {
            Lib_Primavera.Model.RespostaErro erro = new Lib_Primavera.Model.RespostaErro();
            erro = Lib_Primavera.PriIntegration.InsereLeadObj(lead);

            if (erro.Erro == 0)
            {
                var response = Request.CreateResponse(
                   HttpStatusCode.Created, lead);
                /*string uri = Url.Link("DefaultApi", new { CodCliente = cliente.CodCliente });
                response.Headers.Location = new Uri(uri);*/
                return response;
            }

            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, erro.Descricao);
            }

        }
    }
}
