using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace Vida.DAOOracle
{
    public static class HelperXmlSerializer
    {
        //Serializa o Obj em uma string XML.
        public static string ToXml(object Obj, System.Type ObjType)
        {
            XmlSerializer ser;
            ser = new XmlSerializer(ObjType);
            MemoryStream memStream;
            memStream = new MemoryStream();
            XmlTextWriter xmlWriter;
            xmlWriter = new XmlTextWriter(memStream, Encoding.UTF8);
            xmlWriter.Namespaces = true;
            ser.Serialize(xmlWriter, Obj);
            xmlWriter.Close();
            memStream.Close();
            string xml;
            xml = Encoding.UTF8.GetString(memStream.GetBuffer());
            xml = xml.Substring(xml.IndexOf(Convert.ToChar(60)));
            xml = xml.Substring(0, (xml.LastIndexOf(Convert.ToChar(62)) + 1));
            return xml;
        }
    }
}
