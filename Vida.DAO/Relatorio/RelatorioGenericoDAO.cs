using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.Relatorio;
using NHibernate;
using NHibernate.Criterion;
using System.Collections;
using System.Data;
using System.Reflection;
using ViverMais.Model.Entities;

namespace ViverMais.DAO.Relatorio
{
    public class RelatorioGenericoDAO : ViverMaisServiceFacadeDAO, IRelatorioGenerico
    {
        /// <summary>
        /// Adiciona uma Expressão ao ICriteria
        /// </summary>
        /// <param name="type">Tipo da Entidade do ICriteria</param>
        /// <param name="criteria">ICriteria</param>
        /// <param name="propriedade">Nome da Property do Tipo</param>
        /// <param name="expressao">Expressão do ICriteria</param>
        /// <param name="valor">Valor para Comparação</param>
        private void AddExpressionToCriteria(Type type, ICriteria criteria, string propriedade, int expressao, object valor)
        {
            //Converte o valor object no tipo da Property usando Reflection a partir do Type
            object value = Convert.ChangeType(valor, type.GetProperty(propriedade).PropertyType);
            //Adiciona a Expression no Criteria
            switch (expressao)
            {
                case 0:
                    criteria.Add(Expression.Eq(propriedade, value));
                    break;
                case 1:
                    criteria.Add(Expression.Not(Expression.Eq(propriedade, value)));
                    break;
                case 2:
                    criteria.Add(Expression.Gt(propriedade, value));
                    break;
                case 3:
                    criteria.Add(Expression.Ge(propriedade, value));
                    break;
                case 4:
                    criteria.Add(Expression.Lt(propriedade, value));
                    break;
                case 5:
                    criteria.Add(Expression.Le(propriedade, value));
                    break;
                case 6:
                    criteria.Add(Expression.Like(propriedade, value));
                    break;
            }
        }

        /// <summary>
        /// Monta um DataTable a partir de uma lista de Resultados do Critéria. Só
        /// Serão incluídos no DataTable os campos que tenha o Attribute RelatorioAttribute
        /// e que esse esteja como Visible
        /// </summary>
        /// <param name="type">Tipo da Entidade do Resultado</param>
        /// <param name="result">Resultado da Consulta</param>
        /// <returns>DataTable com os Reseultados</returns>
        private DataTable GetDataTable(Type type, IList result)
        {
            DataTable table = new DataTable();
            //Extrai todas as Properties do Type ordenadas por nome
            PropertyInfo[] properties = type.GetProperties().OrderBy(p => p.Name).ToArray<PropertyInfo>();
            //Monta as Columns do DataTable
            foreach (var item in properties)
            {
                //Verifica se a Property contém o Attribute
                Attribute att = Attribute.GetCustomAttribute(item, typeof(RelatorioAttribute));
                if (att != null)
                {
                    //Se a Property tem o Attribute, deve estar Visible
                    if (((RelatorioAttribute)att).IsVisible)
                    {
                        //Adiciona o DataColumn para a Property
                        DataColumn column = new DataColumn(item.Name);
                        table.Columns.Add(column);
                    }
                }
            }

            //Preenche as Rows do DataTable
            foreach (var item in result)
            {
                DataRow row = table.NewRow();
                int j = 0;
                for (int i = 0; i < properties.Count(); i++)
                {
                    //Verifica se a Property tem o Attirbute
                    Attribute att = Attribute.GetCustomAttribute(properties[i], typeof(RelatorioAttribute));
                    if (att != null)
                    {
                        if (((RelatorioAttribute)att).IsVisible)
                        {
                            //O índice das Properties não é o mesmo necessariamente das Colunms do
                            //DataTable, visto que nem toda Property do Type está incluída, fazendo-se necessária
                            //então a criação do índice j para as Rows
                            row[j] = properties[i].GetValue(item, null);
                            j++;
                        }
                    }
                }
                table.Rows.Add(row);
            }
            return table;
        }

        #region IRelatorioGenerico Members

        System.Data.DataTable IRelatorioGenerico.ObterRelatorio(string tipo, string propriedade, int expressao, object valor)
        {
            Type type = Type.GetType(tipo);
            ICriteria criteria = Session.CreateCriteria(type);
            this.AddExpressionToCriteria(type, criteria, propriedade, expressao, valor);
            IList result = criteria.List();
            DataTable table = this.GetDataTable(type, result);
            return table;
        }

        DataTable IRelatorioGenerico.ObterRelatorio(string tipo, object[] expressions)
        {
            Type type = Type.GetType(tipo);
            ICriteria criteria = Session.CreateCriteria(type);
            foreach (var item in expressions)
            {
                RelatorioExpression re = (RelatorioExpression)item;
                this.AddExpressionToCriteria(type, criteria, re.Propriedade, re.Operador, re.Value);
            }
            IList result = criteria.List();
            DataTable table = this.GetDataTable(type, result);
            return table;
        }

        #endregion

    }
}
