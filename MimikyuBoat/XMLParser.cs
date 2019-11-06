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
using System.Xml;

namespace MimikyuBoat
{
    class XMLParser
    {

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
            }
            else
            {
                // sino supongo que hay data xml en el archivo e intento cargarla. esta funcion falla si 
                // hay data en el archivo pero esa data no es la esperada.
                doc = XDocument.Load(kyuFilePath);
            }

            StringWriter sw = new StringWriter();
            XmlWriter xmlWriter = XmlWriter.Create(sw);
            XmlSerializer xmlSerializer = new XmlSerializer(dataValue.GetType());
            xmlSerializer.Serialize(xmlWriter, dataValue);
            XDocument serializedDoc = XDocument.Parse(sw.ToString()); // convierto el string xml en doc

            XElement dataToAdd = (
                new XElement("setting", new XAttribute("name", attributeName), new XAttribute("type", dataValue.GetType().FullName),
                    serializedDoc.Root // obtengo el primer elemento, es decir, el objeto serializado.
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
            xmlWriter.Dispose();
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

            XmlSerializer xmlSerializer;
            foreach (XElement element in doc.Root.Descendants("setting"))
            {
                if (element.Attribute("name").Value == dataName)
                {
                    string type = element.Attribute("type").Value;
                    Type objectType = GetType(type);
                    xmlSerializer = new XmlSerializer(objectType);
                    XNode child = element.FirstNode; // obtengo el nodo del elemento a deserializar
                    byte[] childXML = Encoding.UTF8.GetBytes(child.ToString());

                    MemoryStream memStream = new MemoryStream(childXML);
                    object deserializedObject = xmlSerializer.Deserialize(memStream);

                    return deserializedObject;
                }

            }
            return null;
        }

        static Type GetType(string typeName)
        {
            Type type = Type.GetType(typeName);
            if (type != null) return type;

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = assembly.GetType(typeName);
                if (type != null) return type;
            }
            return null;
        }
    }
}
