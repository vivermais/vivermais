﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    public class SenhadorUrgence
    {
        public static string NomeServico = "TKTServicesService";

        private string enderecowebservice;
        public virtual string EnderecoWebService
        {
            set { enderecowebservice = value; }
            get { return enderecowebservice; }
        }

        private string enderecologinsenhador;
        public virtual string EnderecoLoginSenhador
        {
            set { enderecologinsenhador = value; }
            get { return enderecologinsenhador; }
        }

        private string cnesunidade;
        public virtual string CNESUnidade
        {
            set { cnesunidade = value; }
            get { return cnesunidade; }
        }

        private int codigo;
        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        ///private WebServiceDinamico _webService;

        public SenhadorUrgence()
        {
        }

        //public SenhadorUrgence(Usuario usuarioAtendimento)
        //{
        //    _webService = new WebServiceDinamico(this.enderecowebservice, "TKTServicesService");
        //}

        //public string CadastrarUsuario(Usuario usuario, CBO cbo)
        //{
        //    string tipoUsuario = string.Empty;
        //    string resultadoCadastro = string.Empty;

        //    if (UsuarioProfissionalUrgence.isMedico(cbo))
        //        tipoUsuario = "M";
        //    else if (UsuarioProfissionalUrgence.isEnfemeiro(cbo))
        //        tipoUsuario = "E";

        //    if (tipoUsuario != string.Empty)
        //    {
        //        object o = null;

        //        try
        //        {
        //            o = _webService.ExecutarMetodoObj("SmsCadastrarUsuario", new string[3] { usuario.Codigo.ToString(), usuario.CartaoSUS, tipoUsuario });
        //        }
        //        catch
        //        {
        //            throw;
        //        }

        //        resultadoCadastro = o.ToString();
        //    }

        //    return resultadoCadastro;
        //}

        //public string EmitirBilheteAcolhimento(Prontuario prontuario)
        //{
        //    string bilhete = string.Empty;
        //    object o = null;

        //    try
        //    {
        //        o = _webService.ExecutarMetodoObj("SmsEmitirBilhete", new string[5] { "A", prontuario.Paciente.Codigo.ToString(), null, null, prontuario.Paciente.Nome });
        //        bilhete = o.ToString();
        //    }
        //    catch
        //    {
        //        throw;
        //    }

        //    return bilhete;
        //}

        //public string EmitirBilheteAtendimento(Prontuario prontuario)
        //{
        //    string bilhete = string.Empty;
        //    object o = null;

        //    try
        //    {
        //         o = _webService.ExecutarMetodoObj("SmsEmitirBilhete", new string[5] { "R", prontuario.Paciente.Codigo.ToString(), ((prontuario.ClassificacaoRiscoAcolhimento.Ordem - 4) * (-1)).ToString(), null, prontuario.Paciente.Nome });
        //         bilhete = o.ToString();
        //    }
        //    catch
        //    {
        //        throw;
        //    }

        //    return bilhete;
        //}
    }
}
