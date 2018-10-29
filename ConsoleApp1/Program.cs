using System;
using System.IO;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace XML_JSON
{
    //Классы для десерилизации
    public partial class MyArray
    {
        [JsonProperty("Capability")]
        public Capability Capability { get; set; }
    }

    public partial class Capability
    {
        [JsonProperty("Layer")]
        public CapabilityLayer Layer { get; set; }
    }

    public partial class CapabilityLayer
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("Layer")]
        public List<LayerElement> Layer { get; set; }

        public CapabilityLayer()
        {
            Title = "";
        }
    }

    public partial class LayerElement
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("Attributes")]
        public Attributes Attributes { get; set; }
    }

    public partial class Attributes
    {
        [JsonProperty("Attribute")]
        public List<Attribute> Attribute { get; set; }
    }

    public partial class Attribute
    {
        [JsonProperty("@name")]
        public string Name { get; set; }

        [JsonProperty("@type")]
        public string Type { get; set; }
    }

    //Классы для серилизации
    public partial class MyArraySer
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("sublayers")]
        public List<LayerSer> sublayers { get; set; }

        public MyArraySer()
        {
            Title = "";
        }
    }

    public partial class LayerSer
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("attributes")]
        public List<AttributeSer> Attribute { get; set; }
    }

    public partial class AttributeSer
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {

            int count = 0;
            string xml = "";
            string json = "";
            string json_form = "";
            string name_list = "";
            string[] filename = new string[2];
            List<string> l_name_list = new List<string>();
            List<string> l_title_list = new List<string>();
            List<string> a_name_list1 = new List<string>();
            List<string> a_name_list2 = new List<string>();
            List<string> a_name_list3 = new List<string>();
            List<string> a_type_list1 = new List<string>();
            List<string> a_type_list2 = new List<string>();
            List<string> a_type_list3 = new List<string>();

            Console.Write("converter ");
            try
            {
                filename = Console.ReadLine().Split(' ');
                if (filename.Length == 1 || filename.Length == 1)
                {
                    Console.WriteLine("ERROR: Недостаточно входных/выходных данных");
                    Console.WriteLine(filename);
                }
                else if (filename.Length > 2)
                {
                    Console.WriteLine("ERROR: Слишком много входных/выходных данных или лишний пробел");
                }
                else
                {
                    char[] filename_char1 = filename[0].ToCharArray();
                    char[] filename_char2 = filename[1].ToCharArray();
                    int q1 = filename_char1.Length;
                    int q2 = filename_char2.Length;
                    if (filename_char1[q1 - 4] == '.' && filename_char1[q1 - 3] == 'x' && filename_char1[q1 - 2] == 'm' && filename_char1[q1 - 1] == 'l')
                    {
                        if (filename_char2[q2 - 5] == '.' && filename_char2[q2 - 4] == 'j' && filename_char2[q2 - 3] == 's' &&
                            filename_char2[q2 - 2] == 'o' && filename_char2[q2 - 1] == 'n')
                        {
                            //Конвертация из XML в JSON
                            xml = File.ReadAllText(filename[0]);
                            XmlDocument doc = new XmlDocument();
                            doc.LoadXml(xml);
                            json = JsonConvert.SerializeXmlNode(doc);

                            //Десериализация            
                            MyArray FromJson = JsonConvert.DeserializeObject<MyArray>(json);

                            name_list = FromJson.Capability.Layer.Name;
                            foreach (LayerElement l in FromJson.Capability.Layer.Layer)
                            {
                                foreach (Attribute a in l.Attributes.Attribute)
                                {
                                    if (count == 0)
                                    {
                                        a_name_list1.Add(a.Name);
                                        a_type_list1.Add(a.Type);
                                    }
                                    else if (count == 1)
                                    {
                                        a_name_list2.Add(a.Name);
                                        a_type_list2.Add(a.Type);
                                    }
                                    else if (count == 2)
                                    {
                                        a_name_list3.Add(a.Name);
                                        a_type_list3.Add(a.Type);
                                    }
                                }
                                l_name_list.Add(l.Name);
                                l_title_list.Add(l.Title);
                                count++;
                            }

                            //Сериализация 
                            MyArraySer arrSer = new MyArraySer();
                            AttributeSer attrSer = new AttributeSer();
                            arrSer = new MyArraySer
                            {
                                Name = name_list,
                                sublayers = new List<LayerSer>
                                {
                                    new LayerSer
                                    {
                                        Name = l_name_list[0],
                                        Title = l_title_list[0],
                                        Attribute = new List<AttributeSer>
                                        {
                                            new AttributeSer
                                            {
                                                Name = a_name_list1[0],
                                                Type = a_type_list1[0],
                                            }
                                        }
                                    },
                                    new LayerSer
                                    {
                                        Name = l_name_list[1],
                                        Title = l_title_list[1],
                                        Attribute = new List<AttributeSer>
                                        {
                                            new AttributeSer
                                            {
                                                Name = a_name_list2[0],
                                                Type = a_type_list2[0],
                                            }
                                        }
                                    },
                                    new LayerSer
                                    {
                                        Name = l_name_list[2],
                                        Title = l_title_list[2],
                                        Attribute = new List<AttributeSer>
                                        {
                                            new AttributeSer
                                            {
                                                Name = a_name_list3[0],
                                                Type = a_type_list3[0],
                                            }
                                        }
                                    }
                                }
                            };
                            for (int j = 1; j < a_name_list1.Count; j++)
                            {
                                attrSer.Name = a_name_list1[j];
                                attrSer.Type = a_type_list1[j];
                                arrSer.sublayers[0].Attribute.Add(attrSer);
                                attrSer = new AttributeSer();
                            }
                            for (int j = 1; j < a_name_list2.Count; j++)
                            {
                                attrSer.Name = a_name_list2[j];
                                attrSer.Type = a_type_list2[j];
                                arrSer.sublayers[1].Attribute.Add(attrSer);
                                attrSer = new AttributeSer();
                            }
                            for (int j = 1; j < a_name_list3.Count; j++)
                            {
                                attrSer.Name = a_name_list3[j];
                                attrSer.Type = a_type_list3[j];
                                arrSer.sublayers[2].Attribute.Add(attrSer);
                                attrSer = new AttributeSer();
                            }

                            json = JsonConvert.SerializeObject(arrSer);
                            json_form = JObject.Parse(json).ToString(Newtonsoft.Json.Formatting.Indented);
                            File.WriteAllText(filename[1], json_form);
                            Console.WriteLine("XML файл преобразован");
                        }
                        else
                        {
                            Console.WriteLine("ERROR: {0}", "Неверный формат выходного файла");
                        }
                    }
                    else
                    {
                        Console.WriteLine("ERROR: {0}", "Неверный формат входного файла");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: {0}", ex.Message);
            }
            Console.ReadKey();
        }
    }
}