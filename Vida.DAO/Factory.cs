﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade;
using System.Xml;
using System.IO;

namespace ViverMais.DAO
{
    public class Factory : AbstractFactory
    {
        public new static T GetInstance<T>()
        {
            Type t = Type.GetType(Mapping[typeof(T).FullName]);
            return (T)Activator.CreateInstance(t);
        }

        public new static void Load(TextReader input)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;
            settings.IgnoreWhitespace = true;

            using (XmlReader reader = XmlReader.Create(input, settings))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        switch (reader.Name)
                        {
                            case "element":
                                Mapping.Add(reader.GetAttribute("interface"), reader.GetAttribute("class"));
                                break;
                        }
                    }
                }
            }
        }

        public new static void Load(string filepath)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;
            settings.IgnoreWhitespace = true;
            
            using (XmlReader reader = XmlReader.Create(filepath, settings))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        switch (reader.Name)
                        {
                            case "element":
                                Mapping.Add(reader.GetAttribute("interface"), reader.GetAttribute("class"));
                                break;
                        }
                    }
                }
            }
        }
    }
}
