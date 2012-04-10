using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class CartaoSUS:AModel
    {
        public enum TipoDeCartao {PROVISORIO = 'P', DEFINITIVO = 'D' }
        
        string numero;

        public virtual string Numero
        {
            get { return numero; }
            set { numero = value; }
        }

        char tipo;

        public virtual char Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        DateTime? dataAtribuicao;

        public virtual DateTime? DataAtribuicao
        {
            get { return dataAtribuicao; }
            set { dataAtribuicao = value; }
        }

        ViverMais.Model.Paciente paciente;

        public virtual ViverMais.Model.Paciente Paciente
        {
            get { return paciente; }
            set { paciente = value; }
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

        public CartaoSUS()
        {

        }

        //Sempre retorna false pois é sempre cadastro.
        public override bool Persistido()
        {
            return false;
        }
    }
}
