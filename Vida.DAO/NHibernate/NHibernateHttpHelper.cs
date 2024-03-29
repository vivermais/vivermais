using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using System.Xml;
using System.IO;
using System.Reflection;
using NHibernate.Caches.SysCache;
using ViverMais.DAL;

namespace ViverMais.DAO
{
    public sealed class NHibernateHttpHelper
    {
        private const string CurrentSessionKey = "nhibernate.current_session";
        static Dictionary<string, ISessionFactory> sessionFactories = null;

        public static Dictionary<string, ISessionFactory> SessionFactories
        {
            get
            {
                if (NHibernateHttpHelper.sessionFactories == null)
                    NHibernateHttpHelper.sessionFactories = new Dictionary<string, ISessionFactory>();
                return NHibernateHttpHelper.sessionFactories;
            }
            set { NHibernateHttpHelper.sessionFactories = value; }
        }

        static NHibernateHttpHelper()
        {
            /*
             * 16-07-2010 -  Escrito por Murilo Freire
             * Apesar das bases de dados do Farmacia, Urgencia, Vacina, etc, apontarem para o mesmo lugar 
             * (a base de dados do ViverMais) foram criadas conex�es diferentes para, caso seja necess�ria a mudan�a
             * de alguma destas bases de servidor, estas j� estarem "pr�-definidas", diminuindo as modifica��es
             * na aplica��o. Al�m disto, estas bases j� n�o possuem refer�ncias diretas para a base do ViverMais,
             * suas refer�ncias s�o controladas na aplica��o, o que possibilita facilmente tais mudan�as de
             * servidor.
             * 
             * Esta arquitetura foi adotada para possibilitar o funcionamento das bases em servidores distintos
             *  devido a poss�veis problemas de desempenho que tais servidores possam apresentar.
             */

            #region Paciente 

            //M�dulos Cadastro, Vacina, Farmacia Urgencia, Seguran�a, Profissional, Regula��o
            NHibernate.Cfg.Configuration cfg = new NHibernate.Cfg.Configuration();
            cfg.Properties.Add("connection.provider", "NHibernate.Connection.DriverConnectionProvider");
            cfg.Properties.Add("connection.driver_class", "NHibernate.Driver.OracleClientDriver");

            //ATEN��O
            //Alterar a conex�o em ConexaoBancoSingle
            cfg.Properties.Add("connection.connection_string", ConexaoBancoSingle.conexao);

            cfg.Properties.Add("dialect", "NHibernate.Dialect.Oracle9Dialect");
            cfg.Properties.Add("use_outer_join", "true");
            cfg.Properties.Add("cache.provider_class", "NHibernate.Caches.SysCache.SysCacheProvider, NHibernate.Caches.SysCache");
            cfg.Properties.Add("cache.use_second_level_cache", "true");
            cfg.Properties.Add("show_sql", "true");
            cfg.AddAssembly("ViverMais.Model");
            SessionFactories.Add("ViverMais", cfg.BuildSessionFactory());
            #endregion

            #region CONEXOES_ANTIGAS
            /*
            #region Vacina
            cfg = new NHibernate.Cfg.Configuration();
            cfg.Properties.Add("connection.provider", "NHibernate.Connection.DriverConnectionProvider");
            cfg.Properties.Add("connection.driver_class", "NHibernate.Driver.OracleClientDriver");
            
            //SMS-ViverMais01
            //cfg.Properties.Add("connection.connection_string", ViverMais.DAO.Properties.Resources.ViverMaisProducaoConectionString);

            //Isis
            //cfg.Properties.Add("connection.connection_string", ViverMais.DAO.Properties.Resources.ViverMaisIsisConnectionString);

            //Gaia
            cfg.Properties.Add("connection.connection_string", ViverMais.DAO.Properties.Resources.ViverMaisGaiaConnectionString);

            cfg.Properties.Add("dialect", "NHibernate.Dialect.Oracle9Dialect");
            cfg.Properties.Add("use_outer_join", "true");
            cfg.Properties.Add("cache.provider_class", "NHibernate.Caches.SysCache.SysCacheProvider, NHibernate.Caches.SysCache");
            cfg.Properties.Add("cache.use_second_level_cache", "true");
            cfg.Properties.Add("show_sql", "true");
            cfg.AddAssembly("ViverMais.Model");
            SessionFactories.Add("vacina", cfg.BuildSessionFactory());
            #endregion
            */
            /*
            #region Urgencia

            cfg = new NHibernate.Cfg.Configuration();
            cfg.Properties.Add("connection.provider", "NHibernate.Connection.DriverConnectionProvider");
            cfg.Properties.Add("connection.driver_class", "NHibernate.Driver.OracleClientDriver");

            //SMS - Urgencia .. LocalHost
            //cfg.Properties.Add("connection.connection_string", ViverMais.DAO.Properties.Resources.ViverMaisServidorUrgenciaConnectionString);

            //SMS-ViverMais01
            //cfg.Properties.Add("connection.connection_string", ViverMais.DAO.Properties.Resources.ViverMaisProducaoConectionString);

            //Isis
            //cfg.Properties.Add("connection.connection_string", ViverMais.DAO.Properties.Resources.ViverMaisIsisConnectionString);

            //Gaia
            cfg.Properties.Add("connection.connection_string", ViverMais.DAO.Properties.Resources.ViverMaisGaiaConnectionString);

            cfg.Properties.Add("dialect", "NHibernate.Dialect.Oracle9Dialect");
            cfg.Properties.Add("use_outer_join", "true");
            cfg.Properties.Add("cache.provider_class", "NHibernate.Caches.SysCache.SysCacheProvider, NHibernate.Caches.SysCache");
            cfg.Properties.Add("cache.use_second_level_cache", "true");
            cfg.Properties.Add("show_sql", "true");
            cfg.AddAssembly("ViverMais.Model");
            SessionFactories.Add("urgencia", cfg.BuildSessionFactory());
            */

            /* Sql Server
          cfg = new NHibernate.Cfg.Configuration();
          cfg.Properties.Add("connection.provider", "NHibernate.Connection.DriverConnectionProvider");
          cfg.Properties.Add("connection.driver_class", "NHibernate.Driver.SqlClientDriver");
          //PRODU��O
          cfg.Properties.Add("connection.connection_string", "Server=172.20.12.44;Database=urgence;User ID=usr_urgence;Password=*usr_urgence*;");
          //Homologa
          //cfg.Properties.Add("connection.connection_string", "Server=172.22.6.2;Database=DB_URGENCE_HM;User ID=urgencehm;Password=*urgencehm*;");
          cfg.Properties.Add("dialect", "NHibernate.Dialect.MsSql2000Dialect");
          cfg.Properties.Add("use_outer_join", "true");
          cfg.Properties.Add("cache.provider_class", "NHibernate.Caches.SysCache.SysCacheProvider, NHibernate.Caches.SysCache");
          cfg.Properties.Add("cache.use_second_level_cache", "true");
          //cfg.Properties.Add("hibernate.cache.use_query_cache", "true");
          //cfg.Properties.Add("show_sql", "true");
          cfg.AddAssembly("ViverMais.Model");
          //SessionFactories.Add("urgencia", cfg.BuildSessionFactory());*/

           // #endregion
            
            /*
            #region Agendamento

            cfg = new NHibernate.Cfg.Configuration();
            cfg.Properties.Add("connection.provider", "NHibernate.Connection.DriverConnectionProvider");
            cfg.Properties.Add("connection.driver_class", "NHibernate.Driver.OracleClientDriver");

            //SMS-ViverMais01
            //cfg.Properties.Add("connection.connection_string", ViverMais.DAO.Properties.Resources.ViverMaisProducaoConectionString);

            //Isis
            //cfg.Properties.Add("connection.connection_string", ViverMais.DAO.Properties.Resources.ViverMaisIsisConnectionString);

            //Gaia
             cfg.Properties.Add("connection.connection_string", ViverMais.DAO.Properties.Resources.ViverMaisGaiaConnectionString);

            cfg.Properties.Add("dialect", "NHibernate.Dialect.Oracle9Dialect");
            cfg.Properties.Add("use_outer_join", "true");
            cfg.Properties.Add("cache.provider_class", "NHibernate.Caches.SysCache.SysCacheProvider, NHibernate.Caches.SysCache");
            cfg.Properties.Add("cache.use_second_level_cache", "true");
            cfg.Properties.Add("show_sql", "true");
            cfg.AddAssembly("ViverMais.Model");
            SessionFactories.Add("agendamento", cfg.BuildSessionFactory());
            */
            /*
                        cfg = new NHibernate.Cfg.Configuration();
                        cfg.Properties.Add("connection.provider", "NHibernate.Connection.DriverConnectionProvider");
                        cfg.Properties.Add("connection.driver_class", "NHibernate.Driver.SqlClientDriver");
                        cfg.Properties.Add("connection.connection_string", "Server=172.22.6.2;Database=DB_CYGNUS2_HM;User ID=cygnus2hm;Password=*cygnus2hm*;");
                        cfg.Properties.Add("dialect", "NHibernate.Dialect.MsSql2000Dialect");
                        cfg.Properties.Add("use_outer_join", "true");
                        cfg.Properties.Add("cache.provider_class", "NHibernate.Caches.SysCache.SysCacheProvider, NHibernate.Caches.SysCache");
                        cfg.Properties.Add("cache.use_second_level_cache", "true");
                        //cfg.Properties.Add("hibernate.cache.use_query_cache", "true");
                        //cfg.Properties.Add("show_sql", "true");
                        cfg.AddAssembly("ViverMais.Model");
                        //SessionFactories.Add("agendamento", cfg.BuildSessionFactory()); */

            //#endregion

            /*
            #region Farmacia


            cfg = new NHibernate.Cfg.Configuration();
            cfg.Properties.Add("connection.provider", "NHibernate.Connection.DriverConnectionProvider");
            cfg.Properties.Add("connection.driver_class", "NHibernate.Driver.OracleClientDriver");

            //SMS - Urgencia .. LocalHost
            //cfg.Properties.Add("connection.connection_string", ViverMais.DAO.Properties.Resources.ViverMaisServidorUrgenciaConnectionString);

            //SMS-ViverMais01
            //cfg.Properties.Add("connection.connection_string", ViverMais.DAO.Properties.Resources.ViverMaisProducaoConectionString);

            //Isis
            //cfg.Properties.Add("connection.connection_string", ViverMais.DAO.Properties.Resources.ViverMaisIsisConnectionString);

            //Gaia
             cfg.Properties.Add("connection.connection_string", ViverMais.DAO.Properties.Resources.ViverMaisGaiaConnectionString);

            cfg.Properties.Add("dialect", "NHibernate.Dialect.Oracle9Dialect");
            cfg.Properties.Add("use_outer_join", "true");
            cfg.Properties.Add("cache.provider_class", "NHibernate.Caches.SysCache.SysCacheProvider, NHibernate.Caches.SysCache");
            cfg.Properties.Add("cache.use_second_level_cache", "true");
            cfg.Properties.Add("show_sql", "true");
            cfg.AddAssembly("ViverMais.Model");
            SessionFactories.Add("farmacia", cfg.BuildSessionFactory());
            */
            /*
            cfg = new NHibernate.Cfg.Configuration();
            cfg.Properties.Add("connection.provider", "NHibernate.Connection.DriverConnectionProvider");
            cfg.Properties.Add("connection.driver_class", "NHibernate.Driver.SqlClientDriver");
            //PRODU��O
            cfg.Properties.Add("connection.connection_string", "Server=172.20.12.44;Database=farmacia;User ID=usr_farmacia;Password=*usr_farmacia*;");
            //Homologa
            //cfg.Properties.Add("connection.connection_string", "Server=172.22.6.2;Database=DB_SISFARMA_HM;User ID=sisfarma;Password=sisfarma;");
            cfg.Properties.Add("dialect", "NHibernate.Dialect.MsSql2000Dialect");
            cfg.Properties.Add("use_outer_join", "true");
            cfg.Properties.Add("cache.provider_class", "NHibernate.Caches.SysCache.SysCacheProvider, NHibernate.Caches.SysCache");
            cfg.Properties.Add("cache.use_second_level_cache", "true");
            //cfg.Properties.Add("hibernate.cache.use_query_cache", "true");
            //cfg.Properties.Add("show_sql", "true");
            cfg.AddAssembly("ViverMais.Model");
            //SessionFactories.Add("farmacia", cfg.BuildSessionFactory());
            */
            // #endregion
            #endregion


            #region Laboratorio

            //Configura��o NHIBERNATE Laborat�rio
            cfg = new NHibernate.Cfg.Configuration();
            cfg.Properties.Add("connection.provider", "NHibernate.Connection.DriverConnectionProvider");
            cfg.Properties.Add("connection.driver_class", "NHibernate.Driver.OracleClientDriver");
            //Isis
            cfg.Properties.Add("connection.connection_string", ViverMais.DAO.Properties.Resources.LaboratorioConnectionString);
            cfg.Properties.Add("dialect", "NHibernate.Dialect.Oracle9Dialect");
            cfg.Properties.Add("use_outer_join", "true");
            cfg.Properties.Add("cache.provider_class", "NHibernate.Caches.SysCache.SysCacheProvider, NHibernate.Caches.SysCache");
            cfg.Properties.Add("cache.use_second_level_cache", "true");
            cfg.Properties.Add("show_sql", "true");
            cfg.AddAssembly("ViverMais.Model");
            SessionFactories.Add("laboratorio", cfg.BuildSessionFactory());
            #endregion
        }

        public static ISession GetCurrentSession(string alias)
        {
            HttpContext context = HttpContext.Current;
            ISession currentSession = null;

            if (context == null)
            {
                currentSession = SessionFactories[alias] == null ? currentSession = null : SessionFactories[alias].OpenSession();
                return currentSession;
            }
            else if (context != null)
            {
                currentSession = SessionFactories[alias].OpenSession();
                context.Items[alias + ".nhibernate.current_session"] = currentSession;
            }

            return currentSession;
        }

        //public static ISession GetCurrentSession()
        //{
        //    HttpContext context = HttpContext.Current;
        //    ISession currentSession = context.Items[CurrentSessionKey] as ISession;
        //    if (currentSession == null)
        //    {
        //        currentSession = sessionFactory.OpenSession();
        //        context.Items[CurrentSessionKey] = currentSession;
        //    }
        //    return currentSession;
        //}

        //public static void CloseSession()
        //{
        //    HttpContext context = HttpContext.Current;
        //    ISession currentSession = context.Items[CurrentSessionKey] as ISession;
        //    if (currentSession == null)
        //    { //No current session                
        //        return;
        //    }
        //    currentSession.Close();
        //    context.Items.Remove(CurrentSessionKey);
        //}

        //public static void CloseSessionFactory()
        //{
        //    if (sessionFactory != null)
        //    {
        //        sessionFactory.Close();
        //    }
        //}

        ~NHibernateHttpHelper()
        {
            foreach (ISessionFactory factory in NHibernateHttpHelper.SessionFactories.Values)
                factory.Close();
            //CloseSession();
            //CloseSessionFactory();
        }
    }
}
