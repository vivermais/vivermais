﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.Model;
using ViverMais.DAOOracle;
using Oracle.DataAccess.Client;


namespace ViverMais.BLL
{
    public class DocumentoBLL
    {
        public static List<Documento> PesqusiarPorPaciente(Paciente paciente)
        {
            List<ControleDocumento> controlesDocumento = ControleDocumentoBLL.PesquisarPorPaciente(paciente);
            TipoDocumentoBLL.CompletarControles(controlesDocumento);
            DocumentoDAO dao = new DocumentoDAO();
            List<Documento> documentos = new List<Documento>();
            foreach (ControleDocumento controle in controlesDocumento)
            {
                documentos.Add(dao.PesquisarPorControle(controle));
            }
            return documentos;
        }

        public static Documento PesqusiarPorPaciente(string codigoDocumento, Paciente paciente)
        {
            ControleDocumentoDAO controleDAO = new ControleDocumentoDAO();
            DocumentoDAO dao = new DocumentoDAO();
            
            //Reaproveitei o método mas posso criar um que apenas retorne o documento
            List<ControleDocumento> controles = controleDAO.PesquisarPorPaciente(paciente);
            ControleDocumento controleEspecifico = controles.Find(x => x.TipoDocumento.Codigo == codigoDocumento);

            if (controleEspecifico != null)
                return dao.PesquisarPorControle(controleEspecifico);
            else
                return null;
        }

        public static void Cadastrar(Documento documento)
        {
            ControleDocumentoDAO controleDAO = new ControleDocumentoDAO();
            DocumentoDAO documentoDAO = new DocumentoDAO();
            OracleTransaction trans = null;
            try
            {
                controleDAO.Cadastrar(documento.ControleDocumento,ref trans);
                documentoDAO.Cadastrar(documento,ref trans);
                trans.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                trans.Rollback();
                throw ex;
            }
        }

        public static void Atualizar(Documento documento)
        {
            DocumentoDAO documentoDAO = new DocumentoDAO();
            documentoDAO.Atualizar(documento);
        }
    }
}
