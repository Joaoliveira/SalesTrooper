using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Interop.ErpBS800;
using Interop.StdPlatBS800;
using Interop.StdBE800;
using Interop.GcpBE800;
using Interop.CrmBE800;
using ADODB;
using Interop.IGcpBS800;
//using Interop.StdBESql800;
//using Interop.StdBSSql800;

namespace FirstREST.Lib_Primavera
{
    public class PriIntegration
    {


        # region Cliente

        public static List<Model.Cliente> ListaClientes()
        {


            StdBELista objList;

            List<Model.Cliente> listClientes = new List<Model.Cliente>();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {

                //objList = PriEngine.Engine.Comercial.Clientes.LstClientes();

                objList = PriEngine.Engine.Consulta("SELECT Cliente, Nome, Moeda, NumContrib as NumContribuinte, Fac_Mor AS campo_exemplo, Descricao  FROM  CLIENTES");


                while (!objList.NoFim())
                {
                    listClientes.Add(new Model.Cliente
                    {
                        CodCliente = objList.Valor("Cliente"),
                        NomeCliente = objList.Valor("Nome"),
                        Moeda = objList.Valor("Moeda"),
                        NumContribuinte = objList.Valor("NumContribuinte"),
                        Morada = objList.Valor("campo_exemplo"),
                        Descricao = objList.Valor("Descricao")
                    });
                    objList.Seguinte();

                }

                return listClientes;
            }
            else
                return null;
        }

        public static Lib_Primavera.Model.Cliente GetCliente(string codCliente)
        {


            StdBELista objCli = new StdBELista();


            Model.Cliente myCli = new Model.Cliente();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {

                if (PriEngine.Engine.Comercial.Clientes.Existe(codCliente) == true)
                {
                    objCli = PriEngine.Engine.Consulta("SELECT * FROM Clientes WHERE Cliente =" + "\'" + codCliente + "\'");
                    myCli.CodCliente = objCli.Valor("Cliente");
                    myCli.NomeCliente = objCli.Valor("Nome");
                    myCli.Moeda = objCli.Valor("Moeda");
                    myCli.NumContribuinte = objCli.Valor("NumContrib");
                    myCli.Morada = objCli.Valor("Fac_Mor");
                    myCli.Descricao = objCli.Valor("Descricao");
                    return myCli;
                }
                else
                {
                    return null;
                }
            }
            else
                return null;
        }

        public static Lib_Primavera.Model.RespostaErro UpdCliente(Lib_Primavera.Model.Cliente cliente)
        {
            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();


            GcpBECliente objCli = new GcpBECliente();

            try
            {

                if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
                {

                    if (PriEngine.Engine.Comercial.Clientes.Existe(cliente.CodCliente) == false)
                    {
                        erro.Erro = 1;
                        erro.Descricao = "O cliente não existe";
                        return erro;
                    }
                    else
                    {

                        objCli = PriEngine.Engine.Comercial.Clientes.Edita(cliente.CodCliente);
                        objCli.set_EmModoEdicao(true);


                        objCli.set_Nome(cliente.NomeCliente);
                        objCli.set_NumContribuinte(cliente.NumContribuinte);
                        objCli.set_Moeda(cliente.Moeda);
                        objCli.set_Morada(cliente.Morada);
                        objCli.set_Descricao(cliente.Descricao);

                        PriEngine.Engine.Comercial.Clientes.Actualiza(objCli);


                        erro.Erro = 0;
                        erro.Descricao = "Sucesso";
                        return erro;
                    }
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir a empresa";
                    return erro;

                }

            }

            catch (Exception ex)
            {
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }

        }


        public static Lib_Primavera.Model.RespostaErro DelCliente(string codCliente)
        {

            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();
            GcpBECliente objCli = new GcpBECliente();


            try
            {

                if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
                {
                    if (PriEngine.Engine.Comercial.Clientes.Existe(codCliente) == false)
                    {
                        erro.Erro = 1;
                        erro.Descricao = "O cliente não existe";
                        return erro;
                    }
                    else
                    {

                        PriEngine.Engine.Comercial.Clientes.Remove(codCliente);
                        erro.Erro = 0;
                        erro.Descricao = "Sucesso";
                        return erro;
                    }
                }

                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir a empresa";
                    return erro;
                }
            }

            catch (Exception ex)
            {
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }

        }

        //leads por cliente
        public static List<Model.Lead> LeadsCliente(string codCliente)
        {
            StdBELista objList;

            List<Model.Lead> listLeads = new List<Model.Lead>();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                objList = PriEngine.Engine.Consulta("SELECT ID, Descricao, Entidade, TipoEntidade FROM CabecOportunidadesVenda WHERE Entidade = " + "\'" + codCliente + "\'");


                while (!objList.NoFim())
                {
                    listLeads.Add(new Model.Lead
                    {
                        idLead = objList.Valor("ID"),
                        DescLead = objList.Valor("Descricao"),
                        Entidade = objList.Valor("Entidade"),
                        TipoEntidade = objList.Valor("TipoEntidade")
                    });
                    objList.Seguinte();

                }

                return listLeads;
            }
            else
                return null;
        }

        public static Lib_Primavera.Model.RespostaErro InsereClienteObj(Model.Cliente cli)
        {

            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();


            GcpBECliente myCli = new GcpBECliente();

            try
            {
                if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
                {

                    myCli.set_Cliente(cli.CodCliente);
                    myCli.set_Nome(cli.NomeCliente);
                    myCli.set_NumContribuinte(cli.NumContribuinte);
                    myCli.set_Moeda(cli.Moeda);
                    myCli.set_Morada(cli.Morada);
                    myCli.set_Descricao(cli.Descricao);
                    myCli.set_Vendedor(cli.Vendedor);

                    PriEngine.Engine.Comercial.Clientes.Actualiza(myCli);

                    erro.Erro = 0;
                    erro.Descricao = "Sucesso";
                    return erro;
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir empresa";
                    return erro;
                }
            }

            catch (Exception ex)
            {
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }


        }



        #endregion Cliente;   // -----------------------------  END   CLIENTE    -----------------------


        #region Vendedor


        public static List<Model.Vendedor> ListaVendedores()
        {


            StdBELista objList;

            List<Model.Vendedor> listVendedores = new List<Model.Vendedor>();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {

                //objList = PriEngine.Engine.Comercial.Clientes.LstClientes();

                objList = PriEngine.Engine.Consulta("SELECT Vendedor, Nome, Morada, Telefone, EMail, Notas, Localidade FROM  VENDEDORES");


                while (!objList.NoFim())
                {
                    listVendedores.Add(new Model.Vendedor
                    {
                        CodVendedor = objList.Valor("Vendedor"),
                        NomeVendedor = objList.Valor("Nome"),
                        MoradaVendedor = objList.Valor("Morada"),
                        TelefoneVendedor = objList.Valor("Telefone"),
                        EmailVendedor = objList.Valor("EMail"),
                        NotasVendedor = objList.Valor("Notas"),
                        LocalidadeVendedor = objList.Valor("Localidade")
                    });
                    objList.Seguinte();

                }

                return listVendedores;
            }
            else
                return null;
        }

        public static Lib_Primavera.Model.Vendedor GetVendedor(string id)
        {
            StdBELista objVen = new StdBELista();


            Model.Vendedor myVend = new Model.Vendedor();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {

                if (PriEngine.Engine.Comercial.Vendedores.Existe(id) == true)
                {
                    objVen = PriEngine.Engine.Consulta("SELECT * FROM Vendedores WHERE Vendedor = " + "\'" + id + "\'");
                    myVend.CodVendedor = objVen.Valor("Vendedor");
                    myVend.NomeVendedor = objVen.Valor("Nome");
                    myVend.MoradaVendedor = objVen.Valor("Morada");
                    myVend.TelefoneVendedor = objVen.Valor("Telefone");
                    myVend.EmailVendedor = objVen.Valor("EMail");
                    myVend.NotasVendedor = objVen.Valor("Notas");
                    myVend.LocalidadeVendedor = objVen.Valor("Localidade");

                    return myVend;
                }
                else
                {
                    return null;
                }
            }
            else
                return null;

        }

        public static List<Model.Lead> LeadsVendedor(string id)
        {
            StdBELista objList;

            List<Model.Lead> listLeads = new List<Model.Lead>();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                objList = PriEngine.Engine.Consulta("SELECT ID, Descricao, Entidade, TipoEntidade, EstadoVenda FROM CabecOportunidadesVenda WHERE Vendedor = " + "\'" + id + "\'");


                while (!objList.NoFim())
                {
                    listLeads.Add(new Model.Lead
                    {
                        idLead = objList.Valor("ID"),
                        DescLead = objList.Valor("Descricao"),
                        Entidade = objList.Valor("Entidade"),
                        TipoEntidade = objList.Valor("TipoEntidade"),
                        Estado = objList.Valor("EstadoVenda")
                    });
                    objList.Seguinte();

                }

                return listLeads;
            }
            else
                return null;
        }

        public static List<Model.Cliente> GetVendedorClientes(string id)
        {


            StdBELista objList;

            List<Model.Cliente> listClientes = new List<Model.Cliente>();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {

                //objList = PriEngine.Engine.Comercial.Clientes.LstClientes();

                objList = PriEngine.Engine.Consulta("SELECT Cliente, Nome, Moeda, NumContrib as NumContribuinte, Fac_Mor AS campo_exemplo FROM  CLIENTES WHERE Vendedor = " + "\'" + id + "\'");


                while (!objList.NoFim())
                {
                    listClientes.Add(new Model.Cliente
                    {
                        CodCliente = objList.Valor("Cliente"),
                        NomeCliente = objList.Valor("Nome"),
                        Moeda = objList.Valor("Moeda"),
                        NumContribuinte = objList.Valor("NumContribuinte"),
                        Morada = objList.Valor("campo_exemplo")
                    });
                    objList.Seguinte();

                }

                return listClientes;
            }
            else
                return null;
        }

        public static List<Model.Tarefa> GetVendedorTarefas(string id, string dataInicio, string dataFim)
        {


            StdBELista objList;

            List<Model.Tarefa> listTarefas = new List<Model.Tarefa>();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {

                //objList = PriEngine.Engine.Comercial.Clientes.LstClientes();

                objList = PriEngine.Engine.Consulta("SELECT Tarefas.* FROM Tarefas, CabecOportunidadesVenda WHERE Vendedor = " + "\'" + id + "\' AND  IdCabecOVenda = CabecOportunidadesVenda.ID"
                        + " AND DataInicio >= \'" + dataInicio + "\'"
                        + " AND DataFim <= \'" + dataFim + "\'"
                    );


                while (!objList.NoFim())
                {
                    listTarefas.Add(new Model.Tarefa
                    {
                        Id = objList.Valor("Id"),
                        TipoAtividade = objList.Valor("IdTipoActividade"),
                        Prioridade = objList.Valor("Prioridade"),
                        Estado = objList.Valor("Estado"),
                        Resumo = objList.Valor("Resumo"),
                        Descricao = objList.Valor("Descricao"),
                        EntidadePrincipal = objList.Valor("EntidadePrincipal"),
                        idContactoPrincipal = objList.Valor("IdContactoPrincipal"),
                        DataDeInicio = objList.Valor("DataInicio"),
                        DataDeFim = objList.Valor("DataFim"),
                        LocalRealizacao = objList.Valor("LocalRealizacao"),
                        Utilizador = objList.Valor("Utilizador"),
                        DataUltimaAtualizacao = objList.Valor("DataUltAct"),
                        TodoDia = objList.Valor("TodoDia"),
                        PeriodoAntecedencia = objList.Valor("PeriodoAntecedencia"),
                        ResponsavelPor = objList.Valor("ResponsavelPor"),
                        idCabecalhoOportunidadeVenda = objList.Valor("IDCabecOVenda"),
                        DataLimiteRealizacao = objList.Valor("DataLimiteRealizacao")
                    });
                    objList.Seguinte();

                }

                return listTarefas;
            }
            else
                return null;
        }


        #endregion Vendedor;

        #region Artigo

        public static Lib_Primavera.Model.Artigo GetArtigo(string codArtigo)
        {

            GcpBEArtigo objArtigo = new GcpBEArtigo();
            Model.Artigo myArt = new Model.Artigo();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {

                if (PriEngine.Engine.Comercial.Artigos.Existe(codArtigo) == false)
                {
                    return null;
                }
                else
                {
                    objArtigo = PriEngine.Engine.Comercial.Artigos.Edita(codArtigo);
                    myArt.CodArtigo = objArtigo.get_Artigo();
                    myArt.DescArtigo = objArtigo.get_Descricao();

                    return myArt;
                }

            }
            else
            {
                return null;
            }

        }

        public static List<Model.Artigo> ListaArtigos()
        {

            StdBELista objList;

            Model.Artigo art = new Model.Artigo();
            List<Model.Artigo> listArts = new List<Model.Artigo>();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {

                objList = PriEngine.Engine.Comercial.Artigos.LstArtigos();

                while (!objList.NoFim())
                {
                    art = new Model.Artigo();
                    art.CodArtigo = objList.Valor("artigo");
                    art.DescArtigo = objList.Valor("descricao");

                    listArts.Add(art);
                    objList.Seguinte();
                }

                return listArts;

            }
            else
            {
                return null;

            }

        }

        #endregion Artigo

        #region Lead
        public static List<Model.Lead> ListaLeads()
        {


            StdBELista objList;

            List<Model.Lead> listLeads = new List<Model.Lead>();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {

                //objList = PriEngine.Engine.Comercial.Clientes.LstClientes();

                objList = PriEngine.Engine.Consulta("SELECT ID, Descricao, Entidade, TipoEntidade, Resumo, ValorTotalOV, DataFecho, DataCriacao FROM CabecOportunidadesVenda");


                while (!objList.NoFim())
                {
                    listLeads.Add(new Model.Lead
                    {
                        idLead = objList.Valor("ID"),
                        DescLead = objList.Valor("Descricao"),
                        Entidade = objList.Valor("Entidade"),
                        TipoEntidade = objList.Valor("TipoEntidade"),
                        Resumo = objList.Valor("Resumo"),
                        ValorTotalOV = objList.Valor("ValorTotalOV"),
                        //mudar para DataFecho depois, quando nao houver nulls
                        DataFecho = objList.Valor("DataCriacao")

                    });
                    objList.Seguinte();

                }

                return listLeads;
            }
            else
                return null;
        }

        public static Lib_Primavera.Model.Lead GetLead(string id)
        {

            StdBELista objList;

            List<Model.Lead> listLeads = new List<Model.Lead>();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                objList = PriEngine.Engine.Consulta("SELECT ID, Descricao, Entidade, TipoEntidade, Resumo, ValorTotalOV, DataFecho, DataCriacao FROM CabecOportunidadesVenda WHERE ID = " + "\'" + id + "\'");


                while (!objList.NoFim())
                {
                    listLeads.Add(new Model.Lead
                    {
                        idLead = objList.Valor("ID"),
                        DescLead = objList.Valor("Descricao"),
                        Entidade = objList.Valor("Entidade"),
                        TipoEntidade = objList.Valor("TipoEntidade"),
                        Resumo = objList.Valor("Resumo"),
                        ValorTotalOV = objList.Valor("ValorTotalOV"),
                        //mudar para DataFecho depois, quando nao houver nulls
                        DataFecho = objList.Valor("DataCriacao")
                    });
                    objList.Seguinte();

                }

                if (listLeads.Count > 0) return listLeads[0];
                else return null;
            }
            else
                return null;


        }


        public static Lib_Primavera.Model.RespostaErro InsereLeadObj(Model.Lead lead)
        {

            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();


            CrmBEOportunidadeVenda myLead = new CrmBEOportunidadeVenda();

            try
            {
                if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
                {
                    myLead.set_ID(Guid.NewGuid().ToString());
                    myLead.set_Oportunidade(lead.Oportunidade);
                    myLead.set_Descricao(lead.DescLead);
                    myLead.set_Entidade(lead.Entidade);
                    myLead.set_Resumo(lead.Resumo);
                    myLead.set_TipoEntidade(lead.TipoEntidade);
                    myLead.set_Vendedor(lead.Vendedor);
                    myLead.set_CicloVenda("CV_HW");
                    myLead.set_DataCriacao(DateTime.Now);
                    myLead.set_DataExpiracao(new DateTime(2100, 12, 12));
                    myLead.set_Moeda("EUR");

                    PriEngine.Engine.CRM.OportunidadesVenda.Actualiza(myLead);


                    erro.Erro = 0;
                    erro.Descricao = "Sucesso";
                    return erro;
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir empresa";
                    return erro;
                }
            }

            catch (Exception ex)
            {
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }


        }
        #endregion Lead

        #region DocCompra


        public static List<Model.DocCompra> VGR_List()
        {

            StdBELista objListCab;
            StdBELista objListLin;
            Model.DocCompra dc = new Model.DocCompra();
            List<Model.DocCompra> listdc = new List<Model.DocCompra>();
            Model.LinhaDocCompra lindc = new Model.LinhaDocCompra();
            List<Model.LinhaDocCompra> listlindc = new List<Model.LinhaDocCompra>();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                objListCab = PriEngine.Engine.Consulta("SELECT id, NumDocExterno, Entidade, DataDoc, NumDoc, TotalMerc, Serie From CabecCompras where TipoDoc='VGR'");
                while (!objListCab.NoFim())
                {
                    dc = new Model.DocCompra();
                    dc.id = objListCab.Valor("id");
                    dc.NumDocExterno = objListCab.Valor("NumDocExterno");
                    dc.Entidade = objListCab.Valor("Entidade");
                    dc.NumDoc = objListCab.Valor("NumDoc");
                    dc.Data = objListCab.Valor("DataDoc");
                    dc.TotalMerc = objListCab.Valor("TotalMerc");
                    dc.Serie = objListCab.Valor("Serie");
                    objListLin = PriEngine.Engine.Consulta("SELECT idCabecCompras, Artigo, Descricao, Quantidade, Unidade, PrecUnit, Desconto1, TotalILiquido, PrecoLiquido, Armazem, Lote from LinhasCompras where IdCabecCompras='" + dc.id + "' order By NumLinha");
                    listlindc = new List<Model.LinhaDocCompra>();

                    while (!objListLin.NoFim())
                    {
                        lindc = new Model.LinhaDocCompra();
                        lindc.IdCabecDoc = objListLin.Valor("idCabecCompras");
                        lindc.CodArtigo = objListLin.Valor("Artigo");
                        lindc.DescArtigo = objListLin.Valor("Descricao");
                        lindc.Quantidade = objListLin.Valor("Quantidade");
                        lindc.Unidade = objListLin.Valor("Unidade");
                        lindc.Desconto = objListLin.Valor("Desconto1");
                        lindc.PrecoUnitario = objListLin.Valor("PrecUnit");
                        lindc.TotalILiquido = objListLin.Valor("TotalILiquido");
                        lindc.TotalLiquido = objListLin.Valor("PrecoLiquido");
                        lindc.Armazem = objListLin.Valor("Armazem");
                        lindc.Lote = objListLin.Valor("Lote");

                        listlindc.Add(lindc);
                        objListLin.Seguinte();
                    }

                    dc.LinhasDoc = listlindc;

                    listdc.Add(dc);
                    objListCab.Seguinte();
                }
            }
            return listdc;
        }


        public static Model.RespostaErro VGR_New(Model.DocCompra dc)
        {
            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();


            GcpBEDocumentoCompra myGR = new GcpBEDocumentoCompra();
            GcpBELinhaDocumentoCompra myLin = new GcpBELinhaDocumentoCompra();
            GcpBELinhasDocumentoCompra myLinhas = new GcpBELinhasDocumentoCompra();

            Interop.GcpBE800.PreencheRelacaoCompras rl = new Interop.GcpBE800.PreencheRelacaoCompras();
            List<Model.LinhaDocCompra> lstlindv = new List<Model.LinhaDocCompra>();

            try
            {
                if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
                {
                    // Atribui valores ao cabecalho do doc
                    //myEnc.set_DataDoc(dv.Data);
                    myGR.set_Entidade(dc.Entidade);
                    myGR.set_NumDocExterno(dc.NumDocExterno);
                    myGR.set_Serie(dc.Serie);
                    myGR.set_Tipodoc("VGR");
                    myGR.set_TipoEntidade("F");
                    // Linhas do documento para a lista de linhas
                    lstlindv = dc.LinhasDoc;
                    PriEngine.Engine.Comercial.Compras.PreencheDadosRelacionados(myGR, rl);
                    foreach (Model.LinhaDocCompra lin in lstlindv)
                    {
                        PriEngine.Engine.Comercial.Compras.AdicionaLinha(myGR, lin.CodArtigo, lin.Quantidade, lin.Armazem, "", lin.PrecoUnitario, lin.Desconto);
                    }


                    PriEngine.Engine.IniciaTransaccao();
                    PriEngine.Engine.Comercial.Compras.Actualiza(myGR, "Teste");
                    PriEngine.Engine.TerminaTransaccao();
                    erro.Erro = 0;
                    erro.Descricao = "Sucesso";
                    return erro;
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir empresa";
                    return erro;

                }

            }
            catch (Exception ex)
            {
                PriEngine.Engine.DesfazTransaccao();
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }
        }


        #endregion DocCompra


        #region DocsVenda

        public static Model.RespostaErro Encomendas_New(Model.DocVenda dv)
        {
            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();
            GcpBEDocumentoVenda myEnc = new GcpBEDocumentoVenda();

            GcpBELinhaDocumentoVenda myLin = new GcpBELinhaDocumentoVenda();

            GcpBELinhasDocumentoVenda myLinhas = new GcpBELinhasDocumentoVenda();

            Interop.GcpBE800.PreencheRelacaoVendas rl = new Interop.GcpBE800.PreencheRelacaoVendas();
            List<Model.LinhaDocVenda> lstlindv = new List<Model.LinhaDocVenda>();

            try
            {
                if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
                {
                    // Atribui valores ao cabecalho do doc
                    //myEnc.set_DataDoc(dv.Data);
                    myEnc.set_Entidade(dv.Entidade);
                    myEnc.set_Serie(dv.Serie);
                    myEnc.set_Tipodoc("ECL");
                    myEnc.set_TipoEntidade("C");
                    // Linhas do documento para a lista de linhas
                    lstlindv = dv.LinhasDoc;
                    PriEngine.Engine.Comercial.Vendas.PreencheDadosRelacionados(myEnc, rl);
                    foreach (Model.LinhaDocVenda lin in lstlindv)
                    {
                        PriEngine.Engine.Comercial.Vendas.AdicionaLinha(myEnc, lin.CodArtigo, lin.Quantidade, "", "", lin.PrecoUnitario, lin.Desconto);
                    }


                    // PriEngine.Engine.Comercial.Compras.TransformaDocumento(

                    PriEngine.Engine.IniciaTransaccao();
                    PriEngine.Engine.Comercial.Vendas.Actualiza(myEnc, "Teste");
                    PriEngine.Engine.TerminaTransaccao();
                    erro.Erro = 0;
                    erro.Descricao = "Sucesso";
                    return erro;
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir empresa";
                    return erro;

                }

            }
            catch (Exception ex)
            {
                PriEngine.Engine.DesfazTransaccao();
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }
        }



        public static List<Model.DocVenda> Encomendas_List()
        {

            StdBELista objListCab;
            StdBELista objListLin;
            Model.DocVenda dv = new Model.DocVenda();
            List<Model.DocVenda> listdv = new List<Model.DocVenda>();
            Model.LinhaDocVenda lindv = new Model.LinhaDocVenda();
            List<Model.LinhaDocVenda> listlindv = new
            List<Model.LinhaDocVenda>();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                objListCab = PriEngine.Engine.Consulta("SELECT id, Entidade, Data, NumDoc, TotalMerc, Serie From CabecDoc where TipoDoc='ECL'");
                while (!objListCab.NoFim())
                {
                    dv = new Model.DocVenda();
                    dv.id = objListCab.Valor("id");
                    dv.Entidade = objListCab.Valor("Entidade");
                    dv.NumDoc = objListCab.Valor("NumDoc");
                    dv.Data = objListCab.Valor("Data");
                    dv.TotalMerc = objListCab.Valor("TotalMerc");
                    dv.Serie = objListCab.Valor("Serie");
                    objListLin = PriEngine.Engine.Consulta("SELECT idCabecDoc, Artigo, Descricao, Quantidade, Unidade, PrecUnit, Desconto1, TotalILiquido, PrecoLiquido from LinhasDoc where IdCabecDoc='" + dv.id + "' order By NumLinha");
                    listlindv = new List<Model.LinhaDocVenda>();

                    while (!objListLin.NoFim())
                    {
                        lindv = new Model.LinhaDocVenda();
                        lindv.IdCabecDoc = objListLin.Valor("idCabecDoc");
                        lindv.CodArtigo = objListLin.Valor("Artigo");
                        lindv.DescArtigo = objListLin.Valor("Descricao");
                        lindv.Quantidade = objListLin.Valor("Quantidade");
                        lindv.Unidade = objListLin.Valor("Unidade");
                        lindv.Desconto = objListLin.Valor("Desconto1");
                        lindv.PrecoUnitario = objListLin.Valor("PrecUnit");
                        lindv.TotalILiquido = objListLin.Valor("TotalILiquido");
                        lindv.TotalLiquido = objListLin.Valor("PrecoLiquido");

                        listlindv.Add(lindv);
                        objListLin.Seguinte();
                    }

                    dv.LinhasDoc = listlindv;
                    listdv.Add(dv);
                    objListCab.Seguinte();
                }
            }
            return listdv;
        }




        public static Model.DocVenda Encomenda_Get(string numdoc)
        {


            StdBELista objListCab;
            StdBELista objListLin;
            Model.DocVenda dv = new Model.DocVenda();
            Model.LinhaDocVenda lindv = new Model.LinhaDocVenda();
            List<Model.LinhaDocVenda> listlindv = new List<Model.LinhaDocVenda>();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {


                string st = "SELECT id, Entidade, Data, NumDoc, TotalMerc, Serie From CabecDoc where TipoDoc='ECL' and NumDoc='" + numdoc + "'";
                objListCab = PriEngine.Engine.Consulta(st);
                dv = new Model.DocVenda();
                dv.id = objListCab.Valor("id");
                dv.Entidade = objListCab.Valor("Entidade");
                dv.NumDoc = objListCab.Valor("NumDoc");
                dv.Data = objListCab.Valor("Data");
                dv.TotalMerc = objListCab.Valor("TotalMerc");
                dv.Serie = objListCab.Valor("Serie");
                objListLin = PriEngine.Engine.Consulta("SELECT idCabecDoc, Artigo, Descricao, Quantidade, Unidade, PrecUnit, Desconto1, TotalILiquido, PrecoLiquido from LinhasDoc where IdCabecDoc='" + dv.id + "' order By NumLinha");
                listlindv = new List<Model.LinhaDocVenda>();

                while (!objListLin.NoFim())
                {
                    lindv = new Model.LinhaDocVenda();
                    lindv.IdCabecDoc = objListLin.Valor("idCabecDoc");
                    lindv.CodArtigo = objListLin.Valor("Artigo");
                    lindv.DescArtigo = objListLin.Valor("Descricao");
                    lindv.Quantidade = objListLin.Valor("Quantidade");
                    lindv.Unidade = objListLin.Valor("Unidade");
                    lindv.Desconto = objListLin.Valor("Desconto1");
                    lindv.PrecoUnitario = objListLin.Valor("PrecUnit");
                    lindv.TotalILiquido = objListLin.Valor("TotalILiquido");
                    lindv.TotalLiquido = objListLin.Valor("PrecoLiquido");
                    listlindv.Add(lindv);
                    objListLin.Seguinte();
                }

                dv.LinhasDoc = listlindv;
                return dv;
            }
            return null;
        }

        #endregion DocsVenda

        #region Tarefas

        public static List<Model.Tarefa> ListaTarefas()
        {


            StdBELista objList;

            List<Model.Tarefa> listTarefas = new List<Model.Tarefa>();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {


                objList = PriEngine.Engine.Consulta("SELECT * FROM  TAREFAS");


                while (!objList.NoFim())
                {
                    listTarefas.Add(new Model.Tarefa
                    {
                        Id = objList.Valor("Id"),
                        TipoAtividade = objList.Valor("IdTipoActividade"),
                        Prioridade = objList.Valor("Prioridade"),
                        Estado = objList.Valor("Estado"),
                        Resumo = objList.Valor("Resumo"),
                        Descricao = objList.Valor("Descricao"),
                        EntidadePrincipal = objList.Valor("EntidadePrincipal"),
                        idContactoPrincipal = objList.Valor("IdContactoPrincipal"),
                        DataDeInicio = objList.Valor("DataInicio"),
                        DataDeFim = objList.Valor("DataFim"),
                        LocalRealizacao = objList.Valor("LocalRealizacao"),
                        Utilizador = objList.Valor("Utilizador"),
                        DataUltimaAtualizacao = objList.Valor("DataUltAct"),
                        TodoDia = objList.Valor("TodoDia"),
                        PeriodoAntecedencia = objList.Valor("PeriodoAntecedencia"),
                        ResponsavelPor = objList.Valor("ResponsavelPor"),
                        idCabecalhoOportunidadeVenda = objList.Valor("IDCabecOVenda"),
                        DataLimiteRealizacao = objList.Valor("DataLimiteRealizacao")
                    });
                    objList.Seguinte();

                }

                return listTarefas;
            }
            else
                return null;
        }

        public static Lib_Primavera.Model.Tarefa GetTarefa(string id)
        {


            StdBELista objList = new StdBELista();


            Model.Tarefa myTar = new Model.Tarefa();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                objList = PriEngine.Engine.Consulta("SELECT * FROM TAREFAS WHERE Id =" + "\'" + id + "\'");


                if (objList.NumLinhas() == 1)
                {
                    myTar.Id = objList.Valor("Id");
                    myTar.TipoAtividade = objList.Valor("IdTipoActividade");
                    myTar.Prioridade = objList.Valor("Prioridade");
                    myTar.Estado = objList.Valor("Estado");
                    myTar.Resumo = objList.Valor("Resumo");
                    myTar.Descricao = objList.Valor("Descricao");
                    myTar.EntidadePrincipal = objList.Valor("EntidadePrincipal");
                    myTar.idContactoPrincipal = objList.Valor("idContactoPrincipal");
                    myTar.DataDeInicio = objList.Valor("DataInicio");
                    myTar.DataDeFim = objList.Valor("DataFim");
                    myTar.LocalRealizacao = objList.Valor("LocalRealizacao");
                    myTar.Utilizador = objList.Valor("Utilizador");
                    myTar.DataUltimaAtualizacao = objList.Valor("DataUltAct");
                    myTar.TodoDia = objList.Valor("TodoDia");
                    myTar.PeriodoAntecedencia = objList.Valor("PeriodoAntecedencia");
                    myTar.ResponsavelPor = objList.Valor("ResponsavelPor");
                    myTar.idCabecalhoOportunidadeVenda = objList.Valor("IDCabecOVenda");
                    myTar.DataLimiteRealizacao = objList.Valor("DataLimiteRealizacao");
                    return myTar;
                }
                else
                {
                    return null;
                }
            }
            else
                return null;
        }

        public static List<Lib_Primavera.Model.Tarefa> GetTarefasLead(string idLead)
        {
            StdBELista objList = new StdBELista();

            List<Lib_Primavera.Model.Tarefa> results = new List<Model.Tarefa>();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(),
                FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                objList = PriEngine.Engine.Consulta("SELECT * FROM Tarefas WHERE IDCabecOVenda = \'" + idLead + "\'");

                while (!objList.NoFim())
                {
                    results.Add(new Model.Tarefa
                    {
                        Id = objList.Valor("Id"),
                        TipoAtividade = objList.Valor("IdTipoActividade"),
                        Prioridade = objList.Valor("Prioridade"),
                        Estado = objList.Valor("Estado"),
                        Resumo = objList.Valor("Resumo"),
                        Descricao = objList.Valor("Descricao"),
                        EntidadePrincipal = objList.Valor("EntidadePrincipal"),
                        idContactoPrincipal = objList.Valor("IdContactoPrincipal"),
                        DataDeInicio = objList.Valor("DataInicio"),
                        DataDeFim = objList.Valor("DataFim"),
                        LocalRealizacao = objList.Valor("LocalRealizacao"),
                        Utilizador = objList.Valor("Utilizador"),
                        DataUltimaAtualizacao = objList.Valor("DataUltAct"),
                        TodoDia = objList.Valor("TodoDia"),
                        PeriodoAntecedencia = objList.Valor("PeriodoAntecedencia"),
                        ResponsavelPor = objList.Valor("ResponsavelPor"),
                        idCabecalhoOportunidadeVenda = objList.Valor("IDCabecOVenda"),
                        DataLimiteRealizacao = objList.Valor("DataLimiteRealizacao")
                    });
                    objList.Seguinte();
                }

                return results;
            }

            return null;

        }

        public static Lib_Primavera.Model.RespostaErro UpdTarefa(string id, string value)
        {
            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();


            CrmBEActividade objAtiv = new CrmBEActividade();

            try
            {

                if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
                {
                    if (PriEngine.Engine.CRM.Actividades.Existe(id) == false)
                    {
                        erro.Erro = 1;
                        erro.Descricao = "A tarefa não existe";
                        return erro;
                    }
                    else
                    {

                        objAtiv = PriEngine.Engine.CRM.Actividades.Edita(id);
                        objAtiv.set_EmModoEdicao(true);

                        objAtiv.set_Estado(value);

                        PriEngine.Engine.CRM.Actividades.Actualiza(objAtiv);


                        erro.Erro = 0;
                        erro.Descricao = "Sucesso";
                        return erro;
                    }
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir a empresa";
                    return erro;

                }

            }

            catch (Exception ex)
            {
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }
        }

        public static Lib_Primavera.Model.RespostaErro InsereTarefaObj(Model.Tarefa tarefa)
        {

            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();


            CrmBEActividade myAct = new CrmBEActividade();// Actividades

            try
            {
                if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
                {

                    myAct.set_ID(tarefa.Id);
                    myAct.set_IDTipoActividade(tarefa.TipoAtividade);
                    //myAct.set_Prioridade(tarefa.Prioridade);// miss match de parametros
                    //myAct.set_Estado(tarefa.Estado);// miss match de parametros
                    myAct.set_Resumo(tarefa.Resumo);
                    myAct.set_Descricao(tarefa.Descricao);
                    myAct.set_EntidadePrincipal(tarefa.EntidadePrincipal);
                    //myAct.set_Contacto(tarefa.Contacto); // nao existe
                    myAct.set_IDContactoPrincipal(tarefa.idContactoPrincipal);
                    myAct.set_DataInicio(tarefa.DataDeInicio);
                    myAct.set_DataFim(tarefa.DataDeFim);
                    myAct.set_LocalRealizacao(tarefa.LocalRealizacao);
                    myAct.set_Utilizador(tarefa.Utilizador);
                    myAct.set_DataUltAct(tarefa.DataUltimaAtualizacao);
                    myAct.set_TodoDia(tarefa.TodoDia);
                    //myAct.set_PeriodoAntecedencia(tarefa.PeriodoAntecedencia);// miss match de parametros
                    //myAct.set_ResponsavelPor(tarefa.ResponsavelPor); // nao existe
                    myAct.set_IDCabecOVenda(tarefa.idCabecalhoOportunidadeVenda);
                    //myAct.set_DataLimiteRealizacao(tarefa.DataLimiteRealizacao); //miss match de parametros

                    PriEngine.Engine.CRM.Actividades.Actualiza(myAct);

                    erro.Erro = 0;
                    erro.Descricao = "Sucesso";
                    return erro;
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir empresa";
                    return erro;
                }
            }

            catch (Exception ex)
            {
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }


        }





        #endregion Tarefas

        #region IterTarefas

        public static List<Model.IterTarefa> ListaIterTarefas()
        {

            StdBELista objList;

            List<Model.IterTarefa> listIterTarefas = new List<Model.IterTarefa>();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {


                objList = PriEngine.Engine.Consulta("SELECT * FROM  TAREFASATTACH");


                while (!objList.NoFim())
                {
                    listIterTarefas.Add(new Model.IterTarefa
                    {
                        Id = objList.Valor("Id"),
                        IdTarefa = objList.Valor("idTarefa"),
                        Descricao = objList.Valor("Descricao"),
                        Utilizador = objList.Valor("Utilizador"),
                        //Data = objList.Valor("Data")                
                    });
                    objList.Seguinte();

                }

                return listIterTarefas;
            }
            else
                return null;
        }

        public static Lib_Primavera.Model.IterTarefa GetIterTarefa(string id)
        {


            StdBELista objList = new StdBELista();


            Model.IterTarefa myIterTar = new Model.IterTarefa();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                objList = PriEngine.Engine.Consulta("SELECT * FROM TAREFASATTACH WHERE Id =" + "\'" + id + "\'");


                if (objList.NumLinhas() == 1)
                {
                    myIterTar.Id = objList.Valor("Id");
                    myIterTar.IdTarefa = objList.Valor("idTarefa");
                    myIterTar.Descricao = objList.Valor("Descricao");
                    myIterTar.Utilizador = objList.Valor("Utilizador");
                   // myIterTar.Data = objList.Valor("Data");
                    
                    return myIterTar;
                }
                else
                {
                    return null;
                }
            }
            else
                return null;
        }

        public static List<Lib_Primavera.Model.IterTarefa> GetIterTarefasTarefa(string idTarefa)
        {
            StdBELista objList = new StdBELista();

            List<Lib_Primavera.Model.IterTarefa> results = new List<Model.IterTarefa>();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(),
                FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                objList = PriEngine.Engine.Consulta("SELECT * FROM TAREFASATTACH WHERE IdTarefa = \'" + idTarefa + "\'");

                while (!objList.NoFim())
                {
                    results.Add(new Model.IterTarefa
                    {
                        Id = objList.Valor("Id"),
                        IdTarefa = objList.Valor("idTarefa"),
                        Descricao = objList.Valor("Descricao"),
                        Utilizador = objList.Valor("Utilizador"),
                        //Data = objList.Valor("Data"),
                        VersaoUltAct = objList.Valor("VersaoUltAct")
                    });
                    objList.Seguinte();
                }

                return results;
            }

            return null;

        }

        public static Lib_Primavera.Model.RespostaErro UpdIterTarefa(string id, string value)
        {
            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();


            CrmBEActividade objAtiv = new CrmBEActividade();

            try
            {

                if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
                {
                    if (PriEngine.Engine.CRM.Actividades.Existe(id) == false)
                    {
                        erro.Erro = 1;
                        erro.Descricao = "A Itertarefa não existe";
                        return erro;
                    }
                    else
                    {

                        objAtiv = PriEngine.Engine.CRM.Actividades.Edita(id);
                        objAtiv.set_EmModoEdicao(true);

                        objAtiv.set_Estado(value);

                        PriEngine.Engine.CRM.Actividades.Actualiza(objAtiv);


                        erro.Erro = 0;
                        erro.Descricao = "Sucesso";
                        return erro;
                    }
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir a empresa";
                    return erro;

                }

            }

            catch (Exception ex)
            {
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }
        }

        public static Lib_Primavera.Model.RespostaErro InsereIterTarefaObj(Model.IterTarefa iterTarefa)
        {

            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();


            CrmBEActividade myAct = new CrmBEActividade();// Actividades

            try
            {
                if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
                {

                    myAct.set_ID(iterTarefa.Id);
                    myAct.set_IDActividadeOrigem(iterTarefa.IdTarefa);
                    myAct.set_Descricao(iterTarefa.Descricao);
                    myAct.set_Utilizador(iterTarefa.Utilizador);
                    //myAct.set_DataInicio(iterTarefa.Data);
                   

                    PriEngine.Engine.CRM.Actividades.Actualiza(myAct);

                    erro.Erro = 0;
                    erro.Descricao = "Sucesso";
                    return erro;
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir empresa";
                    return erro;
                }
            }

            catch (Exception ex)
            {
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }


        }

        #endregion IterTarefas

        #region Faturas
        internal static IEnumerable<Model.Fatura> FaturasCliente(string id)
        {
            StdBELista objList;

            List<Model.Fatura> listFaturas = new List<Model.Fatura>();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                objList = PriEngine.Engine.Consulta(
                    "SELECT Id, Data, NumDoc, TotalMerc, TotalIva, TotalOutros, Moeda, DataVencimento FROM CabecDoc WHERE " +
                    "TipoEntidade = \'C\' AND " +
                    "TipoDoc = \'FA\' AND " +
                    "Entidade =\'" + id + "\'");


                while (!objList.NoFim())
                {
                    listFaturas.Add(new Model.Fatura
                    {
                        Id = objList.Valor("Id"),
                        Data = objList.Valor("Data"),
                        NumDoc = objList.Valor("NumDoc"),
                        TotalMerc = objList.Valor("TotalMerc"),
                        TotalIva = objList.Valor("TotalIva"),
                        TotalOutros = objList.Valor("TotalOutros"),
                        Moeda = objList.Valor("Moeda"),
                        DataVencimento = objList.Valor("DataVencimento")
                    });
                    objList.Seguinte();

                }

                return listFaturas;
            }
            else
                return null;
        }
        #endregion Faturas
    }
}
