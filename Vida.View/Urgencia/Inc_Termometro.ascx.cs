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
using InfoSoftGlobal;

namespace ViverMais.View.Urgencia
{
    public partial class Inc_Termometro : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        private void AtualizaQuadro()
        {
            Literal_Quadro.Text = CreateCharts();
        }

        protected void OnTick_AtualizaQuadro(object sender, EventArgs e)
        {
            AtualizaQuadro();
        }

        public string CreateCharts()
        {
            IList<ViverMais.Model.Prontuario> fila = Factory.GetInstance<IProntuario>().BuscarFilaAtendimentoUnidade<ViverMais.Model.Prontuario>(((ViverMais.Model.Usuario)Session["Usuario"]).Unidade.CNES);

            string strXML = string.Empty;
            strXML += "<graph caption='Atendimento' xAxisName='Classificação de Risco' yAxisName='Quantidade' showPercentageValues='0' decimalPrecision='0' formatNumberScale='0'>";

            IList<ClassificacaoRisco> classificacoesrisco = Factory.GetInstance<IUrgenciaServiceFacade>().ListarTodos<ClassificacaoRisco>().OrderBy(p => p.Ordem).ToList();

            foreach (ClassificacaoRisco classificacaorisco in classificacoesrisco)
                strXML += "<set name='" + classificacaorisco.Descricao + "' hoverText='" + classificacaorisco.Descricao + "' value='" + fila.Where(p => p.ClassificacaoRisco.Codigo == classificacaorisco.Codigo).Count() + "' color='" + classificacaorisco.CorGrafico + "' />";

            strXML += "</graph>";

            return FusionCharts.RenderChartHTML("FusionCharts/FCF_Column3D.swf", "", strXML, "myNext", "600", "300", false);
        }
    }
}