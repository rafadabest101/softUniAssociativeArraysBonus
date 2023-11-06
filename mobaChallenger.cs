namespace softUniAssociativeArraysBonus
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Player> players = new List<Player>();
            string command = Console.ReadLine();

            while(command != "Season end")
            {
                string commandType = command.Split()[1];
                switch(commandType)
                {
                    case "->":
                        string[] data = command.Split(" -> ");
                        string playerName = data[0];
                        string position = data[1];
                        int skill = int.Parse(data[2]);

                        Player player = new Player(playerName, new Dictionary<string, int>());
                        player.PositionsAndSkill.Add(position, skill);

                        bool playerExists = false;
                        foreach(Player testPl in players)
                        {
                            if(testPl.Name == playerName)
                            {
                                playerExists = true;
                                int posSkill = 0;
                                bool posExists = false;
                                foreach(string pos in testPl.PositionsAndSkill.Keys)
                                {
                                    posSkill = testPl.PositionsAndSkill[pos];
                                    if(pos == position) posExists = true;
                                }
                                if(posExists) posSkill = testPl.UpdateSkill(skill, posSkill);
                                else testPl.PositionsAndSkill.Add(position, skill);
                                break;
                            }
                        }

                        if(!playerExists) players.Add(player);
                        break;
                    case "vs":
                        string[] battlingPlayers = command.Split(" vs ");
                        string plName1 = battlingPlayers[0];
                        string plName2 = battlingPlayers[1];

                        Player pl1 = new Player("", new Dictionary<string, int>());
                        Player pl2 = new Player("", new Dictionary<string, int>());
                        bool pl1Exists = false;
                        bool pl2Exists = false;
                        foreach(Player testPl in players)
                        {
                            if(testPl.Name == plName1)
                            {
                                pl1Exists = true;
                                pl1 = testPl;
                            }
                            if(testPl.Name == plName2)
                            {
                                pl2Exists = true;
                                pl2 = testPl;
                            }
                        }
                        if(pl1Exists && pl2Exists)
                        {
                            foreach(string pos1 in pl1.PositionsAndSkill.Keys)
                            {
                                foreach(string pos2 in pl2.PositionsAndSkill.Keys)
                                {
                                    if(pos1 == pos2)
                                    {
                                        int pl1Skill = pl1.PositionsAndSkill[pos1];
                                        int pl2Skill = pl2.PositionsAndSkill[pos2];
                                        if(pl1Skill > pl2Skill) players.Remove(pl2);
                                        else if(pl1Skill < pl2Skill) players.Remove(pl1);
                                    }
                                }
                            }
                        }
                        break;
                }

                command = Console.ReadLine();
            }

            players = players
                .OrderByDescending(p => p.PositionsAndSkill.Sum(p => p.Value))
                .ThenBy(p => p.Name)
                .ToList();
            foreach(Player player in players)
            {
                Console.WriteLine($"{player.Name}: {player.PositionsAndSkill.Sum(p => p.Value)} skill");
                player.PositionsAndSkill = player.PositionsAndSkill
                        .OrderByDescending(p => p.Value)
                        .ThenBy(p => p.Key)
                        .ToDictionary(p => p.Key, p => p.Value);
                foreach(string pos in player.PositionsAndSkill.Keys)
                {
                    Console.WriteLine($"- {pos} <::> {player.PositionsAndSkill[pos]}");
                }
            }
        }
    }

    class Player
    {
        public string Name { get; set; }
        public Dictionary<string, int> PositionsAndSkill { get; set; }

        public Player(string name, Dictionary<string, int> positionsAndSkill)
        {
            Name = name;
            PositionsAndSkill = positionsAndSkill;
        }

        public int UpdateSkill(int newSkill, int oldSkill)
        {
            return Math.Max(newSkill, oldSkill);
        }
    }
}