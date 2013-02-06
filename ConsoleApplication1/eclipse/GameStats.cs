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
        private int _winnerCount = 0;
        private bool _hasColorData = true;
        private bool _isAllTerran = true;
        private bool _isExpansion = false;


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
                    _winnerCount++;
                }
                if (player.race == eclipse.EclipseRace.Unknown)
                {
                    _hasColorData = false;
                }
                if (player.race != eclipse.EclipseRace.Terran)
                {
                    _isAllTerran = false;
                }
                switch (player.race)
                {
                    case EclipseRace.Magellan:
                    case EclipseRace.Exiles:
                    case EclipseRace.Syndicate:
                    case EclipseRace.Enlightened:
                        _isExpansion = true;
                        break;
                    default:
                        break;
                }
            }
            if (!hasWinner)
            {
                _hasColorData = false;
            }
        }

        public List<EclipsePlayer> players { get; set; }
        public int duration { get; set; }
        public int winnerCount { get { return _winnerCount; } }
        public bool hasWinner { get { return _winnerCount > 0; } }
        public bool hasOneWinner { get { return _winnerCount == 1; }}
        public bool hasColors { get { return _hasColorData; } }
        public bool isExpansion { get { return _isExpansion; } }
        public bool isAllTerran { get { return _isAllTerran; } }

        public static List<EclipsePlay> getAllPlayStats()
        {
            BoardGameGeekAPI.BGGConnection connection = new BoardGameGeekAPI.BGGConnection();
            List<eclipse.EclipsePlay> plays = new List<eclipse.EclipsePlay>();

            foreach (String gameID in new String[] {EclipsePlay.gameID, EclipsePlay.expansionID})
            {
                BoardGameGeekAPI.BGGRequestPlays request = new BoardGameGeekAPI.BGGRequestPlays();
                request.ID = gameID;
                int totalGames = 100;
                int pageNeeded = 1;

                while (request.Page <= totalGames / 100)
                {
                    System.Xml.XmlDocument doc = connection.GetResponse(request);
                    totalGames = int.Parse(doc.SelectSingleNode("plays").Attributes["total"].InnerText);

                    foreach (XmlNode node in doc.SelectNodes("plays/play"))
                    {
                        eclipse.EclipsePlay play = new eclipse.EclipsePlay(node);
                        if (gameID == EclipsePlay.expansionID)
                        {
                            play._isExpansion = true;
                        }
                        plays.Add(play);
                    }

                    request = new BoardGameGeekAPI.BGGRequestPlays();
                    request.ID = gameID;
                    request.Page = ++pageNeeded;
                }
            }
            return plays;
        }

        internal string playID()
        {
            return node.Attributes["id"].InnerText;
        }
    }
}
