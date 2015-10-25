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
        [Route("api/Leads")]
        [HttpGet]
        public IEnumerable<Lib_Primavera.Model.Lead> Get()
        {
            return Lib_Primavera.PriIntegration.ListaLeads();
        }

        [Route("api/Lead/{id}")]
        [HttpGet]
        public Lib_Primavera.Model.Lead Get(string id)
        {
            return Lib_Primavera.PriIntegration.GetLead(id);
        }
    }
}
