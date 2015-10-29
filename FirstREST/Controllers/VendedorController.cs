﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FirstREST.Controllers
{
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
    }
}
