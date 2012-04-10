using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using System.Data;
using ViverMais.Model;
using System.IO;

namespace ViverMais.View.Urgencia
{
    public partial class _ExibeFilaAcompanhamento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IUsuarioProfissionalUrgence iUsuarioProfissional = Factory.GetInstance<IUsuarioProfissionalUrgence>();

                Usuario usuario = (Usuario)Session["Usuario"];
                bool acessoMedico = iUsuarioProfissional.VerificarAcessoMedico(usuario.Codigo, "EXECUTAR_ACOLHIMENTO", usuario.Unidade.CNES);
                bool acessoEnfermeiro = iUsuarioProfissional.VerificarAcessoEnfermeiro(usuario.Codigo, "EXECUTAR_ACOLHIMENTO", usuario.Unidade.CNES);

                if (!acessoMedico && !acessoEnfermeiro)
                {
                    gridFila.Columns.RemoveAt(1);

                    BoundField numeroprontuario = new BoundField();
                    numeroprontuario.DataField = "NumeroProntuario";
                    numeroprontuario.HeaderText = "Identificador";
                    numeroprontuario.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                    numeroprontuario.ItemStyle.Width = Unit.Pixel(100);
                    gridFila.Columns.Insert(1, numeroprontuario);
                }

                this.CarregaLista(true, false);
            }
        }

        /// <summary>
        /// Carrega os prontuários que estão na fila
        /// </summary>
        private void CarregaLista(bool carregamentoinicial, bool ler_arquivo)
        {
            bool existe_nova_entrada = false;

            if (ler_arquivo)
            {
                var stream = new FileStream(Server.MapPath("~/Urgencia/Documentos/FilaAcolhimento.txt"), FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
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
                IList<ViverMais.Model.Prontuario> fila = iProntuario.buscaFilaAcompanhamento<ViverMais.Model.Prontuario>(((ViverMais.Model.Usuario)Session["Usuario"]).Unidade.CNES);
                DataTable tabela = iProntuario.getDataTablePronturario<IList<ViverMais.Model.Prontuario>>(fila);
                
                Session["TabelaFilaAcolhimento"] = tabela;
            }

            gridFila.DataSource = (DataTable)Session["TabelaFilaAcolhimento"];
            gridFila.DataBind();
        }

        protected void OnTick_Temporizador(object sender, EventArgs e)
        {
            this.CarregaLista(false, true);
        }
    }
}
