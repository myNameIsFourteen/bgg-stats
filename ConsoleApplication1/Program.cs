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

            writeAssortedStats(plays, "percents.csv");

            writeUnknownRaces();
        }

        private static void writeAssortedStats(List<eclipse.EclipsePlay> plays, String fileName)
        {
            Dictionary<int, int> playerCounts = new Dictionary<int, int>();
            Dictionary<int, Dictionary<eclipse.EclipseRace, int>> winCounts = new Dictionary<int, Dictionary<eclipse.EclipseRace, int>>();
            Dictionary<int, Dictionary<eclipse.EclipseRace, int>> participationCounts = new Dictionary<int, Dictionary<eclipse.EclipseRace, int>>();
            System.IO.StreamWriter writer = new System.IO.StreamWriter(fileName + ".txt");
            writer.WriteLine("Total Plays: " + plays.Count);
            plays = plays.FindAll(play => play.hasWinner);
            writer.WriteLine("Plays With Winner: " + plays.Count);
            plays = plays.FindAll(play => play.hasColors);
            writer.WriteLine("Plays With Parsed Races: " + plays.Count);

            for (int playerCount = 1; playerCount < 10; playerCount++)
            {
                playerCounts[playerCount] = 0;
                winCounts[playerCount] = new Dictionary<eclipse.EclipseRace, int>();
                participationCounts[playerCount] = new Dictionary<eclipse.EclipseRace, int>();
                foreach (eclipse.EclipseRace race in Enum.GetValues(typeof(eclipse.EclipseRace)))
                {
                    winCounts[playerCount][race] = 0;
                    participationCounts[playerCount][race] = 0;
                }
            }

            foreach (eclipse.EclipsePlay play in plays)
            {
                int playerCount = play.players.Count;
                playerCounts[playerCount] += 1;
                foreach (eclipse.EclipsePlayer player in play.players)
                {
                    participationCounts[playerCount][player.race] += 1;
                    if (player.win)
                    {
                        winCounts[playerCount][player.race] += 1;
                    }
                }
            }

            for (int playerCount = 1; playerCount < 10; playerCount++)
            {
                String header = "Statsfor " + playerCount + " player games";
                String participation = "Participates in: " + playerCounts[playerCount] + " games";
                String weightedWins = "%Wins out of " + playerCounts[playerCount] * playerCount +  " participations" ;
                String overallWins = "%Wins out of " + playerCounts[playerCount] + " games";
                foreach (eclipse.EclipseRace race in Enum.GetValues(typeof(eclipse.EclipseRace)))
                {
                    header += ", " + race;
                    participation += ", " + Math.Round(100 * participationCounts[playerCount][race] / (double)playerCounts[playerCount], 2);
                    weightedWins += ", " + Math.Round(100 * winCounts[playerCount][race] / (double)participationCounts[playerCount][race], 2);
                    overallWins += ", " + Math.Round(100 * winCounts[playerCount][race] / (double)playerCounts[playerCount], 2);
                }

                writer.WriteLine(header);
                writer.WriteLine(participation);
                writer.WriteLine(weightedWins);
                writer.WriteLine(overallWins);
            }
            writer.Close();
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
