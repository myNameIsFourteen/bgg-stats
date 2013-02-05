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
            List<eclipse.EclipsePlay> plays = eclipse.EclipsePlay.getAllPlayStats();

            writePlayerCountStats(plays);

            for (int i = 1; i < 10; i++)
            {
                writeNPlayerGameDetails(plays, i);
            }

            writeWinnerDetails(plays);

            writeUnknownRaces();
        }

        private static void writeUnknownRaces()
        {
            System.IO.StreamWriter writer = new System.IO.StreamWriter("UnknownRaces.txt");

            foreach (String str in eclipse.EclipseRaceParser.unknownRaces)
            {
                writer.WriteLine('"' + str + "\",");
            }

            writer.Close();
        }

        private static void writeNPlayerGameDetails(List<eclipse.EclipsePlay> plays, int pCount)
        {
            plays = plays.FindAll(play => play.hasWinner);
            plays = plays.FindAll(play => play.players.Count == pCount);
            plays = plays.FindAll(play => play.hasColors);
            System.IO.StreamWriter writer = new System.IO.StreamWriter(pCount + "playerPlays.csv");
            String outputLine = "GameID, playerRace, playerWin?";
            writer.WriteLine(outputLine);

            foreach (eclipse.EclipsePlay play in plays)
            {
                foreach (eclipse.EclipsePlayer player in play.players)
                {
                    outputLine = play.playID() + ", " + player.race + ", " + player.win;
                    writer.WriteLine(outputLine);
                }
            }
            writer.Close();
        }

        private static void writeWinnerDetails(List<eclipse.EclipsePlay> plays)
        {
            plays = plays.FindAll(play => play.hasWinner);
            plays = plays.FindAll(play => play.hasColors);
            System.IO.StreamWriter writer = new System.IO.StreamWriter("winners.csv");
            String outputLine = "GameID, playerCount, playerRace, playerWin?";
            writer.WriteLine(outputLine);

            foreach (eclipse.EclipsePlay play in plays)
            {
                foreach (eclipse.EclipsePlayer player in play.players)
                {
                    outputLine = play.playID() + ", " + play.players.Count + ", " + player.race + ", " + player.win;
                    writer.WriteLine(outputLine);
                }
            }
            writer.Close();
        }

        private static void writePlayerCountStats(List<eclipse.EclipsePlay> plays)
        {
            System.Console.WriteLine("Plays Found: " + plays.Count);
            plays = plays.FindAll(play => play.hasWinner);
            System.Console.WriteLine("Plays Found with a winner: " + plays.Count);
            for (int i = 0; i < 10; i++)
            {
                System.Console.WriteLine("Plays with a winner and exactly" + i + " Players: " + plays.FindAll(play => play.players.Count == i).Count);
            }
            System.Console.WriteLine("Plays with a winner and more than 9 Players: " + plays.FindAll(play => play.players.Count == 9).Count);
        }
    }
}
