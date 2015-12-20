using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FirstREST.Lib_Primavera.Model;
using System.Web.Http.Cors;

namespace FirstREST.Controllers
{

    public class TarefasController : ApiController
    {
        // GET api/Tarefas
        [Route("api/tasks/")]
        [HttpGet]
        public IEnumerable<Lib_Primavera.Model.Tarefa> Get()
        {
            return Lib_Primavera.PriIntegration.ListaTarefas();
        }

        // GET api/Tarefas/5
        [Route("api/tasks/{id}/")]
        [HttpGet]
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
        [Route("api/tasks")]
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
                return response;
            }

            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, erro.Descricao);
            }

        }
        
        // PUT api/<controller>/5
        [Route("api/tasks/{id}/")]
        [HttpPut]
        public HttpResponseMessage Put(string id, [FromBody]string value)
        {
            Lib_Primavera.Model.RespostaErro erro = new Lib_Primavera.Model.RespostaErro();

            try
            {
                erro = Lib_Primavera.PriIntegration.UpdTarefa(id, value);
                if (erro.Erro == 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, erro.Descricao);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, erro.Descricao);
                }
            }

            catch (Exception exc)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, erro.Descricao);
            }
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}