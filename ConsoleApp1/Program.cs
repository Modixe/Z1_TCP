using System.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using Formatting = Newtonsoft.Json.Formatting;


namespace Consol
{
    class Program
    {
        XmlTextReader reader = new XmlTextReader("itu.xml");
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;

            NameEl("itu.xml");


            //NameE("itu.xml");



            Console.ReadLine();
        }
        public static void NameE(string filename)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);

            string json = JsonConvert.SerializeXmlNode(doc);
            Console.WriteLine(json);
        }


        public static void NameEl(string filename)
        {

            var doc = new XmlDocument();

            doc.Load(filename);

            //string val = JsonConvert.SerializeXmlNode(doc, Formatting.None);
            //Console.WriteLine (val);

            var root = doc.DocumentElement;

            PrintItem(root);
            Console.WriteLine(root);

        }
        private static void PrintItem(XmlElement item, int indent = 0)
        {
            //Console.WriteLine("Начало " + item.LocalName);
            //string x = item.LocalName;


            //if (item.LocalName == "Name")
            Console.Write($"{new string('\t', indent)}{item.LocalName + '"' + ':' } ");
            //else
            //    indent--;





            foreach (XmlAttribute attr in item.Attributes)
            {

                Console.Write($"[{attr.InnerText}]");
            }


            foreach (var child in item.ChildNodes)
            {
                if (child is XmlElement node)
                {

                    Console.WriteLine();
                    PrintItem(node, indent + 1);
                }

                if (child is XmlText text)
                {

                    Console.Write($"- {text.InnerText}");
                }
            }
            string val = JsonConvert.SerializeXmlNode(item, Formatting.None);


        }


    }
}
