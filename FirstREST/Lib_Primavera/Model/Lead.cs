using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstREST.Lib_Primavera.Model
{
    public class Lead
    {
        public string idLead
        {
            get;
            set;
        }

        public string DescLead
        {
            get;
            set;
        }

        public string Entidade
        {
            get;
            set;
        }

        public string Resumo
        {
            get;
            set;
        }

        public string TipoEntidade
        {
            get;
            set;
        }

        public string Vendedor
        {
            get;
            set;
        }

        public string Oportunidade
        {
            get;
            set;
        }

        public double ValorTotalOV
        {
            get;
            set;
        }

        public DateTime DataFecho
        {
            get;
            set;
        }



    }
}