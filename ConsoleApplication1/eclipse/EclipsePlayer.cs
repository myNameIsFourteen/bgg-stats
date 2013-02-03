using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eclipse
{
    class EclipsePlayer
    {
        

        private System.Xml.XmlNode playerNode;

        public EclipseRace race { get; set; }
        public String color { get; set; }
        public bool win { get; set; }
        public String score { get; set; }

        public EclipsePlayer(System.Xml.XmlNode playerNode)
        {
            this.playerNode = playerNode;
            color = playerNode.Attributes["color"].InnerText;
            win = int.Parse(playerNode.Attributes["win"].InnerText) > 0;
            score = playerNode.Attributes["score"].InnerText;
            race = EclipseRaceParser.categorizeByRace(color);
        }
    }


}
