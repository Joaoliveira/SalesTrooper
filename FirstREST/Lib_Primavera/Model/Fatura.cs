using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstREST.Lib_Primavera.Model
{
    public class Fatura
    {
        public string Id
        {
            get;
            set;
        }

        public DateTime Data
        {
            get;
            set;
        }

        public int NumDoc
        {
            get;
            set;
        }

        public double TotalMerc
        {
            get;
            set;
        }

        public double TotalIva
        {
            get;
            set;
        }

        public double TotalOutros
        {
            get;
            set;
        }

        public string Moeda
        {
            get;
            set;
        }

        public DateTime DataVencimento
        {
            get;
            set;
        }

    }
}