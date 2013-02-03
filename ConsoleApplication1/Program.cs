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

            write4PlayerGameDetails(plays);
        }

        private static void write4PlayerGameDetails(List<eclipse.EclipsePlay> plays)
        {
            int pCount = 4;
            plays = plays.FindAll(play => play.players.Count == pCount);
            System.IO.StreamWriter writer = new System.IO.StreamWriter("4playerPlays.csv");
            String outputLine = "GameID";
            for (int i = 0; i < 4; i++)
            {
                outputLine = outputLine + ", playerRace, playerScore , playerWin?";
            }
            writer.WriteLine(outputLine);

            foreach (eclipse.EclipsePlay play in plays)
            {
                outputLine = play.playID();
                foreach (eclipse.EclipsePlayer player in play.players)
                {
                    outputLine += ", " + player.raceStr + ", " + player.score + ", " + player.win;
                }
                writer.WriteLine(outputLine);
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
