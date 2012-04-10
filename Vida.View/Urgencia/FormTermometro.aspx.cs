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
using System.Collections;

namespace ViverMais.View.Urgencia
{
    public partial class FormTermometro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                AtualizaQuadro();
        }

        private void AtualizaQuadro()
        {
            IProntuario iProntuario = Factory.GetInstance<IProntuario>();
            IList<ViverMais.Model.Prontuario> fila = iProntuario.BuscaFilaAtendimentoUnidade<ViverMais.Model.Prontuario>(((ViverMais.Model.Usuario)Session["Usuario"]).Unidade.CNES);
            IList<ClassificacaoRisco> classificacoesrisco = iProntuario.ListarTodos<ClassificacaoRisco>().OrderBy(p => p.Ordem).ToList();

            ArrayList arrayclassificacao = new ArrayList();

            foreach (ClassificacaoRisco classificacaorisco in classificacoesrisco)
                arrayclassificacao.Add(new { Imagem = classificacaorisco.Imagem, Quantidade = fila.Where(p => p.ClassificacaoRisco.Codigo == classificacaorisco.Codigo).Count() });

            GridView_TermometroAtendimento.DataSource = arrayclassificacao;
            GridView_TermometroAtendimento.DataBind();
        }

        protected void OnTick_AtualizaQuadro(object sender, EventArgs e) 
        {
            AtualizaQuadro();
        }
    }
}
