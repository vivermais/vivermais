using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class Solicitacao
    {
        public enum SituacaoSolicitacao { PENDENTE = '1', AUTORIZADA = '2', AGENDAUTOMATICO = '3', INDEFERIDA = '4', CONFIRMADA = '5', DESMARCADA = '6', FALTOSO = '7' }
        public enum StatusPrioridade { VERMELHO = '0', AMARELO = '1', VERDE = '2', AZUL = '3', BRANCO = '4' }
        public enum TipoCota { LOCAL = 'L', DISTRITAL = 'D', REDE = 'R', RESERVA_TECNICA = 'T' }

        public static int rbtItemSalvador = 0;
        public static int rbtItemInterior = 1;
        public static int rbtItemMunicipioEspecifico = 2;

        /// Situação atual da Solicitação.
        /// 1 - Pendente, 2 - Autorizada, 3 - Ag. Automático
        /// 4 - Indeferida, 5 - Confirmada, 6 - Desmarcada
        /// 

        public virtual string NomeSituacao
        {
            get
            {
                if (this.Situacao == Convert.ToChar(SituacaoSolicitacao.PENDENTE).ToString())
                    return "PENDENTE";
                else if (this.Situacao == Convert.ToChar(SituacaoSolicitacao.AUTORIZADA).ToString())
                    return "AUTORIZADA";
                else if (this.Situacao == Convert.ToChar(SituacaoSolicitacao.AGENDAUTOMATICO).ToString())
                    return "AG. AUTOMÁTICO";
                else if (this.Situacao == Convert.ToChar(SituacaoSolicitacao.INDEFERIDA).ToString())
                    return "INDEFERIDA";
                else if (this.Situacao == Convert.ToChar(SituacaoSolicitacao.CONFIRMADA).ToString())
                    return "CONFIRMADA";
                else if (this.Situacao == Convert.ToChar(SituacaoSolicitacao.DESMARCADA).ToString())
                    return "DESMARCADA";
                else if (this.Situacao == Convert.ToChar(SituacaoSolicitacao.FALTOSO).ToString())
                    return "FALTOSO";
                else
                    return null;
            }
        }

        private int codigo;
        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private string id_paciente;
        public virtual string ID_Paciente
        {
            get { return id_paciente; }
            set { id_paciente = value; }
        }

        private int id_ProfissionalSolicitante;

        public virtual int Id_ProfissionalSolicitante
        {
            get { return id_ProfissionalSolicitante; }
            set { id_ProfissionalSolicitante = value; }
        }

        private Agenda agenda;
        public virtual Agenda Agenda
        {
            get { return agenda; }
            set { agenda = value; }
        }

        public virtual string NomeProfissionalExecutante
        {
            get
            {
                return agenda != null && agenda.ID_Profissional != null ? agenda.ID_Profissional.Nome : String.Empty;
            }
        }

        private DateTime data_solicitacao;
        public virtual DateTime Data_Solicitacao
        {
            get { return data_solicitacao; }
            set { data_solicitacao = value; }
        }

        private string identificador;
        public virtual string Identificador
        {
            get { return identificador; }
            set { identificador = value; }
        }

        private string situacao;

        /// <summary>
        /// Situação atual da Solicitação.
        /// 1 - Pendente, 2 - Autorizada, 3 - Ag. Automático
        /// 4 - Indeferida, 5 - Confirmada, 6 - Desmarcada
        /// </summary>
        public virtual string Situacao
        {
            get { return situacao; }
            set { situacao = value; }
        }

        private DateTime? data_confirmacao;
        public virtual DateTime? Data_Confirmacao
        {
            get { return data_confirmacao; }
            set { data_confirmacao = value; }
        }

        private int qtd;
        public virtual int Qtd
        {
            get { return qtd; }
            set { qtd = value; }
        }

        private string prioridade;
        public virtual string Prioridade
        {
            get { return prioridade; }
            set { prioridade = value; }
        }

        public virtual string NomePrioridade
        {
            get
            {
                if (this.prioridade == "0")
                    return "Vermelho";
                else if (this.prioridade == "1")
                    return "Amarelo";
                else if (this.prioridade == "2")
                    return "Verde";
                else if (this.prioridade == "3")
                    return "Azul";
                else if (this.prioridade == "4")
                    return "Branco";
                else return null;
            }
        }

        public virtual string NomeUnidadeExecutante
        {
            get { if (this.agenda != null)return this.agenda.Estabelecimento.NomeFantasia; else return null; }
        }

        public virtual string DataAgenda
        {
            get { if (this.agenda != null)return this.agenda.Data.ToShortDateString(); else return null; }
        }

        private string easSolicitante;
        public virtual string EasSolicitante
        {
            get { return easSolicitante; }
            set { easSolicitante = value; }
        }

        private string observacao;
        public virtual string Observacao
        {
            get { return observacao; }
            set { observacao = value; }
        }

        private string justificativaIndeferimento;

        public virtual string JustificativaIndeferimento
        {
            get { return justificativaIndeferimento; }
            set { justificativaIndeferimento = value; }
        }


        private string numeroProtocolo;
        public virtual string NumeroProtocolo
        {
            get { return numeroProtocolo; }
            set { numeroProtocolo = value; }
        }

        private Procedimento procedimento;

        public virtual Procedimento Procedimento
        {
            get { return procedimento; }
            set { procedimento = value; }
        }

        public virtual string NomeProcedimento
        {
            get { return procedimento != null ? procedimento.Nome : ""; }
        }

        private string cidSolicitante;

        public virtual string CidSolicitante
        {
            get { return cidSolicitante; }
            set { cidSolicitante = value; }
        }

        private Cid cidExecutante;

        public virtual Cid CidExecutante
        {
            get { return cidExecutante; }
            set { cidExecutante = value; }
        }

        private string telefoneContato;

        public virtual string TelefoneContato
        {
            get { return telefoneContato; }
            set { telefoneContato = value; }
        }

        private PactoReferenciaSaldo pactoReferenciaSaldo;

        public virtual PactoReferenciaSaldo PactoReferenciaSaldo
        {
            get { return pactoReferenciaSaldo; }
            set { pactoReferenciaSaldo = value; }
        }

        private PactoAbrangenciaAgregado pactoAbrangenciaAgregado;

        public virtual PactoAbrangenciaAgregado PactoAbrangenciaAgregado
        {
            get { return pactoAbrangenciaAgregado; }
            set { pactoAbrangenciaAgregado = value; }
        }

        private DateTime dataIndeferimento;

        public virtual DateTime DataIndeferimento
        {
            get { return dataIndeferimento; }
            set { dataIndeferimento = value; }
        }

        private string justificativaDesmarcar;

        public virtual string JustificativaDesmarcar
        {
            get { return justificativaDesmarcar; }
            set { justificativaDesmarcar = value; }
        }

        private Usuario usuarioSolicitante;

        public virtual Usuario UsuarioSolicitante
        {
            get { return usuarioSolicitante; }
            set { usuarioSolicitante = value; }
        }

        private char tipoCotaUtilizada;

        public virtual char TipoCotaUtilizada
        {
            get { return tipoCotaUtilizada; }
            set { tipoCotaUtilizada = value; }
        }

        private Usuario usuarioRegulador;
        public virtual Usuario UsuarioRegulador
        {
            get { return usuarioRegulador; }
            set { usuarioRegulador = value; }
        }

        private long prontuario;

        public virtual long Prontuario
        {
            get { return prontuario; }
            set { prontuario = value; }
        }

        public Solicitacao()
        {

        }
    }
}
