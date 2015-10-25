using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FirstREST.Controllers
{
    public class VendedorController : ApiController
    {
        [Route("api/Vendedores")]
        [HttpGet]
        public IEnumerable<Lib_Primavera.Model.Vendedor> Get()
        {
            return Lib_Primavera.PriIntegration.ListaVendedores();
        }

        [Route("api/Vendedor/{id}")]
        [HttpGet]
        public Lib_Primavera.Model.Vendedor Get(string id)
        {
            return Lib_Primavera.PriIntegration.GetVendedor(id);
        }


        [Route("api/Vendedor/{id}/clientes")]
        [HttpGet]
        public  IEnumerable<Lib_Primavera.Model.Cliente> GetVendedorClientes(string id)
        {
            return Lib_Primavera.PriIntegration.GetVendedorClientes(id);
        }
    }
}
