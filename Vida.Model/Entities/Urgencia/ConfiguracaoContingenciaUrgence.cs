using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace ViverMais.Model
{
    [Serializable]
    public class ConfiguracaoContingenciaUrgence
    {
        public static string DIR_ACOLHIMENTO = "Acolhimento";
        public static string DIR_APRAZAMENTOS = "Aprazamentos";
        public static string DIR_ATESTADOSRECEITAS = "AtestadosReceitas";
        public static string DIR_CONSULTAMEDICA = "ConsultaMedica";
        public static string DIR_EVOLUCOESENFERMAGEM = "EvolucoesEnfermagem";
        public static string DIR_EVOLUCOESMEDICAS = "EvolucoesMedicas";
        public static string DIR_EXAMESELETIVOS = "ExamesEletivos";
        public static string DIR_EXAMESINTERNOS = "ExamesInternos";
        public static string DIR_FICHAATENDIMENTO = "FichaAtendimento";
        public static string DIR_PRESCRICOES = "Prescricoes";
        public static string DIR_RELATORIOGERAL = "RelatorioGeral";

        private EstabelecimentoSaude unidade;
        public virtual EstabelecimentoSaude Unidade
        {
            get { return unidade; }
            set { unidade = value; }
        }

        private string usuarioftp;
        public virtual string UsuarioFTP
        {
            get { return usuarioftp; }
            set { usuarioftp = value; }
        }

        private string senhaftp;
        public virtual string SenhaFTP
        {
            get { return senhaftp; }
            set { senhaftp = value; }
        }

        private string enderecoftp;
        public virtual string EnderecoFTP
        {
            get { return enderecoftp; }
            set { enderecoftp = value; }
        }

        public ConfiguracaoContingenciaUrgence()
        {
        }

        public override bool Equals(object obj)
        {
            return this.Unidade.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() * 11;
        }

        /// <summary>
        /// Cria um FtpWebRequest
        /// </summary>
        /// <param name="diretorio">nome do diretório</param>
        /// <returns></returns>
        private FtpWebRequest CriarFtpWebRequest(string diretorio)
        {
            FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(diretorio);
            ftp.Credentials = new NetworkCredential(this.usuarioftp, this.senhaftp);

            ftp.KeepAlive = false;
            ftp.UseBinary = true;

            return ftp;
        }

        /// <summary>
        /// Cria um diretório no FTP
        /// </summary>
        /// <param name="path">nome do diretório</param>
        private void CriarDiretorio(string diretorio)
        {
            FtpWebRequest ftp = this.CriarFtpWebRequest(diretorio);

            ftp.Method = WebRequestMethods.Ftp.MakeDirectory;

            FtpWebResponse response = null;

            try
            {
                response = (FtpWebResponse)ftp.GetResponse();
            }
            catch
            {
                if (response != null && response.StatusCode != FtpStatusCode.ActionNotTakenFileUnavailable)
                    throw;
            }
            finally
            {
                if (response != null)
                    response.Close();
            }
        }

        /// <summary>
        /// Cria a pasta principal para um atendimento especificado.
        /// </summary>
        /// <param name="pasta">Nome da pasta a ser criada</param>
        public virtual void CriarPastaPrincipal(string pasta)
        {
            try
            {
                this.CriarDiretorio(this.enderecoftp + "/" + pasta);
            }
            catch
            {
                throw;
            }
            //FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(this.enderecoftp + "/" + pasta);
            //ftp.Credentials = new NetworkCredential(this.usuarioftp, this.senhaftp);

            //ftp.KeepAlive = false;
            //ftp.UseBinary = true;

            //ftp.Method = WebRequestMethods.Ftp.MakeDirectory;

            //FtpWebResponse response = null;

            //try
            //{
            //    response = (FtpWebResponse)ftp.GetResponse();
            //}
            //catch
            //{
            //    if (response != null &&
            //        response.StatusCode != FtpStatusCode.ActionNotTakenFileUnavailable)
            //        throw;
            //}
            //finally
            //{
            //    if (response != null)
            //        response.Close();
            //}
        }

        /// <summary>
        /// Cria as sub-pastas referentes aos relatórios existentes de um atendimento
        /// </summary>
        /// <param name="pastaprincipal">Nome da pasta principal na qual as sub-pastas serão criadas</param>
        public virtual void CriarSubPastas(string pastaprincipal)
        {
            string[] subpastas = new[] { 
                                        ConfiguracaoContingenciaUrgence.DIR_ACOLHIMENTO,
                                        ConfiguracaoContingenciaUrgence.DIR_APRAZAMENTOS,
                                        ConfiguracaoContingenciaUrgence.DIR_ATESTADOSRECEITAS,
                                        ConfiguracaoContingenciaUrgence.DIR_CONSULTAMEDICA,
                                        ConfiguracaoContingenciaUrgence.DIR_EVOLUCOESENFERMAGEM,
                                        ConfiguracaoContingenciaUrgence.DIR_EVOLUCOESMEDICAS,
                                        ConfiguracaoContingenciaUrgence.DIR_EXAMESELETIVOS,
                                        ConfiguracaoContingenciaUrgence.DIR_EXAMESINTERNOS,
                                        ConfiguracaoContingenciaUrgence.DIR_FICHAATENDIMENTO,
                                        ConfiguracaoContingenciaUrgence.DIR_PRESCRICOES,
                                        ConfiguracaoContingenciaUrgence.DIR_RELATORIOGERAL };
            //FtpWebRequest ftp = null;
            //FtpWebResponse response = null;

            for (int i = 0; i < subpastas.Length; i++)
            {
                try
                {
                    this.CriarDiretorio(this.enderecoftp + "/" + pastaprincipal + "/" + subpastas[i]);
                }
                catch
                {
                    throw;
                }
                //ftp = (FtpWebRequest)FtpWebRequest.Create(this.enderecoftp + "/" + pastaprincipal + "/" + subpastas[i]);
                //ftp.Credentials = new NetworkCredential(this.usuarioftp, this.senhaftp);

                //ftp.KeepAlive = false;
                //ftp.UseBinary = true;

                //ftp.Method = WebRequestMethods.Ftp.MakeDirectory;

                //response = null;

                //try
                //{
                //    response = (FtpWebResponse)ftp.GetResponse();
                //}
                //catch
                //{
                //    if (response.StatusCode != FtpStatusCode.ActionNotTakenFileUnavailable)
                //        throw;
                //}
                //finally
                //{
                //    response.Close();
                //}
            }
        }

        /// <summary>
        /// Salva o documento no formato PDF com o nome especificado
        /// </summary>
        /// <param name="documento">Documento</param>
        /// <param name="nome">Nome do arquivo</param>
        public virtual void SalvarDocumento(Stream documento, string nome)
        {
            FtpWebRequest ftp = this.CriarFtpWebRequest(this.enderecoftp + "/" + nome);

            //FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(this.enderecoftp + "/" + nome);
            //ftp.Credentials = new NetworkCredential(this.usuarioftp, this.senhaftp);

            //ftp.KeepAlive = false;
            //ftp.UseBinary = true;

            ftp.Method = WebRequestMethods.Ftp.UploadFile;
            Stream ftpstream = null;

            try
            {
                byte[] buffer = new byte[documento.Length];
                documento.Read(buffer, 0, buffer.Length);

                ftpstream = ftp.GetRequestStream();
                ftpstream.Write(buffer, 0, buffer.Length);
            }
            catch
            {
                throw;
            }
            finally
            {
                if (documento != null)
                    documento.Dispose();

                if (ftpstream != null)
                    ftpstream.Close();
            }
        }

        /// <summary>
        /// Salva o documento no formato PDF com o nome especificado
        /// </summary>
        /// <param name="documento">Documento</param>
        /// <param name="nome">Nome do arquivo</param>
        public virtual void SalvarDocumento(byte[] documento, string nome)
        {
            FtpWebRequest ftp = this.CriarFtpWebRequest(this.enderecoftp + "/" + nome);

            //FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(this.enderecoftp + "/" + nome);
            //ftp.Credentials = new NetworkCredential(this.usuarioftp, this.senhaftp);

            //ftp.KeepAlive = false;
            //ftp.UseBinary = true;

            ftp.Method = WebRequestMethods.Ftp.UploadFile;
            Stream ftpstream = null;

            try
            {
                ftpstream = ftp.GetRequestStream();
                ftpstream.Write(documento, 0, documento.Length);
            }
            catch
            {
                throw;
            }
            finally
            {
                if (ftpstream != null)
                    ftpstream.Close();
            }
        }
    }
}
