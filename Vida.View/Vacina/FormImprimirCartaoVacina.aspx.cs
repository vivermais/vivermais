﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using ViverMais.Model;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Vacina.Misc;
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;

namespace ViverMais.View.Vacina
{
    public partial class FormImprimirCartaoVacina : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["pacienteSelecionado"] != null)
                {
                    CarregaGridView();
                    CarregaAvatares();
                }
            }
        }

        /// <summary>
        /// Recarrega todos os grid dos cartões de vacina.
        /// </summary>
        void CarregaGridView()
        {

            ViverMais.Model.Paciente paciente = (ViverMais.Model.Paciente)Session["pacienteSelecionado"];
            Label_Paciente.Text = paciente.Nome;
            //carrega as vacinas q o paciente ja tomou
            IList<CartaoVacina> cartaovacinapaciente = Factory.GetInstance<ICartaoVacina>().BuscarPorPaciente<CartaoVacina>(paciente.Codigo);
            //carrega todas as vacinas do calendario
            IList<CartaoVacina> listacv = Factory.GetInstance<ICartaoVacina>().CarregaCartaoVacina<CartaoVacina>(paciente.Codigo).ToList();

            var result = from cartao in listacv
                         orderby cartao.Vacina.Nome, cartao.DoseVacina.NumeracaoDose, cartao.DataAplicacao
                         select cartao;
            IList<CartaoVacina> listacartaovacina = result.ToList<CartaoVacina>();

            //percorre a lista de vacinas do calendario
            //e inclui, na lista de vacinas do paciente, os itens
            //remanescentes
            int valor = cartaovacinapaciente.Count;
            foreach (CartaoVacina cv in listacartaovacina)
            {
                for (int i = 0; i < valor; i++)
                {
                    if (cartaovacinapaciente[i].CodigoVacina == cv.CodigoVacina &&
                        cartaovacinapaciente[i].CodigoDoseVacina == cv.CodigoDoseVacina &&
                        cartaovacinapaciente[i].DataAplicacao != null)
                    {
                        //se o cara ja tiver tomado a vacina não inclui na lista
                        break;
                        
                    }
                        cartaovacinapaciente.Add(cv);
                }
            }


            //cada lista representa um cartao de vacina do cidadao
            IList<CartaoVacina> cvcrianca = new List<CartaoVacina>();
            IList<CartaoVacina> cvadolescente = new List<CartaoVacina>();
            IList<CartaoVacina> cvadulto = new List<CartaoVacina>();
            IList<CartaoVacina> cvidoso = new List<CartaoVacina>();

            //Sao testadas as faixas etárias e as vacinas são exibidas em seus respectivos cartões
            //O mesmo item pode constar em mais de um cartao
            foreach (CartaoVacina itemcartao in listacartaovacina)
            {
                //vacinas da criança
                if (Factory.GetInstance<IParametrizacaoVacina>().VerificarRequisitos(itemcartao.Vacina.Codigo, itemcartao.DoseVacina.Codigo, CartaoVacina.CRIANCA))
                {
                    itemcartao.ProximaDose = ProximaDose(itemcartao.Vacina, itemcartao.DoseVacina, itemcartao.DataAplicacao);
                    //itemcartao.SiglaEstabelecimento = string.IsNullOrEmpty(itemcartao.CNESUnidade) ? "" : Factory.GetInstance<IEstabelecimentoSaude>().BuscarEstabelecimentoPorCNES<ViverMais.Model.EstabelecimentoSaude>(itemcartao.CNESUnidade).SiglaEstabelecimento;
                    cvcrianca.Add(itemcartao);
                }
                //vacinas do adolescente
                if (Factory.GetInstance<IParametrizacaoVacina>().VerificarRequisitos(itemcartao.Vacina.Codigo, itemcartao.DoseVacina.Codigo, CartaoVacina.ADOLESCENTE))
                {
                    itemcartao.ProximaDose = ProximaDose(itemcartao.Vacina, itemcartao.DoseVacina, itemcartao.DataAplicacao);
                    //itemcartao.SiglaEstabelecimento = string.IsNullOrEmpty(itemcartao.CNESUnidade) ? "" : Factory.GetInstance<IEstabelecimentoSaude>().BuscarEstabelecimentoPorCNES<ViverMais.Model.EstabelecimentoSaude>(itemcartao.CNESUnidade).SiglaEstabelecimento;
                    cvadolescente.Add(itemcartao);
                }
                //vacinas do adulto
                if (Factory.GetInstance<IParametrizacaoVacina>().VerificarRequisitos(itemcartao.Vacina.Codigo, itemcartao.DoseVacina.Codigo, CartaoVacina.ADULTOIDOSO))
                {
                    itemcartao.ProximaDose = ProximaDose(itemcartao.Vacina, itemcartao.DoseVacina, itemcartao.DataAplicacao);
                    //itemcartao.SiglaEstabelecimento = string.IsNullOrEmpty(itemcartao.CNESUnidade) ? "" : Factory.GetInstance<IEstabelecimentoSaude>().BuscarEstabelecimentoPorCNES<ViverMais.Model.EstabelecimentoSaude>(itemcartao.CNESUnidade).SiglaEstabelecimento;
                    cvadulto.Add(itemcartao);
                }
            }
            while (cvcrianca.Count < 35)
            {
                cvcrianca.Add(new CartaoVacina());
            }
            GridView_CartaoVacina.DataSource = cvcrianca;
            GridView_CartaoVacina.DataBind();

        }

        void CarregaAvatares()
        {
            IList<AvatarCartaoVacina> avatares = Factory.GetInstance<IVacinaServiceFacade>().ListarTodos<AvatarCartaoVacina>();
            DataList_Avatares.DataSource = avatares;
            DataList_Avatares.DataBind();
        }

        string ProximaDose(ViverMais.Model.Vacina vacina, DoseVacina dose, DateTime? dataaplicada)
        {
            IList<ParametrizacaoVacina> parametrizacoes = Factory.GetInstance<IParametrizacaoVacina>().BuscaProximaDose<ParametrizacaoVacina>(dose.Codigo, vacina.Codigo);
            //Add string 'NoRegisters' caso não haja próxima. Desta forma é tratado no Bsn para não add a coluna
            if (parametrizacoes.Count != 0)
            {
                switch (parametrizacoes.Last().UnidadeTempoInicial)
                {
                    //Hora
                    case 1: { return dataaplicada != null ? dataaplicada.Value.AddHours(parametrizacoes.Last().FaixaEtariaInicial).ToString("dd/MM/yyyy") : ""; }
                    //Dia
                    case 2: { return dataaplicada != null ? dataaplicada.Value.AddDays(parametrizacoes.Last().FaixaEtariaInicial).ToString("dd/MM/yyyy") : ""; }
                    //Mês
                    case 3: { return dataaplicada != null ? dataaplicada.Value.AddMonths(Convert.ToInt32(parametrizacoes.Last().FaixaEtariaInicial)).ToString("dd/MM/yyyy") : ""; }
                    //Ano
                    case 4: { return dataaplicada != null ? dataaplicada.Value.AddYears(Convert.ToInt32(parametrizacoes.Last().FaixaEtariaInicial)).ToString("dd/MM/yyyy") : ""; }
                    default: return "";
                }
            }
            else
                return "";
        }

        protected void OnItemDataBound_DataList_Avatares(object sender, DataListItemEventArgs e) 
        {
            int codigo = int.Parse(DataList_Avatares.DataKeys[e.Item.ItemIndex].ToString());
            AvatarCartaoVacina avatar = Factory.GetInstance<IVacinaServiceFacade>().BuscarPorCodigo<AvatarCartaoVacina>(codigo);
            //((Image)e.Item.FindControl("IMG_Avatar")).ImageUrl = avatar.ImagemTopo;
            ((Label)e.Item.FindControl("Label_Avatar")).Text = Server.MapPath("~/Vacina/img/nome_cartao_vacina.png");
        }

        protected void OnClick_IMG_Avatar(object sender, EventArgs e) 
        {
            ImageButton botao = (ImageButton)sender;
            int codigo = int.Parse(botao.CommandArgument.ToString());

            AvatarCartaoVacina avatar = Factory.GetInstance<IVacinaServiceFacade>().BuscarPorCodigo<AvatarCartaoVacina>(codigo);
            //ImagemTopo.ImageUrl = avatar.ImagemTopo;
        }
    }
}