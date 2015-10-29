using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FirstREST.Lib_Primavera.Model;

namespace FirstREST.Controllers
{
    public class TarefasController : ApiController
    {
        // GET api/Tarefas
        public IEnumerable<Lib_Primavera.Model.Tarefa> Get()
        {
            return Lib_Primavera.PriIntegration.ListaTarefas();
        }

        // GET api/Tarefas/5
        public Tarefa Get(string id)
        {
            Lib_Primavera.Model.Tarefa tarefa = Lib_Primavera.PriIntegration.GetTarefa(id);
            if (tarefa == null)
            {
                throw new HttpResponseException(
                        Request.CreateResponse(HttpStatusCode.NotFound));
               

            }
            else
            {
                return tarefa;
            }
        }

        // POST api/<controller>
        [Route("api/task")]
        [HttpPost] 
        public HttpResponseMessage Post(Lib_Primavera.Model.Tarefa tarefa)
        {
            Lib_Primavera.Model.RespostaErro erro = new Lib_Primavera.Model.RespostaErro();
            erro = Lib_Primavera.PriIntegration.InsereTarefaObj(tarefa);

            if (erro.Erro == 0)
            {
                var response = Request.CreateResponse(
                   HttpStatusCode.Created, tarefa);
                string uri = Url.Link("DefaultApi", new { IdTarefa = tarefa.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }

            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

        }
        

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}