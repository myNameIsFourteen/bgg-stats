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
            BoardGameGeekAPI.BGGRequestPlays request;
            request = new BoardGameGeekAPI.BGGRequestPlays();
            request.ID = eclipse.EclipsePlay.gameID;
            //request.Username = "mynameisfourteen";
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

            System.Console.WriteLine("Plays Found: " + plays.Count);
            plays = plays.FindAll(play => play.hasWinner);
            System.Console.WriteLine("Plays Found with winner: " + plays.Count);
            for (int i = 0; i < 7; i++)
			{
                System.Console.WriteLine("Plays with winner and exactly" + i + " Players: " + plays.FindAll(play => play.players.Count == i).Count);
			}
        }
    }
}
