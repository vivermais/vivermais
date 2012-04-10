using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.IO;
using System.Xml;

namespace ViverMais.ServiceFacade
{
    public abstract class AbstractFactory
    {
        /// <summary>
        /// Obtém uma instância da classe mapeada para a Interface especificada. A classe
        /// deve implementar a interface.
        /// </summary>
        /// <typeparam name="T">Tipo da Interface</typeparam>
        /// <returns></returns>
        public static T GetInstance<T>()
        {
            throw new NotImplementedException();
        }

        protected static IDictionary<string, string> mapping = null;
        
        /// <summary>
        /// Singleton de associação das Interfaces com suas implementações concretas
        /// </summary>
        public static IDictionary<string, string> Mapping
        {
            get
            {
                if (AbstractFactory.mapping == null)
                    AbstractFactory.mapping = new Dictionary<string, string>();
                return AbstractFactory.mapping;
            }
        }

        /// <summary>
        /// Carrega o arquivo de Mapeamento entre as Interfaces de Serviço e as 
        /// Implementações Concretas
        /// </summary>
        /// <param name="input">Stream com o XML do mapeamento</param>
        public static void Load(TextReader input)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Carrega o arquivo de Mapeamento entre as Interfaces de Serviço e as 
        /// Implementações Concretas
        /// </summary>
        /// <param name="filepath">Caminho do arquivo XML de mapeamento</param>
        public static void Load(string filepath)
        {
            throw new NotImplementedException();
        }
    }
}
