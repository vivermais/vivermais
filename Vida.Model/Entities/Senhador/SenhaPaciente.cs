using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    public class SenhaPaciente : SenhaServico
    {
        public SenhaPaciente()
        {
        }

        private Paciente paciente;

        public virtual Paciente Paciente
        {
            get { return paciente; }
            set { paciente = value; }
        }

        /// <summary>
        /// Retorna a senha de atendimento do paciente no formato para impressão
        /// </summary>
        /// <returns></returns>
        public override string Impressao()
        {
            return this.Estabelecimento.NomeFantasia + "<br/> <br/> <br/>" + this.Sigla + this.Senha.ToString() + "<br/> <br/>"
                + this.Servico.Nome + "<br/>" + this.TipoSenha.Nome + "<br/> <br/>" + this.GeradaEm.ToString("dd/MM/yyyy HH:mm:ss");
        }
    }
}
