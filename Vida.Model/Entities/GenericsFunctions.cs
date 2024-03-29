﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace ViverMais.Model
{
    public static class GenericsFunctions
    {
        /// <summary>
        /// Remove caracteres especiais da palavra
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string RemoveDiacritics(string text)
        {
            string textnormalize = text.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            for (int ich = 0; ich < textnormalize.Length; ich++)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(textnormalize[ich]);
                if (uc != UnicodeCategory.NonSpacingMark)
                    sb.Append(textnormalize[ich]);
            }

            return (sb.ToString().Normalize(NormalizationForm.FormC));
        }

        /// <summary>
        /// Traduz para portugês o dia da semana
        /// </summary>
        /// <param name="dia"></param>
        /// <returns></returns>
        public static string DiaDaSemana(DayOfWeek dia)
        {
            CultureInfo cultureInfo = CultureInfo.CurrentCulture;

            return cultureInfo.TextInfo.ToTitleCase(cultureInfo.DateTimeFormat.GetDayName(dia));
        }
    }
}
