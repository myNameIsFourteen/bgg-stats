using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eclipse
{
    class EclipsePlay
    {
        public static string gameID = "125898";

        public List<EclipsePlayer> players { get; set; }
        public int duration { get; set; }

        public void parseFromXml(System.Xml.XmlNode node)
        {
            duration = 0;
        }
    }
}
