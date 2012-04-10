using System;
using NHibernate;
using Vida.Model;
using System.Linq;
using System.Text;
using NHibernate.Criterion;
using System.Collections.Generic;
using Vida.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;

namespace Vida.DAO.AtendimentoMedico.Urgencia
{
    public class ClassificacaoRiscoDAO : UrgenciaServiceFacadeDAO, IClassificacaoRisco
    {
        void IClassificacaoRisco.SalvarClassificacaoRisco<T,F>(T classificacao, F file, string path, string path_temp)
        {
            Vida.Model.ClassificacaoRisco risco = (Vida.Model.ClassificacaoRisco)(object)classificacao;
            FileUpload arquivo = (FileUpload)(object)file;
            string name = string.Empty;

            using (Session.BeginTransaction())
            {
                try
                {
                    Session.Save(risco);

                    if (arquivo.HasFile)
                    {
                        if (File.Exists(path + "c_" + risco.Codigo.ToString() + System.IO.Path.GetExtension(arquivo.FileName)))
                        {
                            name = "c_" + risco.Codigo.ToString() + "_n" + System.IO.Path.GetExtension(arquivo.FileName);
                            File.Delete(path + "c_" + risco.Codigo.ToString() + System.IO.Path.GetExtension(arquivo.FileName));
                        }
                        else
                        {
                            name = "c_" + risco.Codigo.ToString() + System.IO.Path.GetExtension(arquivo.FileName);
                            File.Delete(path + "c_" + risco.Codigo.ToString() + "_n" + System.IO.Path.GetExtension(arquivo.FileName));
                        }

                        arquivo.SaveAs(path_temp + name);
                        File.Copy(path_temp + name, path + name, true);
                        File.Delete(path_temp + name);
                    }

                    if (!string.IsNullOrEmpty(name)) 
                    {
                        risco.Imagem = name;
                        Session.Update(risco);
                    }

                    Session.Transaction.Commit();
                }
                catch (Exception f)
                {
                    Session.Transaction.Rollback();
                    throw f;
                }
            }
        }
    }
}
