﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3053
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#pragma warning disable 1591

namespace Vida.View.Agendamento.RelatoriosCrystal {
    
    
    /// <summary>
    ///Represents a strongly typed in-memory cache of data.
    ///</summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    [global::System.Serializable()]
    [global::System.ComponentModel.DesignerCategoryAttribute("code")]
    [global::System.ComponentModel.ToolboxItem(true)]
    [global::System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedDataSetSchema")]
    [global::System.Xml.Serialization.XmlRootAttribute("DSCabecalhoAgendaPrestador")]
    [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.DataSet")]
    public partial class DSCabecalhoAgendaPrestador : global::System.Data.DataSet {
        
        private CabecalhoAgendaPrestadorDataTable tableCabecalhoAgendaPrestador;
        
        private global::System.Data.SchemaSerializationMode _schemaSerializationMode = global::System.Data.SchemaSerializationMode.IncludeSchema;
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public DSCabecalhoAgendaPrestador() {
            this.BeginInit();
            this.InitClass();
            global::System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new global::System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += schemaChangedHandler;
            base.Relations.CollectionChanged += schemaChangedHandler;
            this.EndInit();
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected DSCabecalhoAgendaPrestador(global::System.Runtime.Serialization.SerializationInfo info, global::System.Runtime.Serialization.StreamingContext context) : 
                base(info, context, false) {
            if ((this.IsBinarySerialized(info, context) == true)) {
                this.InitVars(false);
                global::System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler1 = new global::System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
                this.Tables.CollectionChanged += schemaChangedHandler1;
                this.Relations.CollectionChanged += schemaChangedHandler1;
                return;
            }
            string strSchema = ((string)(info.GetValue("XmlSchema", typeof(string))));
            if ((this.DetermineSchemaSerializationMode(info, context) == global::System.Data.SchemaSerializationMode.IncludeSchema)) {
                global::System.Data.DataSet ds = new global::System.Data.DataSet();
                ds.ReadXmlSchema(new global::System.Xml.XmlTextReader(new global::System.IO.StringReader(strSchema)));
                if ((ds.Tables["CabecalhoAgendaPrestador"] != null)) {
                    base.Tables.Add(new CabecalhoAgendaPrestadorDataTable(ds.Tables["CabecalhoAgendaPrestador"]));
                }
                this.DataSetName = ds.DataSetName;
                this.Prefix = ds.Prefix;
                this.Namespace = ds.Namespace;
                this.Locale = ds.Locale;
                this.CaseSensitive = ds.CaseSensitive;
                this.EnforceConstraints = ds.EnforceConstraints;
                this.Merge(ds, false, global::System.Data.MissingSchemaAction.Add);
                this.InitVars();
            }
            else {
                this.ReadXmlSchema(new global::System.Xml.XmlTextReader(new global::System.IO.StringReader(strSchema)));
            }
            this.GetSerializationData(info, context);
            global::System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new global::System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += schemaChangedHandler;
            this.Relations.CollectionChanged += schemaChangedHandler;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.Browsable(false)]
        [global::System.ComponentModel.DesignerSerializationVisibility(global::System.ComponentModel.DesignerSerializationVisibility.Content)]
        public CabecalhoAgendaPrestadorDataTable CabecalhoAgendaPrestador {
            get {
                return this.tableCabecalhoAgendaPrestador;
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.BrowsableAttribute(true)]
        [global::System.ComponentModel.DesignerSerializationVisibilityAttribute(global::System.ComponentModel.DesignerSerializationVisibility.Visible)]
        public override global::System.Data.SchemaSerializationMode SchemaSerializationMode {
            get {
                return this._schemaSerializationMode;
            }
            set {
                this._schemaSerializationMode = value;
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.DesignerSerializationVisibilityAttribute(global::System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public new global::System.Data.DataTableCollection Tables {
            get {
                return base.Tables;
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.DesignerSerializationVisibilityAttribute(global::System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public new global::System.Data.DataRelationCollection Relations {
            get {
                return base.Relations;
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override void InitializeDerivedDataSet() {
            this.BeginInit();
            this.InitClass();
            this.EndInit();
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public override global::System.Data.DataSet Clone() {
            DSCabecalhoAgendaPrestador cln = ((DSCabecalhoAgendaPrestador)(base.Clone()));
            cln.InitVars();
            cln.SchemaSerializationMode = this.SchemaSerializationMode;
            return cln;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override bool ShouldSerializeTables() {
            return false;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override bool ShouldSerializeRelations() {
            return false;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override void ReadXmlSerializable(global::System.Xml.XmlReader reader) {
            if ((this.DetermineSchemaSerializationMode(reader) == global::System.Data.SchemaSerializationMode.IncludeSchema)) {
                this.Reset();
                global::System.Data.DataSet ds = new global::System.Data.DataSet();
                ds.ReadXml(reader);
                if ((ds.Tables["CabecalhoAgendaPrestador"] != null)) {
                    base.Tables.Add(new CabecalhoAgendaPrestadorDataTable(ds.Tables["CabecalhoAgendaPrestador"]));
                }
                this.DataSetName = ds.DataSetName;
                this.Prefix = ds.Prefix;
                this.Namespace = ds.Namespace;
                this.Locale = ds.Locale;
                this.CaseSensitive = ds.CaseSensitive;
                this.EnforceConstraints = ds.EnforceConstraints;
                this.Merge(ds, false, global::System.Data.MissingSchemaAction.Add);
                this.InitVars();
            }
            else {
                this.ReadXml(reader);
                this.InitVars();
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override global::System.Xml.Schema.XmlSchema GetSchemaSerializable() {
            global::System.IO.MemoryStream stream = new global::System.IO.MemoryStream();
            this.WriteXmlSchema(new global::System.Xml.XmlTextWriter(stream, null));
            stream.Position = 0;
            return global::System.Xml.Schema.XmlSchema.Read(new global::System.Xml.XmlTextReader(stream), null);
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        internal void InitVars() {
            this.InitVars(true);
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        internal void InitVars(bool initTable) {
            this.tableCabecalhoAgendaPrestador = ((CabecalhoAgendaPrestadorDataTable)(base.Tables["CabecalhoAgendaPrestador"]));
            if ((initTable == true)) {
                if ((this.tableCabecalhoAgendaPrestador != null)) {
                    this.tableCabecalhoAgendaPrestador.InitVars();
                }
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void InitClass() {
            this.DataSetName = "DSCabecalhoAgendaPrestador";
            this.Prefix = "";
            this.Namespace = "http://tempuri.org/DSCabecalhoAgendaPrestador.xsd";
            this.EnforceConstraints = true;
            this.SchemaSerializationMode = global::System.Data.SchemaSerializationMode.IncludeSchema;
            this.tableCabecalhoAgendaPrestador = new CabecalhoAgendaPrestadorDataTable();
            base.Tables.Add(this.tableCabecalhoAgendaPrestador);
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private bool ShouldSerializeCabecalhoAgendaPrestador() {
            return false;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void SchemaChanged(object sender, global::System.ComponentModel.CollectionChangeEventArgs e) {
            if ((e.Action == global::System.ComponentModel.CollectionChangeAction.Remove)) {
                this.InitVars();
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static global::System.Xml.Schema.XmlSchemaComplexType GetTypedDataSetSchema(global::System.Xml.Schema.XmlSchemaSet xs) {
            DSCabecalhoAgendaPrestador ds = new DSCabecalhoAgendaPrestador();
            global::System.Xml.Schema.XmlSchemaComplexType type = new global::System.Xml.Schema.XmlSchemaComplexType();
            global::System.Xml.Schema.XmlSchemaSequence sequence = new global::System.Xml.Schema.XmlSchemaSequence();
            global::System.Xml.Schema.XmlSchemaAny any = new global::System.Xml.Schema.XmlSchemaAny();
            any.Namespace = ds.Namespace;
            sequence.Items.Add(any);
            type.Particle = sequence;
            global::System.Xml.Schema.XmlSchema dsSchema = ds.GetSchemaSerializable();
            if (xs.Contains(dsSchema.TargetNamespace)) {
                global::System.IO.MemoryStream s1 = new global::System.IO.MemoryStream();
                global::System.IO.MemoryStream s2 = new global::System.IO.MemoryStream();
                try {
                    global::System.Xml.Schema.XmlSchema schema = null;
                    dsSchema.Write(s1);
                    for (global::System.Collections.IEnumerator schemas = xs.Schemas(dsSchema.TargetNamespace).GetEnumerator(); schemas.MoveNext(); ) {
                        schema = ((global::System.Xml.Schema.XmlSchema)(schemas.Current));
                        s2.SetLength(0);
                        schema.Write(s2);
                        if ((s1.Length == s2.Length)) {
                            s1.Position = 0;
                            s2.Position = 0;
                            for (; ((s1.Position != s1.Length) 
                                        && (s1.ReadByte() == s2.ReadByte())); ) {
                                ;
                            }
                            if ((s1.Position == s1.Length)) {
                                return type;
                            }
                        }
                    }
                }
                finally {
                    if ((s1 != null)) {
                        s1.Close();
                    }
                    if ((s2 != null)) {
                        s2.Close();
                    }
                }
            }
            xs.Add(dsSchema);
            return type;
        }
        
        public delegate void CabecalhoAgendaPrestadorRowChangeEventHandler(object sender, CabecalhoAgendaPrestadorRowChangeEvent e);
        
        /// <summary>
        ///Represents the strongly named DataTable class.
        ///</summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        [global::System.Serializable()]
        [global::System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedTableSchema")]
        public partial class CabecalhoAgendaPrestadorDataTable : global::System.Data.TypedTableBase<CabecalhoAgendaPrestadorRow> {
            
            private global::System.Data.DataColumn columnUnidadeExecutante;
            
            private global::System.Data.DataColumn columnPeriodo;
            
            private global::System.Data.DataColumn columnProcedimento;
            
            private global::System.Data.DataColumn columnQtdRegistros;
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public CabecalhoAgendaPrestadorDataTable() {
                this.TableName = "CabecalhoAgendaPrestador";
                this.BeginInit();
                this.InitClass();
                this.EndInit();
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            internal CabecalhoAgendaPrestadorDataTable(global::System.Data.DataTable table) {
                this.TableName = table.TableName;
                if ((table.CaseSensitive != table.DataSet.CaseSensitive)) {
                    this.CaseSensitive = table.CaseSensitive;
                }
                if ((table.Locale.ToString() != table.DataSet.Locale.ToString())) {
                    this.Locale = table.Locale;
                }
                if ((table.Namespace != table.DataSet.Namespace)) {
                    this.Namespace = table.Namespace;
                }
                this.Prefix = table.Prefix;
                this.MinimumCapacity = table.MinimumCapacity;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected CabecalhoAgendaPrestadorDataTable(global::System.Runtime.Serialization.SerializationInfo info, global::System.Runtime.Serialization.StreamingContext context) : 
                    base(info, context) {
                this.InitVars();
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataColumn UnidadeExecutanteColumn {
                get {
                    return this.columnUnidadeExecutante;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataColumn PeriodoColumn {
                get {
                    return this.columnPeriodo;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataColumn ProcedimentoColumn {
                get {
                    return this.columnProcedimento;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataColumn QtdRegistrosColumn {
                get {
                    return this.columnQtdRegistros;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.ComponentModel.Browsable(false)]
            public int Count {
                get {
                    return this.Rows.Count;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public CabecalhoAgendaPrestadorRow this[int index] {
                get {
                    return ((CabecalhoAgendaPrestadorRow)(this.Rows[index]));
                }
            }
            
            public event CabecalhoAgendaPrestadorRowChangeEventHandler CabecalhoAgendaPrestadorRowChanging;
            
            public event CabecalhoAgendaPrestadorRowChangeEventHandler CabecalhoAgendaPrestadorRowChanged;
            
            public event CabecalhoAgendaPrestadorRowChangeEventHandler CabecalhoAgendaPrestadorRowDeleting;
            
            public event CabecalhoAgendaPrestadorRowChangeEventHandler CabecalhoAgendaPrestadorRowDeleted;
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void AddCabecalhoAgendaPrestadorRow(CabecalhoAgendaPrestadorRow row) {
                this.Rows.Add(row);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public CabecalhoAgendaPrestadorRow AddCabecalhoAgendaPrestadorRow(string UnidadeExecutante, string Periodo, string Procedimento, string QtdRegistros) {
                CabecalhoAgendaPrestadorRow rowCabecalhoAgendaPrestadorRow = ((CabecalhoAgendaPrestadorRow)(this.NewRow()));
                object[] columnValuesArray = new object[] {
                        UnidadeExecutante,
                        Periodo,
                        Procedimento,
                        QtdRegistros};
                rowCabecalhoAgendaPrestadorRow.ItemArray = columnValuesArray;
                this.Rows.Add(rowCabecalhoAgendaPrestadorRow);
                return rowCabecalhoAgendaPrestadorRow;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public override global::System.Data.DataTable Clone() {
                CabecalhoAgendaPrestadorDataTable cln = ((CabecalhoAgendaPrestadorDataTable)(base.Clone()));
                cln.InitVars();
                return cln;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override global::System.Data.DataTable CreateInstance() {
                return new CabecalhoAgendaPrestadorDataTable();
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            internal void InitVars() {
                this.columnUnidadeExecutante = base.Columns["UnidadeExecutante"];
                this.columnPeriodo = base.Columns["Periodo"];
                this.columnProcedimento = base.Columns["Procedimento"];
                this.columnQtdRegistros = base.Columns["QtdRegistros"];
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            private void InitClass() {
                this.columnUnidadeExecutante = new global::System.Data.DataColumn("UnidadeExecutante", typeof(string), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnUnidadeExecutante);
                this.columnPeriodo = new global::System.Data.DataColumn("Periodo", typeof(string), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnPeriodo);
                this.columnProcedimento = new global::System.Data.DataColumn("Procedimento", typeof(string), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnProcedimento);
                this.columnQtdRegistros = new global::System.Data.DataColumn("QtdRegistros", typeof(string), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnQtdRegistros);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public CabecalhoAgendaPrestadorRow NewCabecalhoAgendaPrestadorRow() {
                return ((CabecalhoAgendaPrestadorRow)(this.NewRow()));
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override global::System.Data.DataRow NewRowFromBuilder(global::System.Data.DataRowBuilder builder) {
                return new CabecalhoAgendaPrestadorRow(builder);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override global::System.Type GetRowType() {
                return typeof(CabecalhoAgendaPrestadorRow);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowChanged(global::System.Data.DataRowChangeEventArgs e) {
                base.OnRowChanged(e);
                if ((this.CabecalhoAgendaPrestadorRowChanged != null)) {
                    this.CabecalhoAgendaPrestadorRowChanged(this, new CabecalhoAgendaPrestadorRowChangeEvent(((CabecalhoAgendaPrestadorRow)(e.Row)), e.Action));
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowChanging(global::System.Data.DataRowChangeEventArgs e) {
                base.OnRowChanging(e);
                if ((this.CabecalhoAgendaPrestadorRowChanging != null)) {
                    this.CabecalhoAgendaPrestadorRowChanging(this, new CabecalhoAgendaPrestadorRowChangeEvent(((CabecalhoAgendaPrestadorRow)(e.Row)), e.Action));
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowDeleted(global::System.Data.DataRowChangeEventArgs e) {
                base.OnRowDeleted(e);
                if ((this.CabecalhoAgendaPrestadorRowDeleted != null)) {
                    this.CabecalhoAgendaPrestadorRowDeleted(this, new CabecalhoAgendaPrestadorRowChangeEvent(((CabecalhoAgendaPrestadorRow)(e.Row)), e.Action));
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowDeleting(global::System.Data.DataRowChangeEventArgs e) {
                base.OnRowDeleting(e);
                if ((this.CabecalhoAgendaPrestadorRowDeleting != null)) {
                    this.CabecalhoAgendaPrestadorRowDeleting(this, new CabecalhoAgendaPrestadorRowChangeEvent(((CabecalhoAgendaPrestadorRow)(e.Row)), e.Action));
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void RemoveCabecalhoAgendaPrestadorRow(CabecalhoAgendaPrestadorRow row) {
                this.Rows.Remove(row);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public static global::System.Xml.Schema.XmlSchemaComplexType GetTypedTableSchema(global::System.Xml.Schema.XmlSchemaSet xs) {
                global::System.Xml.Schema.XmlSchemaComplexType type = new global::System.Xml.Schema.XmlSchemaComplexType();
                global::System.Xml.Schema.XmlSchemaSequence sequence = new global::System.Xml.Schema.XmlSchemaSequence();
                DSCabecalhoAgendaPrestador ds = new DSCabecalhoAgendaPrestador();
                global::System.Xml.Schema.XmlSchemaAny any1 = new global::System.Xml.Schema.XmlSchemaAny();
                any1.Namespace = "http://www.w3.org/2001/XMLSchema";
                any1.MinOccurs = new decimal(0);
                any1.MaxOccurs = decimal.MaxValue;
                any1.ProcessContents = global::System.Xml.Schema.XmlSchemaContentProcessing.Lax;
                sequence.Items.Add(any1);
                global::System.Xml.Schema.XmlSchemaAny any2 = new global::System.Xml.Schema.XmlSchemaAny();
                any2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
                any2.MinOccurs = new decimal(1);
                any2.ProcessContents = global::System.Xml.Schema.XmlSchemaContentProcessing.Lax;
                sequence.Items.Add(any2);
                global::System.Xml.Schema.XmlSchemaAttribute attribute1 = new global::System.Xml.Schema.XmlSchemaAttribute();
                attribute1.Name = "namespace";
                attribute1.FixedValue = ds.Namespace;
                type.Attributes.Add(attribute1);
                global::System.Xml.Schema.XmlSchemaAttribute attribute2 = new global::System.Xml.Schema.XmlSchemaAttribute();
                attribute2.Name = "tableTypeName";
                attribute2.FixedValue = "CabecalhoAgendaPrestadorDataTable";
                type.Attributes.Add(attribute2);
                type.Particle = sequence;
                global::System.Xml.Schema.XmlSchema dsSchema = ds.GetSchemaSerializable();
                if (xs.Contains(dsSchema.TargetNamespace)) {
                    global::System.IO.MemoryStream s1 = new global::System.IO.MemoryStream();
                    global::System.IO.MemoryStream s2 = new global::System.IO.MemoryStream();
                    try {
                        global::System.Xml.Schema.XmlSchema schema = null;
                        dsSchema.Write(s1);
                        for (global::System.Collections.IEnumerator schemas = xs.Schemas(dsSchema.TargetNamespace).GetEnumerator(); schemas.MoveNext(); ) {
                            schema = ((global::System.Xml.Schema.XmlSchema)(schemas.Current));
                            s2.SetLength(0);
                            schema.Write(s2);
                            if ((s1.Length == s2.Length)) {
                                s1.Position = 0;
                                s2.Position = 0;
                                for (; ((s1.Position != s1.Length) 
                                            && (s1.ReadByte() == s2.ReadByte())); ) {
                                    ;
                                }
                                if ((s1.Position == s1.Length)) {
                                    return type;
                                }
                            }
                        }
                    }
                    finally {
                        if ((s1 != null)) {
                            s1.Close();
                        }
                        if ((s2 != null)) {
                            s2.Close();
                        }
                    }
                }
                xs.Add(dsSchema);
                return type;
            }
        }
        
        /// <summary>
        ///Represents strongly named DataRow class.
        ///</summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        public partial class CabecalhoAgendaPrestadorRow : global::System.Data.DataRow {
            
            private CabecalhoAgendaPrestadorDataTable tableCabecalhoAgendaPrestador;
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            internal CabecalhoAgendaPrestadorRow(global::System.Data.DataRowBuilder rb) : 
                    base(rb) {
                this.tableCabecalhoAgendaPrestador = ((CabecalhoAgendaPrestadorDataTable)(this.Table));
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string UnidadeExecutante {
                get {
                    try {
                        return ((string)(this[this.tableCabecalhoAgendaPrestador.UnidadeExecutanteColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'UnidadeExecutante\' in table \'CabecalhoAgendaPrestador\' is D" +
                                "BNull.", e);
                    }
                }
                set {
                    this[this.tableCabecalhoAgendaPrestador.UnidadeExecutanteColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string Periodo {
                get {
                    try {
                        return ((string)(this[this.tableCabecalhoAgendaPrestador.PeriodoColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'Periodo\' in table \'CabecalhoAgendaPrestador\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableCabecalhoAgendaPrestador.PeriodoColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string Procedimento {
                get {
                    try {
                        return ((string)(this[this.tableCabecalhoAgendaPrestador.ProcedimentoColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'Procedimento\' in table \'CabecalhoAgendaPrestador\' is DBNull" +
                                ".", e);
                    }
                }
                set {
                    this[this.tableCabecalhoAgendaPrestador.ProcedimentoColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string QtdRegistros {
                get {
                    try {
                        return ((string)(this[this.tableCabecalhoAgendaPrestador.QtdRegistrosColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'QtdRegistros\' in table \'CabecalhoAgendaPrestador\' is DBNull" +
                                ".", e);
                    }
                }
                set {
                    this[this.tableCabecalhoAgendaPrestador.QtdRegistrosColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsUnidadeExecutanteNull() {
                return this.IsNull(this.tableCabecalhoAgendaPrestador.UnidadeExecutanteColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetUnidadeExecutanteNull() {
                this[this.tableCabecalhoAgendaPrestador.UnidadeExecutanteColumn] = global::System.Convert.DBNull;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsPeriodoNull() {
                return this.IsNull(this.tableCabecalhoAgendaPrestador.PeriodoColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetPeriodoNull() {
                this[this.tableCabecalhoAgendaPrestador.PeriodoColumn] = global::System.Convert.DBNull;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsProcedimentoNull() {
                return this.IsNull(this.tableCabecalhoAgendaPrestador.ProcedimentoColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetProcedimentoNull() {
                this[this.tableCabecalhoAgendaPrestador.ProcedimentoColumn] = global::System.Convert.DBNull;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsQtdRegistrosNull() {
                return this.IsNull(this.tableCabecalhoAgendaPrestador.QtdRegistrosColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetQtdRegistrosNull() {
                this[this.tableCabecalhoAgendaPrestador.QtdRegistrosColumn] = global::System.Convert.DBNull;
            }
        }
        
        /// <summary>
        ///Row event argument class
        ///</summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        public class CabecalhoAgendaPrestadorRowChangeEvent : global::System.EventArgs {
            
            private CabecalhoAgendaPrestadorRow eventRow;
            
            private global::System.Data.DataRowAction eventAction;
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public CabecalhoAgendaPrestadorRowChangeEvent(CabecalhoAgendaPrestadorRow row, global::System.Data.DataRowAction action) {
                this.eventRow = row;
                this.eventAction = action;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public CabecalhoAgendaPrestadorRow Row {
                get {
                    return this.eventRow;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataRowAction Action {
                get {
                    return this.eventAction;
                }
            }
        }
    }
}

#pragma warning restore 1591