/*
* --------------------------------------------------------------------
* Simple Password Strength Checker
* by Siddharth S, www.ssiddharth.com, hello@ssiddharth.com
* for Net Tuts, www.net.tutsplus.com
* Version: 1.0, 05.10.2009 	
* --------------------------------------------------------------------
*/

//Com Master => ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder3_ccAlterarSenha_complexity
//Sem Master => ccalterarsenha_complexity

//Com Master => ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder3_ccAlterarSenha_tbxSenhaNova
//Sem Master => ccalterarsenha_tbxSenhaNova

$(document).ready(function() {
    var strPassword;
    var charPassword;
    var complexity;
    var valorSenha;

    if (document.getElementById("ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder3_ccAlterarSenha_tbxSenhaNova")
        == null || document.getElementById("ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder3_ccAlterarSenha_tbxSenhaNova")
        == "") {
        complexity = $("#ctl00_ContentPlaceHolder1_ccalterarsenha_complexity");
        valorSenha = "#ctl00_ContentPlaceHolder1_ccalterarsenha_tbxSenhaNova";
    }else{
        complexity = $("#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder3_ccAlterarSenha_complexity");
        valorSenha = "#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder3_ccAlterarSenha_tbxSenhaNova";
    }

    var minPasswordLength = 6;
    var baseScore = 0, score = 0;

    var num = {};
    num.Excess = 0;
    num.Upper = 0;
    num.Numbers = 0;
    num.Symbols = 0;

    var bonus = {};
    bonus.Excess = 3;
    bonus.Upper = 4;
    bonus.Numbers = 5;
    bonus.Symbols = 5;
    bonus.Combo = 0;
    bonus.FlatLower = 0;
    bonus.FlatNumber = 0;

    outputResult();
    $(valorSenha).bind("keyup", checkVal);

    function checkVal() {
        init();

        if (charPassword.length >= minPasswordLength) {
            baseScore = 50;
            analyzeString();
            calcComplexity();
        }
        else {
            baseScore = 0;
        }

        outputResult();
    }

    function init() {
        strPassword = $(valorSenha).val();
        charPassword = strPassword.split("");

        num.Excess = 0;
        num.Upper = 0;
        num.Numbers = 0;
        num.Symbols = 0;
        bonus.Combo = 0;
        bonus.FlatLower = 0;
        bonus.FlatNumber = 0;
        baseScore = 0;
        score = 0;
    }

    function analyzeString() {
        for (i = 0; i < charPassword.length; i++) {
            if (charPassword[i].match(/[A-Z]/g)) { num.Upper++; }
            if (charPassword[i].match(/[0-9]/g)) { num.Numbers++; }
            if (charPassword[i].match(/(.*[!,@,#,$,%,^,&,*,?,_,~])/)) { num.Symbols++; }
        }

        num.Excess = charPassword.length - minPasswordLength;

        if (num.Upper && num.Numbers && num.Symbols) {
            bonus.Combo = 25;
        }

        else if ((num.Upper && num.Numbers) || (num.Upper && num.Symbols) || (num.Numbers && num.Symbols)) {
            bonus.Combo = 15;
        }

        if (strPassword.match(/^[\sa-z]+$/)) {
            bonus.FlatLower = -15;
        }

        if (strPassword.match(/^[\s0-9]+$/)) {
            bonus.FlatNumber = -35;
        }
    }

    function calcComplexity() {
        score = baseScore + (num.Excess * bonus.Excess) + (num.Upper * bonus.Upper) + (num.Numbers * bonus.Numbers) + (num.Symbols * bonus.Symbols) + bonus.Combo + bonus.FlatLower + bonus.FlatNumber;

    }

    function outputResult() {
        if ($(valorSenha).val() == "") {
            complexity.html("").removeClass("weak strong stronger strongest").addClass("default");
        }
        else if (charPassword.length < minPasswordLength) {
            complexity.html("Digite pelo menos " + minPasswordLength + " caracteres!").removeClass("strong stronger strongest").addClass("weak");
        }
        else if (score < 50) {
            complexity.html("Fraco!").removeClass("strong stronger strongest").addClass("weak");
        }
        else if (score >= 50 && score < 75) {
            complexity.html("M&eacutedio!").removeClass("stronger strongest").addClass("strong");
        }
        else if (score >= 75 && score < 100) {
            complexity.html("Forte!").removeClass("strongest").addClass("stronger");
        }
        else if (score >= 100) {
            complexity.html("Seguro!").addClass("strongest");
        }


        //		$("#details").html("Base Score :<span class=\"value\">" + baseScore  + "</span>"
        //					   + "<br />Length Bonus :<span class=\"value\">" + (num.Excess*bonus.Excess) + " ["+num.Excess+"x"+bonus.Excess+"]</span> " 
        //					   + "<br />Upper case bonus :<span class=\"value\">" + (num.Upper*bonus.Upper) + " ["+num.Upper+"x"+bonus.Upper+"]</span> "
        //					   + "<br />Number Bonus :<span class=\"value\"> " + (num.Numbers*bonus.Numbers) + " ["+num.Numbers+"x"+bonus.Numbers+"]</span>"
        //					   + "<br />Symbol Bonus :<span class=\"value\"> " + (num.Symbols*bonus.Symbols) + " ["+num.Symbols+"x"+bonus.Symbols+"]</span>"
        //					   + "<br />Combination Bonus :<span class=\"value\"> " + bonus.Combo + "</span>"
        //					   + "<br />Lower case only penalty :<span class=\"value\"> " + bonus.FlatLower + "</span>"
        //					   + "<br />Numbers only penalty :<span class=\"value\"> " + bonus.FlatNumber + "</span>"
        //					   + "<br />Total Score:<span class=\"value\"> " + score  + "</span>" );
    }

}
); 