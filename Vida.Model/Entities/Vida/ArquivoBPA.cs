using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.Model.Entities.ViverMais;

namespace ViverMais.Model
{
    public class ArquivoBPA
    {
        public static int TamanhoFolha = 20;

        //Lembrar de substituir por uma única lista de bpa's apontando
        //como base a classe BPA, pois as outras herdam da mesma.
        public ArquivoBPA()
        {
            this.bpas = new List<BPA>();
            //this.individualizados = new List<BpaIndividualizado>();
            //this.consolidados = new List<BpaConsolidado>();
        }

        private IList<BPA> bpas;
        public IList<BPA> Bpas
        {
            get { return bpas; }
            set { bpas = value; }
        }

        /// <summary>
        /// Unidade responsável pela geração do BPA
        /// </summary>
        private EstabelecimentoSaude unidade;
        public EstabelecimentoSaude Unidade
        {
            get { return unidade; }
            set { unidade = value; }
        }

        /// <summary>
        /// Tipo do arquivo gerado: BPAI, BPAC
        /// </summary>
        private char tipo;
        public char Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        /// <summary>
        /// Competência do BPA. Formato: AAAAMM
        /// </summary>
        private int competencia;
        public int Competencia
        {
            get { return competencia; }
            set { competencia = value; }
        }

        ///// <summary>
        ///// Lista de BPAI'S geradas
        ///// </summary>
        //private IList<BpaIndividualizado> individualizados;
        //public IList<BpaIndividualizado> Individualizados
        //{
        //    get { return individualizados; }
        //    set { individualizados = value; }
        //}

        ///// <summary>
        ///// Lista de BPAC'S geradas
        ///// </summary>
        //private IList<BpaConsolidado> consolidados;
        //public IList<BpaConsolidado> Consolidados
        //{
        //    get { return consolidados; }
        //    set { consolidados = value; }
        //}

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

        /// <summary>
        /// Retorna o ano da competência solicitada
        /// </summary>
        /// <returns></returns>
        public int AnoCompetencia()
        {
            return int.Parse(this.competencia.ToString().Substring(0, competencia.ToString().Length - 2));
        }

        /// <summary>
        /// Número de controle para os procedimentos executados e
        /// contidos no arquivo.
        /// </summary>
        public long NumeroControle
        {
            get
            {
                long soma_codigo_procedimento = bpas.Sum(p => long.Parse(p.Procedimento.Codigo));
                long soma_qtd = bpas.Sum(p => p.Quantidade);
                long calculoinicial = bpas.Sum(p => p.Quantidade) + bpas.Sum(p => long.Parse(p.Procedimento.Codigo));

                //if (tipo == BPA.CONSOLIDADO)
                //    calculoinicial = consolidados.Sum(p => p.Quantidade) + consolidados.Sum(p => long.Parse(p.Procedimento.Codigo));
                //else if (tipo == BPA.INDIVIDUALIZADO)
                //    calculoinicial = individualizados.Sum(p => p.Quantidade) + individualizados.Sum(p => long.Parse(p.Procedimento.Codigo));

                return (calculoinicial % 1111) + 1111;
            }
        }

        /// <summary>
        /// Retorna o número de folhas geradas pelo arquivo. Deve-se incluir também o cabeçalho.
        /// </summary>
        /// <returns></returns>
        public int NumeroFolhas()
        {
            int tamanhofolha = ArquivoBPA.TamanhoFolha;
            int numerolinhas = this.NumeroLinhas();
            int totaisfolha = 0;

            if ((numerolinhas) <= tamanhofolha)
                totaisfolha = 1;
            else
            {
                totaisfolha = numerolinhas / tamanhofolha;

                if ((numerolinhas % tamanhofolha) != 0)
                    totaisfolha += 1;
            }

            return totaisfolha;
        }

        /// <summary>
        /// Retorna o número de linhas geradas pelo arquivo. Deve-se incluir também o cabeçalho.
        /// </summary>
        /// <returns></returns>
        public int NumeroLinhas()
        {
            int numerolinhas = 1;

            numerolinhas += this.bpas.Count();
            //if (tipo == BPA.CONSOLIDADO)
            //    numerolinhas += this.consolidados.Count();
            //else if (tipo == BPA.INDIVIDUALIZADO)
            //    numerolinhas += this.individualizados.Count();

            return numerolinhas;
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

        /// <summary>
        /// Nome do arquivo com extensão no formato: CNESTIPO.EXTENSAO
        /// </summary>
        /// <returns>nome do arquivo</returns>
        public string NomeDoArquivo()
        {
            string formato = string.Empty;

            if (this.tipo == BPA.INDIVIDUALIZADO)
                formato += "PAI";
            else if (this.tipo == BPA.CONSOLIDADO)
                formato += "PAC";
            else if (this.tipo == BPA.APAC)
                formato += "PAAPAC";

            formato += this.unidade.CNES.Remove(5);
            formato += this.ExtensaoDocumento();

            return formato;
        }
    }
}
