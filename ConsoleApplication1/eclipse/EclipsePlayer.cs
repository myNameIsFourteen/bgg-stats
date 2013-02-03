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
        public String raceStr { get; set; }
        public bool win { get; set; }
        public int score { get; set; }

        public EclipsePlayer(System.Xml.XmlNode playerNode)
        {
            this.playerNode = playerNode;
            raceStr = playerNode.Attributes["color"].InnerText;
            win = int.Parse(playerNode.Attributes["win"].InnerText) > 0;
        }
    }
}
