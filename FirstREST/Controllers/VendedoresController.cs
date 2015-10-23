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
    public class VendedoresController : ApiController
    {
        //
        // GET: /Vendedores/

        public IEnumerable<Lib_Primavera.Model.Vendedor> Get()
        {
            return Lib_Primavera.PriIntegration.ListaVendedores();
        }

        // GET api/Vendedores/5    
        public Vendedor Get(string id)
        {
            Lib_Primavera.Model.Vendedor vendedor = Lib_Primavera.PriIntegration.GetVendedor(id);
            if (vendedor == null)
            {
                throw new HttpResponseException(
                        Request.CreateResponse(HttpStatusCode.NotFound));

            }
            else
            {
                return vendedor;
            }
        }

        public IEnumerable<Lib_Primavera.Model.Cliente> GetClientes(string id)
        {
            return Lib_Primavera.PriIntegration.ListaClientesPorVendedor(id);
        }

    }
}
