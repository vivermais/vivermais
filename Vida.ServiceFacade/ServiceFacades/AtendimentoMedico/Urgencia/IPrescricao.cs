using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Collections;

namespace ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia
{
    public interface IPrescricao : IServiceFacade
    {
        IList<T> ListarMedicamentos<T>(string[] codigosmedicamentos);
        IList<T> BuscarMedicamentos<T>(long co_prescricao);
        IList<T> BuscarProcedimentos<T>(long co_prescricao);
        IList<T> BuscarProcedimentosNaoFaturaveis<T>(long co_prescricao);
        IList<T> BuscarPorProntuario<T>(long co_prontuario);
        IList<T> ListarProcedimentosFPOUnidade<T>(bool faturavel, string co_unidade, int competencia);
        IList<T> BuscarPorProntuario<T>(long co_prontuario, char status);
        IList<T> BuscarCidsProcedimentoPorCodigo<T>(string co_procedimento, string codigocid);
        IList<T> BuscarCidsProcedimentoPorGrupo<T>(string co_procedimento, string grupocid);

        void AtualizarStatusPrescricoesProntuario(long co_prontuario);
        void AtualizarStatusItensAprazadosPrescricao<T>(T prescricao, bool verificar_medicamentos, bool verificar_procedimentos, bool verificar_procedimentosnaofaturaveis);
        void ExcluirPrescricao<T>(T prescricao);
        /// <summary>
        /// Exclui o procedimento não-faturável de uma prescrição.
        /// </summary>
        /// <param name="T">Procedimento da Prescrição</param>
        void ExcluirProcedimentoNaoFaturavelPrescricao<T>(T _prescricaoprocedimento);
        /// <summary>
        /// Exclui o medicamento não-faturável de uma prescrição.
        /// </summary>
        /// <param name="T">Procedimento da Prescrição</param>
        void ExcluirMedicamentoPrescricao<T>(T _prescricaomedicamento);

        T RetornaPrescricaoVigente<T>(long co_prontuario);
        T BuscarMedicamento<T>(long co_prescricao, int co_medicamento);
        T BuscarProcedimento<T>(long co_prescricao, string co_procedimento);
        T BuscarProcedimentoNaoFaturavel<T>(long co_prescricao, int co_procedimento);

        /// <summary>
        /// Verifica a possibilidade de excluir um item de uma prescrição, uma vez que a mesma não pode
        /// estar cadastrada sem pelo menos um item.
        /// </summary>
        /// <param name="co_prescricao">código da prescrição</param>
        /// <returns></returns>
        bool VerificaPossibilidadeExcluirItemPrescricao(long co_prescricao);

        /// <summary>
        /// Verifica a possibilidade de excluir um procedimento de uma prescrição, uma vez que
        /// o mesmo não pode ser excluído caso haja aprazamentos com status de confirmado.
        /// </summary>
        /// <param name="co_prescricao">código da prescrição</param>
        /// <param name="co_procedimento">código do procedimento</param>
        /// <returns></returns>
        bool VerificaPossibilidadeExcluirProcedimentoPrescricao(long co_prescricao, string co_procedimento);

        /// <summary>
        /// Exclui o procedimento de uma prescrição.
        /// </summary>
        /// <param name="T">Procedimento da Prescrição</param>
        void ExcluirProcedimentoPrescricao<T>(T _prescricaoprocedimento);

        /// <summary>
        /// Verifica a possibilidade de excluir um procedimento não-faturável de uma prescrição, uma vez que
        /// o mesmo não pode ser excluído caso haja aprazamentos com status de confirmado.
        /// </summary>
        /// <param name="co_prescricao">código da prescrição</param>
        /// <param name="co_procedimento">código do procedimento</param>
        /// <returns></returns>
        bool VerificaPossibilidadeExcluirProcedimentoNaoFaturavelPrescricao(long co_prescricao, int co_procedimento);

        /// <summary>
        /// Verifica a possibilidade de excluir um medicamento de uma prescrição, uma vez que
        /// o mesmo não pode ser excluído caso haja aprazamentos com status de confirmado.
        /// </summary>
        /// <param name="co_prescricao">código da prescrição</param>
        /// <param name="co_procedimento">código do procedimento</param>
        /// <returns></returns>
        bool VerificaPossibilidadeExcluirMedicamentoPrescricao(long co_prescricao, int co_medicamento);
    }
}
