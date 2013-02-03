using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eclipse
{
    class EclipsePlay
    {
        public static string gameID = "72125";
        public static string expansionID = "125898";
        private System.Xml.XmlNode node;
        private bool _hasWinner = false;


        public EclipsePlay(System.Xml.XmlNode node)
        {
            this.node = node; 
            duration = int.Parse(node.Attributes["length"].InnerText);
            players = new List<EclipsePlayer>();

            //System.Console.WriteLine(node.Attributes["date"].InnerText);
            foreach (System.Xml.XmlNode playerNode in node.SelectNodes("players/player"))
            {
                EclipsePlayer player = new EclipsePlayer(playerNode);
                players.Add(player);
                if (player.win)
                {
                    _hasWinner = true;
                }
            }

            foreach (EclipsePlayer player in players)
            {
                if (player.score > 0) {
                    //_hasWinner = true;
                }
            }
        }

        public List<EclipsePlayer> players { get; set; }
        public int duration { get; set; }
        public bool hasWinner { get { return _hasWinner; } }
    }
}
