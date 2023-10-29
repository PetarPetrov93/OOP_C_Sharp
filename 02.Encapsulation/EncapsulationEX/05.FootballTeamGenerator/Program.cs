namespace _05.FootballTeamGenerator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Team> teams = new List<Team>();
            string cmd;

            while ((cmd = Console.ReadLine()) != "END")
            {
                
                string[] stats = cmd.Split(";", StringSplitOptions.RemoveEmptyEntries);
                Team team = null;

                if (stats[0] == "Team")
                {
                    try
                    {
                        team = new Team(stats[1]);
                        teams.Add(team);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else if (stats[0] == "Add")
                {
                    if (!teams.Any(t => t.Name == stats[1]))
                    {
                        Console.WriteLine($"Team {stats[1]} does not exist.");
                        continue;
                    }
                    Player player = null;
                    try
                    {
                        player = new Player(stats[2], int.Parse(stats[3]), int.Parse(stats[4]), int.Parse(stats[5]), int.Parse(stats[6]), int.Parse(stats[7]));
                        teams.First(t => t.Name == stats[1]).AddPlayer(player);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else if (stats[0] == "Remove")
                {
                    try
                    {
                        teams.First(t => t.Name == stats[1]).RemovePlayer(stats[2]);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else if (stats[0] == "Rating")
                {
                    if (!teams.Any(t => t.Name == stats[1]))
                    {
                        Console.WriteLine($"Team {stats[1]} does not exist.");
                        continue;
                    }
                    int averageRating = teams.First(t => t.Name == stats[1]).Rating;
                    Console.WriteLine($"{stats[1]} - {averageRating}");
                }
            }


        }
    }
}