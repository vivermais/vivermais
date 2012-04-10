using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using ViverMais.Model;
using System.Collections.Generic;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Misc;
using System.Drawing;
using ViverMais.ServiceFacade.ServiceFacades.Localidade;

namespace ViverMais.View.Farmacia
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaUsuarios();
            }
        }

        /// <summary>
        /// Carrega os usuários de acordo com as permissões do administrador
        /// </summary>
        private void CarregaUsuarios()
        {
            Usuario u = (ViverMais.Model.Usuario)Session["Usuario"];
            IList<Perfil> lp = u.Perfis.Where(p => p.Nome.Equals("Administrador")).ToList();
            IList<Usuario> lup = new List<Usuario>();

            foreach (Perfil p in lp)
            {
                IList<Usuario> lu = Factory.GetInstance<IUsuario>().BuscarPorModulo<ViverMais.Model.Usuario>(p.Modulo.Codigo);

                foreach (ViverMais.Model.Usuario us in lu)
                {
                    if (!lup.Contains(us))
                        lup.Add(us);
                }
            }
            lup = lup.OrderBy(p => p.Nome).ToList();
            GridView_Usuarios.DataSource = lup.OrderBy(p => p.Nome).ToList();
            GridView_Usuarios.DataBind();
            Session["lup"] = lup;

            IList<ViverMais.Model.Distrito> distritos = Factory.GetInstance<IViverMaisServiceFacade>().ListarTodos<Distrito>();
            foreach (ViverMais.Model.Distrito distrito in distritos)
                ddlConsultaDistrito.Items.Add(new ListItem(distrito.Nome, distrito.Codigo.ToString()));

        }

        protected void OnPageIndexChanging_Paginacao(object sender, GridViewPageEventArgs e)
        {
            CarregaUsuarios();
            GridView_Usuarios.PageIndex = e.NewPageIndex;
            GridView_Usuarios.DataBind();
        }

        protected void GridView_Usuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = GridView_Usuarios.SelectedIndex;
            IList<Usuario> lup = (IList<Usuario>)Session["lup"];
            Usuario usuario = lup[index];
            lblNome.Text = usuario.Nome;
            Session["user"] = usuario;
            GridView_Usuarios.SelectedRowStyle.BackColor = Color.LightGray;

            int? codigoDistrito = Factory.GetInstance<IUsuarioDistritoFarmacia>().BuscarDistritoPorUsuario<int>(usuario.Codigo);
            if (codigoDistrito != null && codigoDistrito != 0)
            {
                ViverMais.Model.Distrito distrito = Factory.GetInstance<IViverMaisServiceFacade>().BuscarPorCodigo<Distrito>(codigoDistrito);
                lblVinculo.Text = distrito.Nome;
            }
            else
                lblVinculo.Text = "Nenhum";
            
            IList<Distrito> distritos = Factory.GetInstance<IViverMaisServiceFacade>().ListarTodos<Distrito>();
            foreach (Distrito d in distritos)
                ddlDistrito.Items.Add(new ListItem(d.Nome, d.Codigo.ToString()));
        }

        protected void btnVincular_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            ViverMais.Model.Usuario usuario = (ViverMais.Model.Usuario)Session["user"];
            //ViverMais.Model.Distrito distrito = Factory.GetInstance<IUsuarioDistritoFarmacia>().BuscarDistritoPorUsuario<ViverMais.Model.Distrito>(usuario.Codigo);
            ViverMais.Model.UsuarioDistritoFarmacia udf = Factory.GetInstance<IUsuarioDistritoFarmacia>().BuscarPorUsuario<UsuarioDistritoFarmacia>(usuario.Codigo);
            //Remove o vínculo com outro distrito que não seja o selecionado
            if (udf != null && udf.CodigoDistrito != int.Parse(ddlDistrito.SelectedValue))
            {
                Factory.GetInstance<IUsuarioDistritoFarmacia>().Deletar(udf);
            }

            //Adiciona o vinculo ao distrito selecionado, se não já existir
            udf = new UsuarioDistritoFarmacia();
            udf.CodigoDistrito = int.Parse(ddlDistrito.SelectedValue);
            udf.CodigoUsuario = usuario.Codigo;
            Factory.GetInstance<IUsuarioDistritoFarmacia>().Salvar(udf);

            ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script type='text/javascript'>alert('Dados Salvos com Sucesso');</script>");
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;
            if (GridView_Usuarios.SelectedRow == null)
                args.IsValid = false;
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            ViverMais.Model.Distrito distrito = Factory.GetInstance<IDistrito>().BuscarPorCodigo<Distrito>(int.Parse(ddlConsultaDistrito.SelectedValue));
            IList<int> codigousuarios = Factory.GetInstance<IUsuarioDistritoFarmacia>().BuscarUsuariosPorDistrito<int>(distrito.Codigo);
            IList<Usuario> usuarios = new List<Usuario>();
            foreach (int u in codigousuarios)
            {
                usuarios.Add(Factory.GetInstance<IViverMaisServiceFacade>().BuscarPorCodigo<Usuario>(u));
            }
            GridViewUsuariosDistrito.DataSource = usuarios;
            GridViewUsuariosDistrito.DataBind();
        }
    }
}
