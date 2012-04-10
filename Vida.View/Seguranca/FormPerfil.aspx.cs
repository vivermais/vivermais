using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.Model;
using AjaxControlToolkit;
using System.Text;
using System.Globalization;

namespace ViverMais.View.Seguranca
{
    public partial class FormPerfil : PageViverMais
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ISeguranca iseguranca = Factory.GetInstance<ISeguranca>();
                if (!iseguranca.VerificarPermissao(((ViverMais.Model.Usuario)Session["Usuario"]).Codigo, "MANTER_PERFIL", Modulo.SEGURANCA))
                    Response.Redirect("FormAcessoNegado.aspx?opcao=1");
                //ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Você não tem permissão para acessar essa página. Em caso de dúViverMais, entre em contato.');window.location='Default.aspx';</script>");
                else
                {
                    IList<ViverMais.Model.Modulo> modulos = Factory.GetInstance<IViverMaisServiceFacade>().ListarTodos<ViverMais.Model.Modulo>().OrderBy(m => m.Nome).ToList();
                    DropDownList_Sistema.DataSource = modulos;
                    DropDownList_Sistema.DataBind();
                    DropDownList_Sistema.Items.Insert(0, new ListItem("Selecione...", "0"));
                    this.OnSelectedIndexChanged_CarregaOperacoes(new object(), new EventArgs());

                    int temp;
                    IList<HorarioPerfil> horarios = HorariosDefaults();

                    if (Request["co_perfil"] != null && int.TryParse(Request["co_perfil"].ToString(), out temp))
                    {
                        IPerfil iPerfil = Factory.GetInstance<IPerfil>();
                        ViverMais.Model.Perfil perfil = iPerfil.BuscarPorCodigo<ViverMais.Model.Perfil>(int.Parse(Request["co_perfil"].ToString()));
                        if (perfil != null)
                        {
                            TextBox_Nome.Text = perfil.Nome;
                            ckbxProfissionalSaude.Checked = perfil.PerfilProfissionalSaude;
                            if (DropDownList_Sistema.Items.FindByValue(perfil.Modulo.Codigo.ToString()) != null)
                                DropDownList_Sistema.SelectedValue = perfil.Modulo.Codigo.ToString();

                            this.OnSelectedIndexChanged_CarregaOperacoes(new object(), new EventArgs());

                            if (this.CheckBoxList_Operacoes.Items.Count > 0)
                            {
                                foreach (ViverMais.Model.Operacao o in perfil.Operacoes)
                                {
                                    if (this.CheckBoxList_Operacoes.Items.FindByValue(o.Codigo.ToString()) != null)
                                        this.CheckBoxList_Operacoes.Items.FindByValue(o.Codigo.ToString()).Selected = true;
                                }
                            }

                            horarios = iPerfil.BuscarHorarios<HorarioPerfil>(perfil.Codigo);

                            if (horarios.Count() == 0)
                                horarios = this.HorariosDefaults();
                        }

                        LinkButtonVoltar.PostBackUrl = "~/Seguranca/FormBuscaPerfil.aspx";
                    }
                    else
                        LinkButtonVoltar.PostBackUrl = "~/Seguranca/Default.aspx";

                    this.CarregaHorarios(horarios);
                }
            }
        }

        protected void OnSelectedIndexChanged_CarregaOperacoes(object sender, EventArgs e)
        {
            ISeguranca iSeguranca = Factory.GetInstance<ISeguranca>();
            
            IList<Operacao> operacoes = iSeguranca.BuscarOperacaoPorModulo<Operacao>(int.Parse(this.DropDownList_Sistema.SelectedValue));
            this.CheckBoxList_Operacoes.DataSource = operacoes;
            this.CheckBoxList_Operacoes.DataBind();

            if (operacoes.Count() == 0)
            {
                this.SelecionarTodasOperacoes.Visible = false;
                this.EmptyOperacoes.Visible = true;
            }
            else
            {
                this.CheckBox_SelecionarTodos.Checked = false;
                this.SelecionarTodasOperacoes.Visible = true;
                this.EmptyOperacoes.Visible = false;
            }
        }

        protected void OnCheckedChanged_Operacoes(object sender, EventArgs e)
        {
            bool itemchecked = false;

            if (this.CheckBox_SelecionarTodos.Checked)
                itemchecked = true;

            foreach (ListItem item in this.CheckBoxList_Operacoes.Items)
                item.Selected = itemchecked;
        }

        private int[] RetornaDias()
        {
            int[] dias = new int[7];
            for (int i = 1; i < 7; i++)
                dias[i - 1] = i;

            dias[6] = 0;

            return dias;
        }

        private IList<HorarioPerfil> HorariosDefaults()
        {
            IList<HorarioPerfil> horarios = new List<HorarioPerfil>();

            foreach (int dia in this.RetornaDias())
            {
                HorarioPerfil horario = new HorarioPerfil();

                horario.Dia         = dia;
                horario.HoraInicial = "0700";
                horario.HoraFinal   = "1900";

                DayOfWeek dayOfWeek = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), dia.ToString());

                if (dayOfWeek == DayOfWeek.Saturday || dayOfWeek == DayOfWeek.Sunday)
                    horario.Bloqueado = true;

                horarios.Add(horario);
            }

            return horarios;
        }

        private void CarregaHorarios(IList<HorarioPerfil> horarios)
        {
            foreach (HorarioPerfil horario in horarios)
            {
                Label dia = this.Horarios.FindControl("Label_Dia_" + horario.Dia.ToString()) as Label;
                dia.Text = horario.DiaEquivalente;

                DropDownList horainicial = this.Horarios.FindControl("DropDownList_HoraInicial_" + horario.Dia.ToString()) as DropDownList;
                string[] _horarios = this.ListaHorariosDropDown(false);

                for (int i = 0; i < _horarios.Count(); i++)
                    horainicial.Items.Add(new ListItem(_horarios[i].Substring(0, 2) + ":" + _horarios[i].Substring(2, 2), _horarios[i]));

                horainicial.SelectedValue = horario.HoraInicial;
                this.OnSelectedIndexChanged_HorarioFinal(horainicial, new EventArgs());

                DropDownList horafinal = this.Horarios.FindControl("DropDownList_HoraFinal_" + horario.Dia.ToString()) as DropDownList;
                horafinal.SelectedValue = horario.HoraFinal;

                CheckBox bloqueado = this.Horarios.FindControl("CheckBox_Bloqueado_" + horario.Dia.ToString()) as CheckBox;
                bloqueado.Checked = horario.Bloqueado;
                this.OnCheckedChanged_Bloqueado(bloqueado, new EventArgs());
            }
        }

        private string[] ListaHorariosDropDown(bool listafinal)
        {
            IList<string> horarios = new List<string>();
            DateTime horario = new DateTime(1900, 1, 1, 0, 0, 0);

            while (horario.CompareTo(new DateTime(1900, 1, 2, 0, 0, 0)) < 0)
            {
                horarios.Add(horario.Hour.ToString("00") + horario.Minute.ToString("00"));
                horario = horario.AddMinutes(30);
            }

            if (listafinal)
                horarios.Add("2359");

            return horarios.ToArray();
        }

        protected void OnSelectedIndexChanged_HorarioFinal(object sender, EventArgs e)
        {
            DropDownList horainicial = (DropDownList)sender;
            string[] dropdownfinal = this.ListaHorariosDropDown(true).Where(p => int.Parse(p.ToString()) > int.Parse(horainicial.SelectedValue)).ToArray();
            DropDownList horariofinal = this.Horarios.FindControl("DropDownList_HoraFinal_" + horainicial.UniqueID.Substring(horainicial.UniqueID.Length - 1, 1)) as DropDownList;
            horariofinal.Items.Clear();

            for (int i = 0; i < dropdownfinal.Count(); i++)
                horariofinal.Items.Add(new ListItem(dropdownfinal[i].Substring(0, 2) + ":" + dropdownfinal[i].Substring(2, 2), dropdownfinal[i]));
        }

        protected void OnCheckedChanged_Bloqueado(object sender, EventArgs e)
        {
            CheckBox bloqueado = (CheckBox)sender;
            int identificacaocontrole = int.Parse(bloqueado.UniqueID.Substring(bloqueado.UniqueID.Length - 1, 1));

            DropDownList horarioinicial = this.Horarios.FindControl("DropDownList_HoraInicial_" + identificacaocontrole.ToString()) as DropDownList;
            DropDownList horariofinal = this.Horarios.FindControl("DropDownList_HoraFinal_" + identificacaocontrole.ToString()) as DropDownList;

            if (bloqueado.Checked)
            {
                horarioinicial.Enabled = false;
                horariofinal.Enabled = false;
            }
            else
            {
                horarioinicial.Enabled = true;
                horariofinal.Enabled = true;
            }
        }

        /// <summary>
        /// Salva o perfil
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_Salvar(object sender, EventArgs e)
        {
            IList<ViverMais.Model.Operacao> operacoes = new List<ViverMais.Model.Operacao>();
            IViverMaisServiceFacade iViverMais = Factory.GetInstance<IViverMaisServiceFacade>();
            IPerfil iPerfil = Factory.GetInstance<IPerfil>();

            foreach (ListItem operacao in CheckBoxList_Operacoes.Items)
            {
                if (operacao.Selected)
                    operacoes.Add(iViverMais.BuscarPorCodigo<ViverMais.Model.Operacao>(int.Parse(operacao.Value)));
            }

            if (operacoes.Count() > 0)
            {
                try
                {
                    int co_perfil;
                    ViverMais.Model.Perfil perfil = null;

                    if (Request["co_perfil"] != null && int.TryParse(Request["co_perfil"].ToString(), out co_perfil))
                        perfil = iViverMais.BuscarPorCodigo<ViverMais.Model.Perfil>(co_perfil);
                    else
                        perfil = new ViverMais.Model.Perfil();

                    string nomeModulo = GenericsFunctions.RemoveDiacritics(TextBox_Nome.Text);
                    perfil.Nome = nomeModulo.ToUpper();
                    perfil.Modulo = iViverMais.BuscarPorCodigo<ViverMais.Model.Modulo>(int.Parse(DropDownList_Sistema.SelectedValue));
                    perfil.PerfilProfissionalSaude = ckbxProfissionalSaude.Checked;

                    if (iPerfil.BuscarPorModulo<ViverMais.Model.Perfil>(perfil.Modulo.Codigo).Where(p => p.Nome == perfil.Nome && p.Codigo != perfil.Codigo).FirstOrDefault() != null)
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Já existe um perfil com o mesmo nome no módulo selecionado! Por favor, escolha outro nome.');", true);
                    else
                    {
                        perfil.Operacoes = operacoes;
                        iPerfil.Salvar(perfil, this.HorariosPerfil(),((Usuario)Session["Usuario"]).Codigo);
                        //Factory.GetInstance<IPerfil>().SalvarPerfil<Perfil, IList<Operacao>>(perfil, lo);

                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Perfil salvo com sucesso!');location='Default.aspx';", true);
                    }
                }
                catch (Exception f)
                {
                    throw f;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('O perfil deve conter no mínimo uma operação associada!');", true);
            }
        }

        private IList<HorarioPerfil> HorariosPerfil()
        {
            IList<HorarioPerfil> horarios = new List<HorarioPerfil>();

            foreach (int dia in this.RetornaDias())
            {
                HorarioPerfil horario = new HorarioPerfil();
                horario.Dia = dia;
                horario.Bloqueado = (this.Horarios.FindControl("CheckBox_Bloqueado_" + dia.ToString()) as CheckBox).Checked;
                horario.HoraInicial = (this.Horarios.FindControl("DropDownList_HoraInicial_" + dia.ToString()) as DropDownList).SelectedValue;
                horario.HoraFinal = (this.Horarios.FindControl("DropDownList_HoraFinal_" + dia.ToString()) as DropDownList).SelectedValue;

                horarios.Add(horario);
            }

            return horarios;
        }
    }
}
