﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model.Entities
{
    public class RelatorioAttribute : System.Attribute
    {
        bool isCriteria;

        public bool IsCriteria
        {
            get { return isCriteria; }
            set { isCriteria = value; }
        }

        bool isVisible;

        public bool IsVisible
        {
            get { return isVisible; }
            set { isVisible = value; }
        }

        public RelatorioAttribute(bool isCriteria, bool isVisible)
        {
            this.IsCriteria = isCriteria;
            this.IsVisible = isVisible;
        }
    }
}
