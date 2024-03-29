﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using ViverMais.DAO;
using System.Data;
using ViverMais.Model;
using System.IO;

namespace ViverMais.View.Urgencia
{
    public partial class _ExibeFilaAtendimento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IUsuarioProfissionalUrgence iUsuarioProfissional = Factory.GetInstance<IUsuarioProfissionalUrgence>();

                Usuario usuario = (Usuario)Session["Usuario"];
                bool acessoMedico = iUsuarioProfissional.VerificarAcessoMedico(usuario.Codigo, "REALIZAR_CONSULTA_MEDICA", usuario.Unidade.CNES);

                if (!acessoMedico)
                {
                    gridFila.Columns.RemoveAt(1);

                    BoundField nomepaciente = new BoundField();
                    nomepaciente.DataField = "NomePaciente";
                    nomepaciente.HeaderText = "Paciente";
                    nomepaciente.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                    nomepaciente.ItemStyle.Width = Unit.Pixel(280);
                    gridFila.Columns.Insert(1, nomepaciente);
                }

                CarregaAtendimentos(true, false);
            }
        }

        /// <summary>
        /// Carrega a fila de atendimento
        /// </summary>
        private void CarregaAtendimentos(bool carregamentoinicial, bool ler_arquivo)
        {
            bool existe_nova_entrada = false;

            if (ler_arquivo)
            {
                var stream = new FileStream(Server.MapPath("~/Urgencia/Documentos/FilaAtendimento.txt"), FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                TextReader leitura = new StreamReader(stream);
                TextWriter escrita = new StreamWriter(stream);

                if (Convert.ToChar(leitura.ReadLine()) == 'S')
                {
                    existe_nova_entrada = true;
                    stream.Seek(0, SeekOrigin.Begin);
                    escrita.WriteLine('N');
                    escrita.Flush();
                }

                stream.Close();
            }

            if (carregamentoinicial || existe_nova_entrada)
            {
                IProntuario iProntuario = Factory.GetInstance<IProntuario>();
                IList<ViverMais.Model.Prontuario> fila = iProntuario.BuscaFilaAtendimentoUnidade<ViverMais.Model.Prontuario>(((ViverMais.Model.Usuario)Session["Usuario"]).Unidade.CNES);
                DataTable tabela = iProntuario.getDataTablePronturario<IList<ViverMais.Model.Prontuario>>(fila.OrderBy(p => p.ClassificacaoRisco.Ordem).ThenByDescending(a => a.EsperaFilaAtendimento).ToList());

                Session["FilaAtendimento"] = fila;
                Session["TabelaFilaAtendimento"] = tabela;
            }

            gridFila.DataSource = (DataTable)Session["TabelaFilaAtendimento"];
            gridFila.DataBind();
        }

        protected void OnTick_Temporizador(object sender, EventArgs e)
        {
            this.CarregaAtendimentos(false, true);
        }

        /// <summary>
        /// Formata o gridview de acordo com a classificação de risco do paciente
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowDataBound_FormataGrid(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                long co_prontuario = long.Parse(this.gridFila.DataKeys[e.Row.RowIndex]["CodigoProntuario"].ToString());
                IList<ViverMais.Model.Prontuario> prontuarios = (IList<ViverMais.Model.Prontuario>)Session["FilaAtendimento"];
                Label labelespera = (Label)e.Row.FindControl("Label_TempoEspera");
                labelespera.Text = prontuarios.Where(p => p.Codigo == co_prontuario).First().EsperaAtualFilaAtendimento;
            }
        }

        protected void OnSelectedIndexChanging_PrimeiraConsultaMedica(object sender, GridViewSelectEventArgs e)
        {
            Session["tempo_atendimento"] = 0;
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Load", "parent.location='FormProntuario.aspx?codigo=" + gridFila.DataKeys[e.NewSelectedIndex]["CodigoProntuario"].ToString() + "';", true);
            //Response.Redirect("FormProntuario.aspx?codigo=" + gridFila.DataKeys[e.NewSelectedIndex]["CodigoProntuario"].ToString());
        }
    }
}
