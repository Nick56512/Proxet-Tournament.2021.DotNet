using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Proxet.Tournament
{
    public class TeamGenerator
    {
        private readonly int quantityPlayers=9;
        private readonly int[] vehicleTypes = new int[] { 1, 2, 3 };

        public (string[] team1, string[] team2) GenerateTeams(string filePath)
        {
            List<Player> players =new List<Player>(GetAllPlayers(filePath)
                .OrderByDescending(x=>x.WaitTime));

            string[] team1 = CreateTeam(players);
            string[] team2 = CreateTeam(players);

            return (team1,team2);
        }

        private string[] CreateTeam(IEnumerable<Player>players)
        {
            string[] team=new string[quantityPlayers];
            for (int i = 0; i < team.Length;) 
            {
                foreach (var type in vehicleTypes)
                {
                    try
                    {
                        Player player = players.FirstOrDefault(x => x.VehicleType == type&&!x.inGame);
                        team[i] = player.Name;
                        player.inGame = true;
                        ++i;
                    }
                    catch{}
                }
            }
            return team;
        }
        private IEnumerable<Player> GetAllPlayers(string filePath)
        {
            using (FileStream fileStream=new FileStream(filePath,FileMode.Open))
            {
                using (StreamReader streamReader=new StreamReader(fileStream))
                {
                    List<Player> players = new List<Player>();
                    while (!streamReader.EndOfStream)
                    {
                        try
                        {
                            string[] arr = streamReader
                                    .ReadLine()
                                    .Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                            Player player = new Player
                            {
                                Name = arr[0],
                                WaitTime = Convert.ToInt32(arr[1]),
                                VehicleType = Convert.ToInt32(arr[2])
                            };
                            players.Add(player);
                        }
                        catch { }
                    }
                    return players;
                }
            }
        }
    }
}