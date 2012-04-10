using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    public class MovimentoVacina
    {
        public static char ABERTO = 'A';
        public static char FECHADO = 'F';

        private long codigo;

        public virtual long Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private DateTime data;

        public virtual DateTime Data
        {
            get { return data; }
            set { data = value; }
        }
        private string observacao;

        public virtual string Observacao
        {
            get { return observacao; }
            set { observacao = value; }
        }
        private SalaVacina sala;

        public virtual SalaVacina Sala
        {
            get { return sala; }
            set { sala = value; }
        }
        private TipoMovimentoVacina tipomovimento;

        public virtual TipoMovimentoVacina TipoMovimento
        {
            get { return tipomovimento; }
            set { tipomovimento = value; }
        }
        private MotivoMovimentoVacina motivo;

        public virtual MotivoMovimentoVacina Motivo
        {
            get { return motivo; }
            set { motivo = value; }
        }
        private OperacaoMovimentoVacina operacao;

        public virtual OperacaoMovimentoVacina Operacao
        {
            get { return operacao; }
            set { operacao = value; }
        }

        private EstabelecimentoSaude estabelecimentosaude;
        public virtual EstabelecimentoSaude EstabelecimentoSaude
        {
            get { return estabelecimentosaude; }
            set { estabelecimentosaude = value; }
        }

        private string responsavelenvio;
        public virtual string ResponsavelEnvio
        {
            get { return responsavelenvio; }
            set { responsavelenvio = value; }
        }

        private string responsavelrecebimento;
        public virtual string ResponsavelRecebimento
        {
            get { return responsavelrecebimento; }
            set { responsavelrecebimento = value; }
        }

        private string responsavel;
        public virtual string Responsavel
        {
            get { return responsavel; }
            set { responsavel = value; }
        }

        private DateTime? dataenvio;
        public virtual DateTime? DataEnvio
        {
            get { return dataenvio; }
            set { dataenvio = value; }
        }

        private DateTime? datarecebimento;
        public virtual DateTime? DataRecebimento
        {
            get { return datarecebimento; }
            set { datarecebimento = value; }
        }

        private SalaVacina saladestino;
        public virtual SalaVacina SalaDestino
        {
            get { return saladestino; }
            set { saladestino = value; }
        }

        public virtual string NomeSalaDestino
        {
            get
            {
                if (this.SalaDestino != null)
                    return this.SalaDestino.Nome;

                return string.Empty;
            }
        }

        private long numero;
        public virtual long Numero
        {
            get { return numero; }
            set { numero = value; }
        }

        private bool editar = false;
        public virtual bool Editar
        {
            get { return editar; }
            set { editar = value; }
        }

        private char status;
        public virtual char Status
        {
            get { return status; }
            set { status = value; }
        }

        public MovimentoVacina()
        {
        }
    }
}
