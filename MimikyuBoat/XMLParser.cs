using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using System.Threading;
using System.Xml.Serialization;
using System.Drawing;

namespace MimikyuBoat
{
    class XMLParser
    {

        static XMLParser()
        {
            Debug.WriteLine("xmlparser statico cargado");
        }
        public void SET_VALUE_TO_KYU(string attributeName, object dataValue)
        {
            string kyuFilePath = BotSettings.KYU_FILE_PATH;
            if (!File.Exists(kyuFilePath))
            {
                Debug.WriteLine("Archivo no existe");
                return;
            }

            XDocument doc;
            if (File.ReadLines(kyuFilePath).Count() == 0)
            {
                // si no hay nada en el archivo creo el primer elemento e inicializo el xml
                doc = new XDocument(new XElement("settings"));
            } else
            {
                // sino supongo que hay data xml en el archivo e intento cargarla. esta funcion falla si 
                // hay data en el archivo pero esa data no es la esperada.
                doc = XDocument.Load(kyuFilePath);
            }

            XElement dataToAdd = (
                new XElement("setting", new XAttribute("name", attributeName), new XAttribute("type", dataValue.GetType().FullName),
                    new XElement("value", dataValue)
                ));

            // Verifico si lo que quiero agregar ya existe asi no duplico como un gil.
            foreach (XElement element in doc.Root.Descendants("setting"))
            {
                Debug.WriteLine(element.Attribute("name").Value);
                if (element.Attribute("name").Value == attributeName)
                {
                    // el elemento ya existe, procedo a reemplazarlo por el actual!
                    element.ReplaceWith(dataToAdd);
                    doc.Save(kyuFilePath);
                    return;
                }
            }

            // y si no existe lo creo.
            doc.Root.Add(dataToAdd);
            doc.Save(kyuFilePath);
        }

        static public object GET_VALUE_FROM_KYU(string dataName)
        {
            // TODO: cambiar esto por objetos deserializados en vez de hardcodear todo

            string kyuFilePath = BotSettings.KYU_FILE_PATH;
            if (!File.Exists(kyuFilePath))
            {
                Debug.WriteLine("Archivo no existe");
                return null;
            }

            XDocument doc;
            if (File.ReadLines(kyuFilePath).Count() == 0)
            {
                // si no hay nada en el archivo retorno null
                Debug.WriteLine("Archivo esta vacio");
                return null;
            }
            else
            {
                // sino supongo que hay data xml en el archivo e intento cargarla.
                doc = XDocument.Load(kyuFilePath);
            }


            foreach (XElement element in doc.Root.Descendants("setting"))
            {
                // busco el valor y lo retorno si existe, sino retorno null.
                if (element.Attribute("name").Value == dataName)
                {
                    string type = element.Attribute("type").Value;
                    string dataValue = element.Element("value").Value;

                    if (type == "System.Drawing.Rectangle")
                    {
                        Rectangle rec = new Rectangle();
                        string[] splittedValue = dataValue.Split('{', '}', '=', ',');
                        rec.X = int.Parse(splittedValue[2]);
                        rec.Y = int.Parse(splittedValue[4]);
                        rec.Width = int.Parse(splittedValue[6]);
                        rec.Height = int.Parse(splittedValue[8]);
                        return rec;
                    }
                    else if (type == "System.Int32")
                    {
                        return int.Parse(dataValue);
                    } 
                    else if (type == "System.Boolean")
                    {
                        return dataValue == "true" ? true : false;
                    } 
                    return dataValue;

                }
            }
            return null;
        }
    }
}
