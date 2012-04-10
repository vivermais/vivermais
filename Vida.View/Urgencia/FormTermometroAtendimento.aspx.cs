using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades;
using System.Data;
using System.Collections;

namespace ViverMais.View.Urgencia
{
    public partial class FormTermometroAtendimento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IList<ViverMais.Model.Prontuario> fila = Factory.GetInstance<IProntuario>().BuscaFilaAtendimentoUnidade<ViverMais.Model.Prontuario>(((ViverMais.Model.Usuario)Session["Usuario"]).Unidade.Codigo);
                IList<ClassificacaoRisco> lc = Factory.GetInstance<IUrgenciaServiceFacade>().ListarTodos<ClassificacaoRisco>().OrderBy(p => p.Ordem).ToList();

                ArrayList lo = new ArrayList();

                foreach (ClassificacaoRisco c in lc)
                    lo.Add(new { Imagem = c.Imagem, Quantidade = fila.Where(p => p.ClassificacaoRisco.Codigo == c.Codigo).Count() });

                GridView_TermometroAtendimento.DataSource = lo;
                GridView_TermometroAtendimento.DataBind();
            }
        }
    }
}
