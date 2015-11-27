﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FirstREST.Lib_Primavera.Model;
using System.Web.Http.Cors;

namespace FirstREST.Controllers
{
    public class IterTarefasController : ApiController
    {
        // GET api/IterTarefas
        [Route("api/itertarefas/")]
        [HttpGet]
        public IEnumerable<Lib_Primavera.Model.IterTarefa> Get()
        {
            return Lib_Primavera.PriIntegration.ListaIterTarefas();
        }

        // GET api/IterTarefas/5
        [Route("api/itertarefas/{id}/")]
        [HttpGet]
        public IEnumerable<Lib_Primavera.Model.IterTarefa> Get(string id)
        {
            IEnumerable<Lib_Primavera.Model.IterTarefa> iterTarefa = Lib_Primavera.PriIntegration.GetIterTarefasTarefa(id);
            if (iterTarefa == null)
            {
                throw new HttpResponseException(
                        Request.CreateResponse(HttpStatusCode.NotFound));


            }
            else
            {
                return iterTarefa;
            }
        }

        // POST api/<controller>
        [Route("api/itertarefas")]
        [HttpPost]
        public HttpResponseMessage Post(Lib_Primavera.Model.IterTarefa tarefa)
        {
            Lib_Primavera.Model.RespostaErro erro = new Lib_Primavera.Model.RespostaErro();
            erro = Lib_Primavera.PriIntegration.InsereIterTarefaObj(tarefa);

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
        [Route("api/itertarefas/{id}/")]
        [HttpPut]
        public HttpResponseMessage Put(string id, [FromBody]string value)
        {
            Lib_Primavera.Model.RespostaErro erro = new Lib_Primavera.Model.RespostaErro();

            try
            {
                erro = Lib_Primavera.PriIntegration.UpdIterTarefa(id, value);
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