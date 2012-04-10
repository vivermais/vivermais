using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    public class ArquivoAPAC
    {
        public ArquivoAPAC() 
        { 
            this.Apacs = new List<APAC>();
        }
        private int competencia;

        /// <summary>
        /// Ano e mês da produção no formato (AAAAMM).
        /// </summary>
        public int Competencia
        {
            get { return competencia; }
            set { competencia = value; }
        }
        
        /// <summary>
        /// Número de controle para os procedimentos Principais executados e
        /// contidos no arquivo.
        /// </summary>
        public long NumeroControle
        {
            get
            {
                //long soma_codigo_procedimento = apacs.Sum(p => long.Parse(p.DescritivoProcedimentosRealizados.Sum(j => j.Procedimento.Codigo)));//Procedimento.Codigo));
                //long soma_qtd = apacs.Sum(p => p.DescritivoProcedimentosRealizados.Length);
                //DescritivoProcedimentosRealizados[] desc = apacs.Select(p => p.DescritivoProcedimentosRealizados);
                long calculoinicial = apacs.Sum(p => p.DescritivoProcedimentosRealizados.Count) + apacs.Sum(p => long.Parse(p.DescritivoProcedimentosRealizados.Sum(j => long.Parse(j.Procedimento.Codigo)).ToString()));
                return (calculoinicial % 1111) + 1111;
            }
        }

        /// <summary>
        /// Retorna o número de linhas geradas pelo arquivo. Deve-se incluir também o cabeçalho.
        /// </summary>
        /// <returns></returns>
        public int NumeroLinhas()
        {
            int numerolinhas = 1;

            numerolinhas += this.Apacs.Count();
            //if (tipo == BPA.CONSOLIDADO)
            //    numerolinhas += this.consolidados.Count();
            //else if (tipo == BPA.INDIVIDUALIZADO)
            //    numerolinhas += this.individualizados.Count();

            return numerolinhas;
        }

        /// <summary>
        /// Nome do arquivo com extensão no formato: CNESTIPO.EXTENSAO
        /// </summary>
        /// <returns>nome do arquivo</returns>
        public string NomeDoArquivo()
        {
            return "PAAPAC" + this.unidade.CNES.Remove(5) + this.ExtensaoDocumento();
        }

        /// <summary>
        /// Unidade responsável pela geração do arquivo APAC
        /// </summary>
        private EstabelecimentoSaude unidade;
        public EstabelecimentoSaude Unidade
        {
            get { return unidade; }
            set { unidade = value; }
        }



        /// <summary>
        /// Nome da unidade com o padrão de 30 caracteres
        /// </summary>
        /// <returns>nome do orgão responsável pela informação</returns>
        public string NomeUnidade()
        {
            string nomeunidade = this.Unidade.NomeFantasia;

            if (nomeunidade.Length > 30)
                nomeunidade = nomeunidade.Remove(30);
            else
            {
                while (nomeunidade.Length != 30)
                    nomeunidade += " ";
            }

            return nomeunidade;
        }
        
        private List<APAC> apacs;
        public List<APAC> Apacs
        {
            get { return apacs; }
            set { apacs = value; }
        }

        /// <summary>
        /// Extensão do arquivo de acordo com o mês da competência
        /// </summary>
        /// <returns>extensão do arquivo</returns>
        public string ExtensaoDocumento()
        {
            int tamanho = this.Competencia.ToString().Length;
            switch (int.Parse(Competencia.ToString()[(tamanho - 2)].ToString() + Competencia.ToString()[(tamanho - 1)].ToString()))
            {
                case 1:
                    return ".JAN";
                case 2:
                    return ".FEV";
                case 3:
                    return ".MAR";
                case 4:
                    return ".ABR";
                case 5:
                    return ".MAI";
                case 6:
                    return ".JUN";
                case 7:
                    return ".JUL";
                case 8:
                    return ".AGO";
                case 9:
                    return ".SET";
                case 10:
                    return ".OUT";
                case 11:
                    return ".NOV";
                case 12:
                    return ".DEZ";
            }

            return ".XXX"; //Formato inválido
        }
    }
}
