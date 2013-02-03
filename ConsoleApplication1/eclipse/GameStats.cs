using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace eclipse
{
    class EclipsePlay
    {
        public static string gameID = "72125";
        public static string expansionID = "125898";
        private System.Xml.XmlNode node;
        private bool _hasWinner = false;
        private bool _hasColorData = false;


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
                if (!player.color.Equals(""))
                {
                    _hasColorData = true;
                }
            }
        }

        public List<EclipsePlayer> players { get; set; }
        public int duration { get; set; }
        public bool hasWinner { get { return _hasWinner; } }
        public bool hasColors { get { return _hasColorData; } }

        public static List<EclipsePlay> getAllPlayStats()
        {
            BoardGameGeekAPI.BGGConnection connection = new BoardGameGeekAPI.BGGConnection();
            BoardGameGeekAPI.BGGRequestPlays request = new BoardGameGeekAPI.BGGRequestPlays();
            request.ID = eclipse.EclipsePlay.gameID;
            int totalGames = 100;
            int pageNeeded = 1;

            List<eclipse.EclipsePlay> plays = new List<eclipse.EclipsePlay>();

            while (request.Page <= totalGames / 100)
            {
                System.Xml.XmlDocument doc = connection.GetResponse(request);
                totalGames = int.Parse(doc.SelectSingleNode("plays").Attributes["total"].InnerText);

                foreach (XmlNode node in doc.SelectNodes("plays/play"))
                {
                    eclipse.EclipsePlay play = new eclipse.EclipsePlay(node);
                    plays.Add(play);
                }

                request = new BoardGameGeekAPI.BGGRequestPlays();
                request.ID = eclipse.EclipsePlay.gameID;
                request.Page = ++pageNeeded;
            }

            return plays;
        }

        internal string playID()
        {
            return node.Attributes["id"].InnerText;
        }
    }
}
