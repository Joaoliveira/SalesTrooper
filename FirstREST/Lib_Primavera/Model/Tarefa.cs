using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstREST.Lib_Primavera.Model
{
    public class Tarefa
    {
        public string Id
        {
            get;
            set;
        }

        public string IdTipoAtividade
        {
            get;
            set;
        }

        public int Prioridade
        {
            get;
            set;
        }

        public int Estado
        {
            get;
            set;
        }

        public string Resumo
        {
            get;
            set;
        }

        public string Descricao
        {
            get;
            set;
        }

        public string EntidadePrincipal
        {
            get;
            set;
        }

  /*      public string Contacto
        {
            get;
            set;
        }*/


        public string idContactoPrincipal
        {
            get;
            set;
        }

        public System.DateTime DataDeInicio
        {
            get;
            set;
        }

        public System.DateTime DataDeFim
        {
            get;
            set;
        }

        public string LocalRealizacao
        {
            get;
            set;
        }

        public string Utilizador
        {
            get;
            set;
        }

        public System.DateTime DataUltimaAtualizacao
        {
            get;
            set;
        }

        public bool TodoDia
        {
            get;
            set;
        }

        public int PeriodoAntecedencia
        {
            get;
            set;
        }

        public string ResponsavelPor
        {
            get;
            set;
        }

        public string idCabecalhoOportunidadeVenda
        {
            get;
            set;
        }

        public string DataLimiteRealizacao
        {
            get;
            set;
        }

    }
}