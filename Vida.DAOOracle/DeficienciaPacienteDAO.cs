﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.Model;
using System.Data;
using Oracle.DataAccess.Client;
using ViverMais.DAL;

namespace ViverMais.DAOOracle
{
    public class DeficienciaPacienteDAO : ADAO<DeficienciaPaciente>
    {
        public void Cadastrar(DeficienciaPaciente deficiencia, string co_usuario, ref OracleTransaction trans)
        {
            sqlText.Append("INSERT INTO TB_PMS_DEFICIENCIA_USUARIO (co_usuario,deficiencia,ortese)");
            sqlText.Append("VALUES (:CodigoUsuario,:Deficiente,:UsaOrtese)");

            parametros.Add(new OracleParameter("CodigoUsuario", OracleDbType.Varchar2));
            parametros.Add(new OracleParameter("Deficiente", OracleDbType.Char));
            parametros.Add(new OracleParameter("UsaOrtese", OracleDbType.Char));

            parametros[0].Value = co_usuario;
            parametros[1].Value = deficiencia.Deficiente ? 'Y' : 'N';
            parametros[2].Value = deficiencia.UsaOrtese ? 'Y' : 'N';

            DALOracle.ExecuteNonQuery(ref trans, CommandType.Text, sqlText.ToString(), parametros.ToArray());
            sqlText.Remove(0, sqlText.Length);
            parametros.Clear();

            this.InserirDeficiencias(deficiencia.Deficiencias, co_usuario, ref trans);
            this.InserirComunicacoes(deficiencia.Comunicacoes, co_usuario, ref trans);
            this.InserirOrigens(deficiencia.Origens, co_usuario, ref trans);
            this.InserirProteses(deficiencia.Proteses, co_usuario, ref trans);
            this.InserirLocomocoes(deficiencia.Locomocoes, co_usuario, ref trans);
        }

        public void Atualizar(DeficienciaPaciente NovaDeficiencia, DeficienciaPaciente AntigaDeficiencia,
    string co_usuario, ref OracleTransaction trans)
        {
            sqlText.Append("UPDATE TB_PMS_DEFICIENCIA_USUARIO SET deficiencia = :Deficiente,");
            sqlText.Append("ortese =:UsaOrtese ");
            sqlText.Append("WHERE co_usuario =:CodigoUsuario");

            //sqlText.Append("UPDATE TB_PMS_DEFICIENCIA_USUARIO SET deficiencia = '" + (NovaDeficiencia.Deficiente ? "Y" : "N") + "',");
            //sqlText.Append("ortese = '" + (NovaDeficiencia.UsaOrtese ? "Y" : "N") + "' ");
            //sqlText.Append("WHERE co_usuario = '" + co_usuario + "'");

            parametros.Add(new OracleParameter("CodigoUsuario", co_usuario));
            parametros.Add(new OracleParameter("Deficiente", NovaDeficiencia.Deficiente ? 'Y' : 'N'));
            parametros.Add(new OracleParameter("UsaOrtese", NovaDeficiencia.UsaOrtese ? 'Y' : 'N'));
            
            DALOracle.ExecuteNonQuery(ref trans, CommandType.Text, sqlText.ToString(), true, parametros.ToArray());
            sqlText.Remove(0, sqlText.Length);
            parametros.Clear();

            #region NOVOS REGISTROS
            var novasdeficiencias = (from m in NovaDeficiencia.Deficiencias
                                     where !AntigaDeficiencia.Deficiencias.Select(p => p.Codigo)
                                     .Contains(m.Codigo)
                                     select m);
            this.InserirDeficiencias(novasdeficiencias.ToList(), co_usuario, ref trans);

            var novascomunicacoes = (from m in NovaDeficiencia.Comunicacoes
                                     where !AntigaDeficiencia.Comunicacoes.Select(p => p.Codigo)
                                     .Contains(m.Codigo)
                                     select m);
            this.InserirComunicacoes(novascomunicacoes.ToList(), co_usuario, ref trans);

            var novasproteses = (from m in NovaDeficiencia.Proteses
                                 where !AntigaDeficiencia.Proteses.Select(p => p.Codigo)
                                 .Contains(m.Codigo)
                                 select m);
            this.InserirProteses(novasproteses.ToList(), co_usuario, ref trans);

            var novaslocomocoes = (from m in NovaDeficiencia.Locomocoes
                                   where !AntigaDeficiencia.Locomocoes.Select(p => p.Codigo)
                                   .Contains(m.Codigo)
                                   select m);
            this.InserirLocomocoes(novaslocomocoes.ToList(), co_usuario, ref trans);

            var novasorigens = (from m in NovaDeficiencia.Origens
                                where !AntigaDeficiencia.Origens.Select(p => p.Codigo)
                                .Contains(m.Codigo)
                                select m);
            this.InserirOrigens(novasorigens.ToList(), co_usuario, ref trans);
            #endregion

            #region ANTIGOS REGISTROS
            var antigasdeficiencias = (from m in AntigaDeficiencia.Deficiencias
                                       where !NovaDeficiencia.Deficiencias.Select(p => p.Codigo)
                                       .Contains(m.Codigo)
                                       select m);

            foreach (Deficiencia def in antigasdeficiencias.ToList())
            {
                sqlText.Append("DELETE FROM rl_pms_usuario_deficiencia ");
                sqlText.Append("WHERE co_usuario=:CodigoUsuario AND co_deficiencia=:Deficiencia");

                parametros.Add(new OracleParameter("CodigoUsuario", OracleDbType.Varchar2));
                parametros.Add(new OracleParameter("Deficiencia", OracleDbType.Int32));

                parametros[0].Value = co_usuario;
                parametros[1].Value = def.Codigo;

                DALOracle.ExecuteNonQuery(ref trans, CommandType.Text, sqlText.ToString(), parametros.ToArray());
                sqlText.Remove(0, sqlText.Length);
                parametros.Clear();
            }

            var antigascomunicacoes = (from m in AntigaDeficiencia.Comunicacoes
                                       where !NovaDeficiencia.Comunicacoes.Select(p => p.Codigo)
                                       .Contains(m.Codigo)
                                       select m);

            foreach (ComunicacaoDeficiencia def in antigascomunicacoes.ToList())
            {
                sqlText.Append("DELETE FROM rl_pms_usuario_comdeficiencia ");
                sqlText.Append("WHERE co_usuario=:CodigoUsuario AND co_comunicacao=:Comunicacao");

                parametros.Add(new OracleParameter("CodigoUsuario", OracleDbType.Varchar2));
                parametros.Add(new OracleParameter("Comunicacao", OracleDbType.Int32));

                parametros[0].Value = co_usuario;
                parametros[1].Value = def.Codigo;

                DALOracle.ExecuteNonQuery(ref trans, CommandType.Text, sqlText.ToString(), parametros.ToArray());
                sqlText.Remove(0, sqlText.Length);
                parametros.Clear();
            }

            var antigasproteses = (from m in AntigaDeficiencia.Proteses
                                   where !NovaDeficiencia.Proteses.Select(p => p.Codigo)
                                   .Contains(m.Codigo)
                                   select m);

            foreach (ProteseDeficiencia def in antigasproteses.ToList())
            {
                sqlText.Append("DELETE FROM rl_pms_usuario_protdeficiencia ");
                sqlText.Append("WHERE co_usuario=:CodigoUsuario AND co_protese=:Protese");

                parametros.Add(new OracleParameter("CodigoUsuario", OracleDbType.Varchar2));
                parametros.Add(new OracleParameter("Protese", OracleDbType.Int32));

                parametros[0].Value = co_usuario;
                parametros[1].Value = def.Codigo;

                DALOracle.ExecuteNonQuery(ref trans, CommandType.Text, sqlText.ToString(), parametros.ToArray());
                sqlText.Remove(0, sqlText.Length);
                parametros.Clear();
            }

            var antigasorigens = (from m in AntigaDeficiencia.Origens
                                  where !NovaDeficiencia.Origens.Select(p => p.Codigo)
                                  .Contains(m.Codigo)
                                  select m);

            foreach (OrigemDeficiencia def in antigasorigens.ToList())
            {
                sqlText.Append("DELETE FROM rl_pms_usuario_origdeficiencia ");
                sqlText.Append("WHERE co_usuario=:CodigoUsuario AND co_origem=:Origem");

                parametros.Add(new OracleParameter("CodigoUsuario", OracleDbType.Varchar2));
                parametros.Add(new OracleParameter("Origem", OracleDbType.Int32));

                parametros[0].Value = co_usuario;
                parametros[1].Value = def.Codigo;

                DALOracle.ExecuteNonQuery(ref trans, CommandType.Text, sqlText.ToString(), parametros.ToArray());
                sqlText.Remove(0, sqlText.Length);
                parametros.Clear();
            }

            var antigaslocomocoes = (from m in AntigaDeficiencia.Locomocoes
                                     where !NovaDeficiencia.Locomocoes.Select(p => p.Codigo)
                                     .Contains(m.Codigo)
                                     select m);

            foreach (LocomocaoDeficiencia def in antigaslocomocoes.ToList())
            {
                sqlText.Append("DELETE FROM rl_pms_usuario_locdeficiencia ");
                sqlText.Append("WHERE co_usuario=:CodigoUsuario AND co_locomocao=:Locomocao");

                parametros.Add(new OracleParameter("CodigoUsuario", OracleDbType.Varchar2));
                parametros.Add(new OracleParameter("Locomocao", OracleDbType.Int32));

                parametros[0].Value = co_usuario;
                parametros[1].Value = def.Codigo;

                DALOracle.ExecuteNonQuery(ref trans, CommandType.Text, sqlText.ToString(), parametros.ToArray());
                sqlText.Remove(0, sqlText.Length);
                parametros.Clear();
            }
            #endregion
        }

        private void InserirDeficiencias(IList<Deficiencia> deficiencias, string co_usuario, ref OracleTransaction trans)
        {
            foreach (Deficiencia def in deficiencias)
            {
                sqlText.Append("INSERT INTO rl_pms_usuario_deficiencia (co_usuario,co_deficiencia)");
                sqlText.Append("VALUES (:CodigoUsuario,:Deficiente)");

                parametros.Add(new OracleParameter("CodigoUsuario", OracleDbType.Varchar2));
                parametros.Add(new OracleParameter("Deficiente", OracleDbType.Int32));

                parametros[0].Value = co_usuario;
                parametros[1].Value = def.Codigo;

                DALOracle.ExecuteNonQuery(ref trans, CommandType.Text, sqlText.ToString(), parametros.ToArray());
                sqlText.Remove(0, sqlText.Length);
                parametros.Clear();
            }
        }

        private void InserirOrigens(IList<OrigemDeficiencia> origens, string co_usuario, ref OracleTransaction trans)
        {
            foreach (OrigemDeficiencia def in origens)
            {
                sqlText.Append("INSERT INTO rl_pms_usuario_origdeficiencia (co_usuario,co_origem)");
                sqlText.Append("VALUES (:CodigoUsuario,:Origem)");

                parametros.Add(new OracleParameter("CodigoUsuario", OracleDbType.Varchar2));
                parametros.Add(new OracleParameter("Origem", OracleDbType.Int32));

                parametros[0].Value = co_usuario;
                parametros[1].Value = def.Codigo;

                DALOracle.ExecuteNonQuery(ref trans, CommandType.Text, sqlText.ToString(), parametros.ToArray());
                sqlText.Remove(0, sqlText.Length);
                parametros.Clear();
            }
        }

        private void InserirComunicacoes(IList<ComunicacaoDeficiencia> comunicacoes, string co_usuario, ref OracleTransaction trans)
        {
            foreach (ComunicacaoDeficiencia def in comunicacoes)
            {
                sqlText.Append("INSERT INTO rl_pms_usuario_comdeficiencia (co_usuario,co_comunicacao)");
                sqlText.Append("VALUES (:CodigoUsuario,:Comunicacao)");

                parametros.Add(new OracleParameter("CodigoUsuario", OracleDbType.Varchar2));
                parametros.Add(new OracleParameter("Comunicacao", OracleDbType.Int32));

                parametros[0].Value = co_usuario;
                parametros[1].Value = def.Codigo;

                DALOracle.ExecuteNonQuery(ref trans, CommandType.Text, sqlText.ToString(), parametros.ToArray());
                sqlText.Remove(0, sqlText.Length);
                parametros.Clear();
            }
        }

        private void InserirProteses(IList<ProteseDeficiencia> proteses, string co_usuario, ref OracleTransaction trans)
        {
            foreach (ProteseDeficiencia def in proteses)
            {
                sqlText.Append("INSERT INTO rl_pms_usuario_protdeficiencia (co_usuario,co_protese)");
                sqlText.Append("VALUES (:CodigoUsuario,:Protese)");

                parametros.Add(new OracleParameter("CodigoUsuario", OracleDbType.Varchar2));
                parametros.Add(new OracleParameter("Protese", OracleDbType.Int32));

                parametros[0].Value = co_usuario;
                parametros[1].Value = def.Codigo;

                DALOracle.ExecuteNonQuery(ref trans, CommandType.Text, sqlText.ToString(), parametros.ToArray());
                sqlText.Remove(0, sqlText.Length);
                parametros.Clear();
            }
        }

        private void InserirLocomocoes(IList<LocomocaoDeficiencia> locomocoes, string co_usuario, ref OracleTransaction trans)
        {
            foreach (LocomocaoDeficiencia def in locomocoes)
            {
                sqlText.Append("INSERT INTO rl_pms_usuario_locdeficiencia (co_usuario,co_locomocao)");
                sqlText.Append("VALUES (:CodigoUsuario,:Locomocao)");

                parametros.Add(new OracleParameter("CodigoUsuario", OracleDbType.Varchar2));
                parametros.Add(new OracleParameter("Locomocao", OracleDbType.Int32));

                parametros[0].Value = co_usuario;
                parametros[1].Value = def.Codigo;

                DALOracle.ExecuteNonQuery(ref trans, CommandType.Text, sqlText.ToString(), parametros.ToArray());
                sqlText.Remove(0, sqlText.Length);
                parametros.Clear();
            }
        }

        public DeficienciaPaciente PesquisarPorPaciente(string co_usuario, ref OracleTransaction trans)
        {
            sqlText.Remove(0, sqlText.Length);
            parametros.Clear();

            sqlText.Append("select * from tb_pms_deficiencia_usuario where co_usuario =:CodigoUsuario");
            parametros.Add(new OracleParameter("CodigoUsuario", OracleDbType.Varchar2));
            parametros[0].Value = co_usuario;

            DataSet dataReader = DALOracle.ExecuteReaderDs(ref trans, CommandType.Text, sqlText.ToString(), parametros.ToArray());

            DeficienciaPaciente deficiencia = null;

            if (dataReader != null && dataReader.Tables[0].Rows.Count > 0)
            {
                deficiencia = new DeficienciaPaciente();
                deficiencia.Deficiente = dataReader.Tables[0].Rows[0]["deficiencia"].ToString().Equals("Y") ? true : false;
                deficiencia.UsaOrtese = dataReader.Tables[0].Rows[0]["ortese"].ToString().Equals("Y") ? true : false;
            }

            if (deficiencia != null)
            {
                #region COMUNICACAO
                List<ComunicacaoDeficiencia> comunicacoes = new List<ComunicacaoDeficiencia>();
                sqlText.Remove(0, sqlText.Length);
                sqlText.Append("SELECT a.no_comunicacao, a.co_comunicacao FROM tb_pms_comunicacaodeficiencia a,");
                sqlText.Append(" rl_pms_usuario_comdeficiencia r WHERE r.co_usuario=:CodigoUsuario AND r.co_comunicacao=a.co_comunicacao");

                dataReader = DALOracle.ExecuteReaderDs(ref trans, CommandType.Text, sqlText.ToString(), parametros.ToArray());

                foreach (DataRow row in dataReader.Tables[0].Rows)
                {
                    ComunicacaoDeficiencia def = new ComunicacaoDeficiencia();
                    def.Codigo = int.Parse(row["co_comunicacao"].ToString());
                    def.Nome = row["no_comunicacao"].ToString();
                    comunicacoes.Add(def);
                }
                #endregion

                #region PROTESE
                List<ProteseDeficiencia> proteses = new List<ProteseDeficiencia>();
                sqlText.Remove(0, sqlText.Length);
                sqlText.Append("SELECT a.no_protese, a.co_protese FROM tb_pms_protesedeficiencia a,");
                sqlText.Append(" rl_pms_usuario_protdeficiencia r WHERE r.co_usuario=:CodigoUsuario AND r.co_protese=a.co_protese");

                dataReader = DALOracle.ExecuteReaderDs(ref trans, CommandType.Text, sqlText.ToString(), parametros.ToArray());

                foreach (DataRow row in dataReader.Tables[0].Rows)
                {
                    ProteseDeficiencia def = new ProteseDeficiencia();
                    def.Codigo = int.Parse(row["co_protese"].ToString());
                    def.Nome = row["no_protese"].ToString();
                    proteses.Add(def);
                }
                #endregion

                #region LOCOMOCAO
                List<LocomocaoDeficiencia> locomocoes = new List<LocomocaoDeficiencia>();
                sqlText.Remove(0, sqlText.Length);
                sqlText.Append("SELECT a.no_locomocao, a.co_locomocao FROM tb_pms_locomocaodeficiencia a,");
                sqlText.Append(" rl_pms_usuario_locdeficiencia r WHERE r.co_usuario=:CodigoUsuario AND r.co_locomocao=a.co_locomocao");

                dataReader = DALOracle.ExecuteReaderDs(ref trans, CommandType.Text, sqlText.ToString(), parametros.ToArray());

                foreach (DataRow row in dataReader.Tables[0].Rows)
                {
                    LocomocaoDeficiencia def = new LocomocaoDeficiencia();
                    def.Codigo = int.Parse(row["co_locomocao"].ToString());
                    def.Nome = row["no_locomocao"].ToString();
                    locomocoes.Add(def);
                }
                #endregion

                #region ORIGEM
                List<OrigemDeficiencia> origens = new List<OrigemDeficiencia>();
                sqlText.Remove(0, sqlText.Length);
                sqlText.Append("SELECT a.no_origem, a.co_origem FROM tb_pms_origemdeficiencia a,");
                sqlText.Append("rl_pms_usuario_origdeficiencia r WHERE r.co_usuario=:CodigoUsuario AND r.co_origem=a.co_origem");

                dataReader = DALOracle.ExecuteReaderDs(ref trans, CommandType.Text, sqlText.ToString(), parametros.ToArray());

                foreach (DataRow row in dataReader.Tables[0].Rows)
                {
                    OrigemDeficiencia def = new OrigemDeficiencia();
                    def.Codigo = int.Parse(row["co_origem"].ToString());
                    def.Nome = row["no_origem"].ToString();
                    origens.Add(def);
                }
                #endregion

                #region DEFICIENCIA
                List<Deficiencia> deficiencias = new List<Deficiencia>();
                sqlText.Remove(0, sqlText.Length);
                sqlText.Append("SELECT a.no_deficiencia, a.co_deficiencia FROM tb_pms_deficiencia a,");
                sqlText.Append("rl_pms_usuario_deficiencia r WHERE r.co_usuario=:CodigoUsuario AND r.co_deficiencia=a.co_deficiencia");

                dataReader = DALOracle.ExecuteReaderDs(ref trans, CommandType.Text, sqlText.ToString(), parametros.ToArray());

                foreach (DataRow row in dataReader.Tables[0].Rows)
                {
                    Deficiencia def = new Deficiencia();
                    def.Codigo = int.Parse(row["co_deficiencia"].ToString());
                    def.Nome = row["no_deficiencia"].ToString();
                    deficiencias.Add(def);
                }
                #endregion

                deficiencia.Locomocoes = locomocoes;
                deficiencia.Origens = origens;
                deficiencia.Deficiencias = deficiencias;
                deficiencia.Proteses = proteses;
                deficiencia.Comunicacoes = comunicacoes;
            }

            parametros.Clear();
            sqlText.Remove(0, sqlText.Length);

            return deficiencia;
        }

        public DeficienciaPaciente PesquisarPorPaciente(string co_usuario)
        {
            sqlText.Remove(0, sqlText.Length);
            parametros.Clear();

            sqlText.Append("select * from tb_pms_deficiencia_usuario where co_usuario =:CodigoUsuario");
            parametros.Add(new OracleParameter("CodigoUsuario", OracleDbType.Varchar2));
            parametros[0].Value = co_usuario;

            DataSet dataReader = DALOracle.ExecuteReaderDs(CommandType.Text, sqlText.ToString(), parametros.ToArray());

            DeficienciaPaciente deficiencia = null;

            if (dataReader != null && dataReader.Tables[0].Rows.Count > 0)
            {
                deficiencia = new DeficienciaPaciente();
                deficiencia.Deficiente = dataReader.Tables[0].Rows[0]["deficiencia"].ToString().Equals("Y") ? true : false;
                deficiencia.UsaOrtese = dataReader.Tables[0].Rows[0]["ortese"].ToString().Equals("Y") ? true : false;
            }

            if (deficiencia != null)
            {
                #region COMUNICACAO
                List<ComunicacaoDeficiencia> comunicacoes = new List<ComunicacaoDeficiencia>();
                sqlText.Remove(0, sqlText.Length);
                sqlText.Append("SELECT a.no_comunicacao, a.co_comunicacao FROM tb_pms_comunicacaodeficiencia a,");
                sqlText.Append(" rl_pms_usuario_comdeficiencia r WHERE r.co_usuario=:CodigoUsuario AND r.co_comunicacao=a.co_comunicacao");

                dataReader = DALOracle.ExecuteReaderDs(CommandType.Text, sqlText.ToString(), parametros.ToArray());

                foreach (DataRow row in dataReader.Tables[0].Rows)
                {
                    ComunicacaoDeficiencia def = new ComunicacaoDeficiencia();
                    def.Codigo = int.Parse(row["co_comunicacao"].ToString());
                    def.Nome = row["no_comunicacao"].ToString();
                    comunicacoes.Add(def);
                }
                #endregion

                #region PROTESE
                List<ProteseDeficiencia> proteses = new List<ProteseDeficiencia>();
                sqlText.Remove(0, sqlText.Length);
                sqlText.Append("SELECT a.no_protese, a.co_protese FROM tb_pms_protesedeficiencia a,");
                sqlText.Append(" rl_pms_usuario_protdeficiencia r WHERE r.co_usuario=:CodigoUsuario AND r.co_protese=a.co_protese");

                dataReader = DALOracle.ExecuteReaderDs(CommandType.Text, sqlText.ToString(), parametros.ToArray());

                foreach (DataRow row in dataReader.Tables[0].Rows)
                {
                    ProteseDeficiencia def = new ProteseDeficiencia();
                    def.Codigo = int.Parse(row["co_protese"].ToString());
                    def.Nome = row["no_protese"].ToString();
                    proteses.Add(def);
                }
                #endregion

                #region LOCOMOCAO
                List<LocomocaoDeficiencia> locomocoes = new List<LocomocaoDeficiencia>();
                sqlText.Remove(0, sqlText.Length);
                sqlText.Append("SELECT a.no_locomocao, a.co_locomocao FROM tb_pms_locomocaodeficiencia a,");
                sqlText.Append(" rl_pms_usuario_locdeficiencia r WHERE r.co_usuario=:CodigoUsuario AND r.co_locomocao=a.co_locomocao");

                dataReader = DALOracle.ExecuteReaderDs(CommandType.Text, sqlText.ToString(), parametros.ToArray());

                foreach (DataRow row in dataReader.Tables[0].Rows)
                {
                    LocomocaoDeficiencia def = new LocomocaoDeficiencia();
                    def.Codigo = int.Parse(row["co_locomocao"].ToString());
                    def.Nome = row["no_locomocao"].ToString();
                    locomocoes.Add(def);
                }
                #endregion

                #region ORIGEM
                List<OrigemDeficiencia> origens = new List<OrigemDeficiencia>();
                sqlText.Remove(0, sqlText.Length);
                sqlText.Append("SELECT a.no_origem, a.co_origem FROM tb_pms_origemdeficiencia a,");
                sqlText.Append("rl_pms_usuario_origdeficiencia r WHERE r.co_usuario=:CodigoUsuario AND r.co_origem=a.co_origem");

                dataReader = DALOracle.ExecuteReaderDs(CommandType.Text, sqlText.ToString(), parametros.ToArray());

                foreach (DataRow row in dataReader.Tables[0].Rows)
                {
                    OrigemDeficiencia def = new OrigemDeficiencia();
                    def.Codigo = int.Parse(row["co_origem"].ToString());
                    def.Nome = row["no_origem"].ToString();
                    origens.Add(def);
                }
                #endregion

                #region DEFICIENCIA
                List<Deficiencia> deficiencias = new List<Deficiencia>();
                sqlText.Remove(0, sqlText.Length);
                sqlText.Append("SELECT a.no_deficiencia, a.co_deficiencia FROM tb_pms_deficiencia a,");
                sqlText.Append("rl_pms_usuario_deficiencia r WHERE r.co_usuario=:CodigoUsuario AND r.co_deficiencia=a.co_deficiencia");

                dataReader = DALOracle.ExecuteReaderDs(CommandType.Text, sqlText.ToString(), parametros.ToArray());

                foreach (DataRow row in dataReader.Tables[0].Rows)
                {
                    Deficiencia def = new Deficiencia();
                    def.Codigo = int.Parse(row["co_deficiencia"].ToString());
                    def.Nome = row["no_deficiencia"].ToString();
                    deficiencias.Add(def);
                }
                #endregion

                deficiencia.Locomocoes = locomocoes;
                deficiencia.Origens = origens;
                deficiencia.Deficiencias = deficiencias;
                deficiencia.Proteses = proteses;
                deficiencia.Comunicacoes = comunicacoes;
            }

            parametros.Clear();
            sqlText.Remove(0, sqlText.Length);

            return deficiencia;
        }

        protected override void GerarQueryAtualizacao()
        {
            throw new NotImplementedException();
        }

        protected override void GerarQueryPesquisaTodos()
        {
            throw new NotImplementedException();
        }

        protected override void GerarQueryPesquisaPorCodigo()
        {
            throw new NotImplementedException();
        }

        protected override void GeraParametrosPesquisaPorCodigo(DeficienciaPaciente objeto)
        {
            throw new NotImplementedException();
        }

        protected override void MontarObjeto(OracleDataReader dataReader, DeficienciaPaciente objeto)
        {
            throw new NotImplementedException();
        }

        protected override void MontarObjeto(DataRow drc, DeficienciaPaciente objeto)
        {
            throw new NotImplementedException();
        }

        protected override void GerarQueryCadastro()
        {
            throw new NotImplementedException();
        }

        protected override void GerarParametrosAtualizacao(DeficienciaPaciente objeto)
        {
            throw new NotImplementedException();
        }

        protected override void GerarQueryRemocao()
        {
            throw new NotImplementedException();
        }

        protected override void GerarQueryBuscarID()
        {
            throw new NotImplementedException();
        }

        protected override void SetarCodigoObjeto(OracleDataReader dataReader, DeficienciaPaciente objeto)
        {
            throw new NotImplementedException();
        }

        protected override void SetarCodigoObjeto(DataRow dataRow, DeficienciaPaciente objeto)
        {
            throw new NotImplementedException();
        }

        protected override void GerarParametrosCadastro(DeficienciaPaciente objeto)
        {
            throw new NotImplementedException();
        }
    }
}
