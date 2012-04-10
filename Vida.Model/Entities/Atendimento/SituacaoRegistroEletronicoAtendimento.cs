using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class SituacaoRegistroEletronicoAtendimento
    {
        //Caso o paciente já tenha sido chamado pelo profissional que ira atende-lo
        public static int EM_ATENDIMENTO = 1;
        //Caso o paciente tenha se evadido do local
        public static int EVASAO = 2;
        //Caso o paciente venha a obito
        public static int OBITO = 3;
        //Caso o paciente tenha retirado a senha e esteja aguardando a chamada do profissional que irá atende-lo
        public static int AGUARDANDO_ATENDIMENTO = 4;
        //Caso tenha finalizado o serviço para o qual o paciente foi encaminhado
        public static int FINALIZADO = 5;

        int codigo;

        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }
        
        string nome;

        public virtual string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        public SituacaoRegistroEletronicoAtendimento()
        {

        }

        public override string ToString()
        {
            return this.nome;
        }
    }
}
