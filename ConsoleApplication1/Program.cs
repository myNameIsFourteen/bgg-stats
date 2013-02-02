using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoardGameGeekAPI;
using System.Xml;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            BoardGameGeekAPI.BGGConnection connection = new BoardGameGeekAPI.BGGConnection();
            BoardGameGeekAPI.BGGRequestCollection request;
            request = new BoardGameGeekAPI.BGGRequestCollection();
            request.Username = "mynameisfourteen";

            System.Xml.XmlDocument doc = connection.GetResponse(request);

            foreach (XmlNode node in doc.SelectNodes("items/item/name"))
            {
                System.Console.WriteLine(node.InnerText);
            }
            //node.Attributes.



            //XmlWriter writer = XmlWriter.Create(System.Console.Out);


            //doc.WriteContentTo(writer);

            //System.Console.WriteLine(request.GetRequestPage());
        }
    }
}
