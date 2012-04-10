using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.Model;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using System.IO;

namespace ViverMais.View.Atendimento
{
    public partial class FormQuadroEntregaExame : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                OnTick_VerificarEntregaExame(new object(), new EventArgs());
        }

        /// <summary>
        /// Verifica a existência de exame para entrega
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnTick_VerificarEntregaExame(object sender, EventArgs e)
        {
            var stream = new FileStream(Server.MapPath("~/Urgencia/Documentos/EntregaExame/" + ((Usuario)Session["Usuario"]).Unidade.CNES + ".txt"), FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            TextReader leitura = new StreamReader(stream);

            if (Convert.ToChar(leitura.ReadLine()) == 'S')
                this.Image_BotaoExames.ImageUrl = "~/Urgencia/img/resultado-exame-animado.gif";
            else
                this.Image_BotaoExames.ImageUrl = "~/Urgencia/img/resultado-exame-estatico.gif";

            stream.Close();

            //IList<ProntuarioExame> exames = Factory.GetInstance<IExame>().ListarPorEstabelecimentoDisponivelEntrega<ProntuarioExame>(((Usuario)Session["Usuario"]).Unidade.CNES);

            //if (exames.Count() > 0)
            //    Image_BotaoExames.ImageUrl = "~/Urgencia/img/resultado-exame-animado.gif";
            //else
            //    Image_BotaoExames.ImageUrl = "~/Urgencia/img/resultado-exame-estatico.gif";
        }
    }
}
