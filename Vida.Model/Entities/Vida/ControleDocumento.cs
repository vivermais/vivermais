using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class ControleDocumento:AModel
    {
        Paciente paciente;

        public virtual Paciente Paciente
        {
            get { return paciente; }
            set { paciente = value; }
        }

        TipoDocumento tipoDocumento;

        public virtual TipoDocumento TipoDocumento
        {
            get { return tipoDocumento; }
            set { tipoDocumento = value; }
        }

        char controle;

        /// <summary>
        /// Identifica a operação a executar no Documento. I ou vazio = inclusão; A = alteração;
        /// </summary>
        public virtual char Controle
        {
            get { return controle; }
            set { controle = value; }
        }

        char excluido;

        /// <summary>
        /// Indica se o Usuário está excluído logicamente. 0 – ativo; 1 – excluído
        /// </summary>
        public virtual char Excluido
        {
            get { return excluido; }
            set { excluido = value; }
        }

        DateTime dataOperacao;

        /// <summary>
        /// Data e hora da última modificação no registro do usuário.
        /// </summary>
        public virtual DateTime DataOperacao
        {
            get { return dataOperacao; }
            set { dataOperacao = value; }
        }    

        public ControleDocumento()
        {

        }

        public override bool Equals(object obj)
        {
            return this.Paciente.Codigo == ((ControleDocumento)obj).Paciente.Codigo && this.TipoDocumento.Codigo == ((ControleDocumento)obj).TipoDocumento.Codigo;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() * 47;
        }

        public override bool Persistido()
        {
            throw new NotImplementedException();
        }
    }
}
