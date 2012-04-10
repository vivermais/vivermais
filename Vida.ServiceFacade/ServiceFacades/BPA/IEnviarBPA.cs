using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Collections;

namespace ViverMais.ServiceFacade.ServiceFacades.BPA
{
    public interface IEnviarBPA
    {
        T BuscarProtocoloEnvio<T>(object usuario, object estabelecimento, DateTime data);
        IList<T> ListarProtocolos<T>(object estabelecimento, DateTime data);
        IList<T> ListarProtocolosPorCompetencia<T>(object competencia);
        IList<T> ListarProtocolosPorEstabelecimento<T>(object estabelecimento);
        IList<T> ListarProtocolos<T>(object estabelecimento, int ano);
        IList<T> ListarCompetencias<T>(bool aberta);
        T BuscarCompetencia<T>(int ano, int mes);
        MemoryStream GerarArquivoBPAAPAC<T>(T _arquivoAPAC);
        MemoryStream GerarArquivoBPAI<T>(T _arquivoBPA);
        MemoryStream GerarArquivoBPAC<T>(T _arquivoBPA);
        DataTable RetornaRelatorioRemessa<T>(T _arquivoBPA);
        Hashtable RetornaRelatorioPrevia<T>(T _arquivoBPA);
    }
}
