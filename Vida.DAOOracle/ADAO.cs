﻿using System;
using System.Collections.Generic;
using System.Text;
using Oracle.DataAccess.Client;
using System.Data;
using ViverMais.Model;
using ViverMais.DAL;

namespace ViverMais.DAOOracle
{
    public abstract class ADAO<T> where T : AModel
    {
        protected StringBuilder sqlText;
        protected List<OracleParameter> parametros;

        /// <summary>
        /// Construtor protegido que instancia os atributos da classe;
        /// </summary>
        protected ADAO()
        {
            sqlText = new StringBuilder();
            parametros = new List<OracleParameter>();
        }

        /// <summary>
        /// Método responsável por inserir ou atualizar um registro em banco de dados.
        /// A decisão é baseada no método Persistido() dos objetos que implementam
        /// a classe AModel.
        /// </summary>
        /// <param name="objeto">Objeto de um tipo que implemente AModel</param>
        /// <returns>O próprio objeto atualizado</returns>
        public T Inserir(T objeto)
        {
            //new DAOLogXml().SalvarLog(objeto, 1);

            if (objeto.Persistido())
            {
                T returnedObject = Atualizar(objeto);
                //Chama a função para gerar log através de xml
                new DAOLogXml().SalvarLog(objeto, 2);
                return returnedObject;
            }
            else
            {
                T returnedObject = Cadastrar(objeto);
                //Chama a função para gerar log através de xml
                new DAOLogXml().SalvarLog(objeto, 1);
                return returnedObject;
            }
        }

        /// <summary>
        /// O mesmo que o anterior com a diferença de receber um objeto do tipo 
        /// OracleTransacion
        /// </summary>
        /// <param name="objeto"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public T Inserir(T objeto, ref OracleTransaction trans)
        {
            //new DAOLogXml().SalvarLog(ref trans, objeto, 1);

            if (objeto.Persistido())
            {
                T returnedObject = Atualizar(objeto, ref trans);
                //Chama a função para gerar log através de xml
                new DAOLogXml().SalvarLog(ref trans, objeto, 2);
                return returnedObject;
            }
            else
            {
                T returnedObject = Cadastrar(objeto, ref trans);
                //Chama a função para gerar log através de xml
                new DAOLogXml().SalvarLog(ref trans, objeto, 1);
                return returnedObject;

            }
        }

        /// <summary>
        /// O mesmo que o anterior com a diferença de receber um objeto do tipo e não salva o Log
        /// OracleTransacion
        /// </summary>
        /// <param name="objeto"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public T InserirSemLog(T objeto, ref OracleTransaction trans)
        {
            //new DAOLogXml().SalvarLog(ref trans, objeto, 1);

            if (objeto.Persistido())
            {
                T returnedObject = Atualizar(objeto, ref trans);
                //Chama a função para gerar log através de xml
                //new DAOLogXml().SalvarLog(ref trans, objeto, 2);
                return returnedObject;
            }
            else
            {
                T returnedObject = Cadastrar(objeto, ref trans);
                //Chama a função para gerar log através de xml
                //new DAOLogXml().SalvarLog(ref trans, objeto, 1);
                return returnedObject;

            }
        }

        /// <summary>
        /// Método que executa os passos necessários para uma atualização em banco de dados.
        /// </summary>
        /// <param name="objeto">Objeto que será atualizado</param>
        /// <returns></returns>
        private T Atualizar(T objeto)
        {
            GerarQueryAtualizacao();
            GerarParametrosAtualizacao(objeto);
            DALOracle.ExecuteNonQuery(CommandType.Text, sqlText.ToString(), parametros.ToArray());
            sqlText.Remove(0, sqlText.Length);
            parametros.Clear();

            //new DAOLogXml().SalvarLog(objeto, 1);

            return objeto;
        }

        /// <summary>
        /// O mesmo que o anterior, contudo recebe um objeto do tipo OracleTransaction.
        /// </summary>
        /// <param name="objeto"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        private T Atualizar(T objeto, ref OracleTransaction trans)
        {
            GerarQueryAtualizacao();
            GerarParametrosAtualizacao(objeto);
            DALOracle.ExecuteNonQuery(ref trans, CommandType.Text, sqlText.ToString(), parametros.ToArray());
            sqlText.Remove(0, sqlText.Length);
            parametros.Clear();

            return objeto;
        }

        /// <summary>
        /// Método responsável por executar os passos para cadastro em
        /// banco de dados.
        /// </summary>
        /// <param name="objeto">Objeto que será cadastrado.</param>
        /// <returns></returns>
        protected virtual T Cadastrar(T objeto)
        {
            GerarQueryCadastro();
            GerarParametrosCadastro(objeto);
            DALOracle.ExecuteNonQuery(CommandType.Text, sqlText.ToString(), parametros.ToArray());
            sqlText.Remove(0, sqlText.Length);
            parametros.Clear();
            GerarQueryBuscarID();
            DataSet dataReader = DALOracle.ExecuteReaderDs(CommandType.Text, sqlText.ToString(), parametros.ToArray());
            sqlText.Remove(0, sqlText.Length);
            parametros.Clear();
            if (dataReader != null)
            {
                SetarCodigoObjeto(dataReader.Tables[0].Rows[0], objeto);
            }

            return objeto;
        }

        /// <summary>
        /// O mesmo que o anterior, contudo recebe um objeto OracleTransaction.
        /// </summary>
        /// <param name="objeto"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        protected virtual T Cadastrar(T objeto, ref OracleTransaction trans)
        {
            GerarQueryCadastro();
            GerarParametrosCadastro(objeto);
            DALOracle.ExecuteNonQuery(ref trans, CommandType.Text, sqlText.ToString(), parametros.ToArray());
            sqlText.Remove(0, sqlText.Length);
            parametros.Clear();
            GerarQueryBuscarID();
            DataSet dataReader = DALOracle.ExecuteReaderDs(ref trans, CommandType.Text, sqlText.ToString(), parametros.ToArray());
            sqlText.Remove(0, sqlText.Length);
            parametros.Clear();
            if (dataReader != null)
            {
                SetarCodigoObjeto(dataReader.Tables[0].Rows[0], objeto);
            }

            return objeto;
        }


        /// <summary>
        /// Método responsável por realizar os passos para
        /// exclusão de registro em banco de dados.
        /// </summary>
        /// <param name="codigo">Código do ítem que será excluído</param>
        public void Remover(int codigo)
        {
            GerarQueryRemocao();

            parametros.Add(new OracleParameter("Codigo", OracleDbType.Int32));
            parametros[0].Value = codigo;

            DALOracle.ExecuteNonQuery(CommandType.Text, sqlText.ToString(), parametros.ToArray());
            sqlText.Remove(0, sqlText.Length);
            parametros.Clear();
        }

        /// <summary>
        /// O mesmo que o anterior, contudo recebe um objeto do tipo OracleTransaction.
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="trans"></param>
        public void Remover(int codigo, ref OracleTransaction trans)
        {
            GerarQueryRemocao();

            parametros.Add(new OracleParameter("Codigo", OracleDbType.Int32));
            parametros[0].Value = codigo;

            DALOracle.ExecuteNonQuery(ref trans, CommandType.Text, sqlText.ToString(), parametros.ToArray());
            sqlText.Remove(0, sqlText.Length);
            parametros.Clear();
        }

        /// <summary>
        /// Realiza pesquisa em banco de dados através do código do objeto.
        /// Normalmente este código é a chave.
        /// </summary>
        /// <param name="codigo">Código chave do objeto a ser pesquisado</param>
        /// <returns>Objeto selecionado ou null caso não encontre.</returns>
        public T Pesquisar(int codigo)
        {
            GerarQueryPesquisaPorCodigo();

            parametros.Add(new OracleParameter("Codigo", OracleDbType.Int32));
            parametros[0].Value = codigo;

            Type tipo = Type.GetType(typeof(T).AssemblyQualifiedName);
            T objeto = null;

            DataSet dataReader = DALOracle.ExecuteReaderDs(CommandType.Text, sqlText.ToString(), parametros.ToArray());
            sqlText.Remove(0, sqlText.Length);
            parametros.Clear();
            if (dataReader != null && dataReader.Tables[0].Rows.Count > 0)
            {
                objeto = (T)Activator.CreateInstance(tipo);
                MontarObjeto(dataReader.Tables[0].Rows[0], objeto);
            }
            else
            {
                throw new ObjectNotFoundException();
            }
            return objeto;


        }

        /// <summary>
        /// O mesmo que o anterior, contudo recebe um objeto do tipo OracleTransaction.
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public T Pesquisar(int codigo, ref OracleTransaction trans)
        {
            GerarQueryPesquisaPorCodigo();

            parametros.Add(new OracleParameter("Codigo", OracleDbType.Int32));
            parametros[0].Value = codigo;

            Type tipo = Type.GetType(typeof(T).AssemblyQualifiedName);
            T objeto = null;

            DataSet dataReader = DALOracle.ExecuteReaderDs(ref trans, CommandType.Text, sqlText.ToString(), parametros.ToArray());
            sqlText.Remove(0, sqlText.Length);
            parametros.Clear();
            if (dataReader != null)
            {
                objeto = (T)Activator.CreateInstance(tipo);
                MontarObjeto(dataReader.Tables[0].Rows[0], objeto);
            }
            else
            {
                throw new ObjectNotFoundException();
            }
            return objeto;


        }

        public T Pesquisar(string codigo)
        {
            GerarQueryPesquisaPorCodigo();

            parametros.Add(new OracleParameter("Codigo", OracleDbType.Varchar2));
            parametros[0].Value = codigo;

            Type tipo = Type.GetType(typeof(T).AssemblyQualifiedName);
            T objeto = null;


            DataSet dataReader = DALOracle.ExecuteReaderDs(CommandType.Text, sqlText.ToString(), parametros.ToArray());
            sqlText.Remove(0, sqlText.Length);
            parametros.Clear();
            if (dataReader != null && dataReader.Tables[0].Rows.Count > 0)
            {
                objeto = (T)Activator.CreateInstance(tipo);
                MontarObjeto(dataReader.Tables[0].Rows[0], objeto);
            }
            else
            {
                throw new ObjectNotFoundException();
            }
            return objeto;
        }

        public T Pesquisar(string codigo, ref OracleTransaction trans)
        {
            GerarQueryPesquisaPorCodigo();

            parametros.Add(new OracleParameter("Codigo", OracleDbType.Varchar2));
            parametros[0].Value = codigo;

            Type tipo = Type.GetType(typeof(T).AssemblyQualifiedName);
            T objeto = null;


            DataSet dataReader = DALOracle.ExecuteReaderDs(ref trans, CommandType.Text, sqlText.ToString(), parametros.ToArray());
            sqlText.Remove(0, sqlText.Length);
            parametros.Clear();
            if (dataReader != null && dataReader.Tables[0].Rows.Count > 0)
            {
                objeto = (T)Activator.CreateInstance(tipo);
                MontarObjeto(dataReader.Tables[0].Rows[0], objeto);
            }
            else
            {
                throw new ObjectNotFoundException();
            }
            return objeto;
        }

        public virtual void Completar(T objeto)
        {
            GerarQueryPesquisaPorCodigo();
            GeraParametrosPesquisaPorCodigo(objeto);

            DataSet dataReader = DALOracle.ExecuteReaderDs(CommandType.Text, sqlText.ToString(), parametros.ToArray());
            sqlText.Remove(0, sqlText.Length);
            parametros.Clear();
            if (dataReader != null && dataReader.Tables[0].Rows.Count > 0)
            {
                MontarObjeto(dataReader.Tables[0].Rows[0], objeto);
            }
            //else
            //{
            //    throw new ObjectNotFoundException();
            //}
        }

        public virtual void Completar(T objeto, ref OracleTransaction trans)
        {
            GerarQueryPesquisaPorCodigo();
            GeraParametrosPesquisaPorCodigo(objeto);

            DataSet dataReader = DALOracle.ExecuteReaderDs(ref trans, CommandType.Text, sqlText.ToString(), parametros.ToArray());
            sqlText.Remove(0, sqlText.Length);
            parametros.Clear();
            if (dataReader != null)
            {
                MontarObjeto(dataReader.Tables[0].Rows[0], objeto);
            }
            else
            {
                throw new ObjectNotFoundException();
            }
        }

        public List<T> ListarTodos()
        {
            GerarQueryPesquisaTodos();

            DataSet ds = DALOracle.ExecuteReaderDs(CommandType.Text, sqlText.ToString(), parametros.ToArray());


            Type tipo = Type.GetType(typeof(T).AssemblyQualifiedName);
            T objeto = null;

            List<T> listaRetorno = new List<T>();

            foreach (DataRow drc in ds.Tables[0].Rows)
            {
                objeto = (T)Activator.CreateInstance(tipo);
                MontarObjeto(drc, objeto);
                listaRetorno.Add(objeto);
            }
            return listaRetorno;
        }

        public List<T> ListarTodos(ref OracleTransaction trans)
        {
            GerarQueryPesquisaTodos();
            DataSet dataReader = DALOracle.ExecuteReaderDs(ref trans, CommandType.Text, sqlText.ToString(), parametros.ToArray());

            Type tipo = Type.GetType(typeof(T).AssemblyQualifiedName);
            T objeto = null;

            List<T> listaRetorno = new List<T>();

            foreach (DataRow drc in dataReader.Tables[0].Rows)
            {
                objeto = (T)Activator.CreateInstance(tipo);
                MontarObjeto(drc, objeto);
                listaRetorno.Add(objeto);
            }
            return listaRetorno;
        }

        //Métodos que serão implementados nas classes que implementem esta classe
        //abstrata.
        protected abstract void GerarQueryPesquisaTodos();

        protected abstract void GerarQueryCadastro();
        protected abstract void GerarParametrosCadastro(T objeto);

        protected abstract void GerarQueryAtualizacao();
        protected abstract void GerarParametrosAtualizacao(T objeto);

        protected abstract void GerarQueryRemocao();

        protected abstract void GerarQueryBuscarID();

        protected abstract void GerarQueryPesquisaPorCodigo();
        protected abstract void GeraParametrosPesquisaPorCodigo(T objeto);

        protected abstract void MontarObjeto(OracleDataReader dataReader, T objeto);
        protected abstract void MontarObjeto(DataRow drc, T objeto);


        protected abstract void SetarCodigoObjeto(OracleDataReader dataReader, T objeto);
        protected abstract void SetarCodigoObjeto(DataRow dataRow, T objeto);

    }
}
