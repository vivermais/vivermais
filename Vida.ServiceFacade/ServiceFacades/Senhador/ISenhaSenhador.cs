using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Senhador
{
    public interface ISenhaSenhador : ISenhador
    {
        /// <summary>
        /// Gera uma senha, de atendimento ao paciente, para um serviço específico de determinado estabelecimento de saúde
        /// </summary>
        /// <param name="co_servico">código do serviço</param>
        /// <param name="co_estabelecimento">código do estabelecimento</param>
        /// <param name="co_tiposenha">código do tipo de senha</param>
        /// <returns></returns>
        string GerarSenhaAtendimentoPaciente(int co_servico, string co_estabelecimento, int co_tiposenha, string co_paciente);

        /// <summary>
        /// Gera uma senha, de atendimento ao paciente, para um serviço específico de determinado estabelecimento de saúde
        /// </summary>
        /// <typeparam name="T">tipo de retorno do objeto senha</typeparam>
        /// <param name="co_servico">código do serviço</param>
        /// <param name="co_estabelecimento">código do estabelecimento</param>
        /// <param name="co_tiposenha">código do tipo de senha</param>
        /// <returns></returns>
        T GerarSenhaAtendimentoPaciente<T>(int co_servico, string co_estabelecimento, int co_tiposenha, string co_paciente);

        /// <summary>
        /// Gera uma senha, de atendimento ao paciente, para um serviço e profissional específico de determinado estabelecimento de saúde
        /// </summary>
        /// <param name="co_servico">código do serviço</param>
        /// <param name="co_estabelecimento">código do estabelecimento</param>
        /// <param name="co_tiposenha">código do tipo de senha</param>
        /// <param name="co_profissional">código do profissional</param>
        /// <returns></returns>
        string GerarSenhaAtendimentoPaciente(int co_servico, string co_estabelecimento, int co_tiposenha, string co_paciente, string co_profissional);

        /// <summary>
        /// Gera uma senha, de atendimento ao paciente, para um serviço e profissional específico de determinado estabelecimento de saúde
        /// </summary>
        /// <typeparam name="T">tipo de retorno do objeto senha</typeparam>
        /// <param name="co_servico">código do serviço</param>
        /// <param name="co_estabelecimento">código do estabelecimento</param>
        /// <param name="co_tiposenha">código do tipo de senha</param>
        /// <param name="co_profissional">código do profissional</param>
        /// <returns></returns>
        T GerarSenhaAtendimentoPaciente<T>(int co_servico, string co_estabelecimento, int co_tiposenha, string co_paciente, string co_profissional);
    }
}
