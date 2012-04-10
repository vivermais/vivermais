using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Services.Description;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Xml.Serialization;
using System.Data;
using System.IO;

namespace ViverMais.Model
{
    public class WebServiceDinamico
    {
        private object instanciaWebService;

        private void InicializarServico(string enderecowsdl, string nomeservico, string protocolo)
        {
            System.Net.WebClient client = new System.Net.WebClient();

            try
            {
                System.IO.Stream stream = client.OpenRead(enderecowsdl);
                // Pega o arquivo WSDL do serviço parametrizado
                ServiceDescription description = ServiceDescription.Read(stream);

                // Inicializa a importação do serviço
                ServiceDescriptionImporter importer = new ServiceDescriptionImporter();
                importer.ProtocolName = protocolo;
                importer.AddServiceDescription(description, null, null);

                // Geração do proxy cliente
                importer.Style = ServiceDescriptionImportStyle.Client;

                // Geração das propriedades para representação dos valores primitivos
                importer.CodeGenerationOptions = System.Xml.Serialization.CodeGenerationOptions.GenerateProperties;

                // Inicialização da árvore do Code-DOM que irá importar o serviço
                CodeNamespace nmspace = new CodeNamespace();
                CodeCompileUnit unit1 = new CodeCompileUnit();
                unit1.Namespaces.Add(nmspace);

                ServiceDescriptionImportWarnings warning = importer.Import(nmspace, unit1);

                if (warning == 0)
                {
                    // Geração e impressão para o proxy.
                    CodeDomProvider provider1 = CodeDomProvider.CreateProvider("CSharp");

                    //Compilação do assembly com suas referências
                    string[] assemblyReferences = new string[2] { "System.Web.Services.dll", "System.Xml.dll" };
                    CompilerParameters parms = new CompilerParameters(assemblyReferences);
                    CompilerResults results = provider1.CompileAssemblyFromDom(parms, unit1);

                    string _erroscompilacao = string.Empty;

                    foreach (CompilerError oops in results.Errors)
                        _erroscompilacao += oops.ErrorText + " ";

                    if (_erroscompilacao != string.Empty)
                        throw new Exception(_erroscompilacao);

                    //Invocação da instância do web-service
                    instanciaWebService = results.CompiledAssembly.CreateInstance(nomeservico);
                }
                else
                    throw new Exception(warning.ToString());
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Construtor da classe WebServiceDinamico [Por default, o protocolo utilizado para comunicação é o Soap]
        /// </summary>
        /// <param name="enderecoWSDL">endereço wsdl do webservice</param>
        /// <param name="nomeServico">nome do serviço do webservice</param>
        public WebServiceDinamico(string enderecoWSDL, string nomeServico)
        {
            this.InicializarServico(enderecoWSDL, nomeServico, "Soap");
        }

        /// <summary>
        /// Construtor da classe WebServiceDinamico
        /// </summary>
        /// <param name="enderecoWSDL">endereço wsdl do webservice</param>
        /// <param name="nomeServico">nome do serviço do webservice</param>
        /// <param name="nomeProtocolo">nome do protocolo utilizado para comunicação</param>
        public WebServiceDinamico(string enderecoWSDL, string nomeServico, string nomeProtocolo)
        {
            this.InicializarServico(enderecoWSDL, nomeServico, nomeProtocolo);
        }

        /// <summary>
        /// Executa um Método do WebService retornando um tipo Object como resposta
        /// </summary>
        /// <param name="nomeMetodo">nome do método</param>
        /// <param name="parametros">parâmetros do método. [Caso o método não possua parâmetros setar valor NULL]</param>
        /// <returns>object</returns>
        public object ExecutarMetodoObj(string nomeMetodo, object[] parametros)
        {
            object resposta = null;

            try
            {
                Type tipoWebService = instanciaWebService.GetType();
                resposta = tipoWebService.InvokeMember(nomeMetodo, System.Reflection.BindingFlags.InvokeMethod, null, instanciaWebService, parametros);
            }
            catch
            {
                throw;
            }

            return resposta;
        }

        /// <summary>
        /// Executa um Método do WebService retornando um tipo DataSet como resposta
        /// </summary>
        /// <param name="nomeMetodo">nome do método</param>
        /// <param name="parametros">parâmetros do método. [Caso o método não possua parâmetros setar valor NULL]</param>
        /// <returns>DataSet</returns>
        public DataSet ExecutarMetodoDs(string nomeMetodo, object[] parametros)
        {
            DataSet ds = new DataSet();

            try
            {
                Type tipoWebService = instanciaWebService.GetType();
                object resposta = tipoWebService.InvokeMember(nomeMetodo, System.Reflection.BindingFlags.InvokeMethod, null, instanciaWebService, parametros);
                
                if (resposta != null)
                {
                    XmlSerializer xs = new XmlSerializer(resposta.GetType());
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    System.IO.StringWriter writer = new System.IO.StringWriter(sb);
                    xs.Serialize(writer, resposta);

                    ds.ReadXml(new StringReader(sb.ToString()));
                }
            }
            catch
            {
                throw;
            }

            return ds;
        }
    }
}
