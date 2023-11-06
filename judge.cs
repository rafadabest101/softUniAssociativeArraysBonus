namespace softUniAssociativeArraysBonus
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Contest> contests = new List<Contest>();
            Dictionary<string, int> participants = new Dictionary<string, int>();

            string command = Console.ReadLine();
            while(command != "no more time")
            {
                string[] info = command.Split(" -> ");
                string username = info[0];
                string contestName = info[1];
                int points = int.Parse(info[2]);

                Participant participant = new Participant(username, points);
                Contest contest = new Contest(contestName, new List<Participant>());

                bool contestExists = false;
                bool participantExists = false;
                foreach(Contest conTest in contests)
                {
                    if(conTest.Name == contestName)
                    {
                        contestExists = true;
                        foreach(Participant part in conTest.Participants)
                        {
                            if(part.Name == username)
                            {
                                part.UpdatePoints(points);
                                participantExists = true;
                                break;
                            }
                        }
                        if(!participantExists) conTest.Participants.Add(participant);
                        break;
                    }
                }

                if(!contestExists)
                {
                    contest.Participants.Add(participant);
                    contests.Add(contest);
                }

                command = Console.ReadLine();
            }
            
            foreach(Contest contest in contests)
            {
                Console.WriteLine($"{contest.Name}: {contest.Participants.Count} participants");
                contest.Participants = contest.Participants
                    .OrderByDescending(p => p.Points)
                    .ThenBy(p => p.Name)
                    .ToList();
                for(int i = 0; i < contest.Participants.Count; i++)
                {
                    string partName = contest.Participants[i].Name;
                    int partPoints = contest.Participants[i].Points;

                    Console.WriteLine($"{i + 1}. {partName} <::> {partPoints}");

                    if(!participants.ContainsKey(partName)) participants.Add(partName, partPoints);
                    else participants[partName] += partPoints;
                }
            }

            participants = participants
                    .OrderByDescending(p => p.Value)
                    .ThenBy(p => p.Key)
                    .ToDictionary(p => p.Key, p => p.Value);
            Console.WriteLine("Individual standings:");
            int counts = 0;
            foreach(string name in participants.Keys)
            {
                counts++;
                Console.WriteLine($"{counts}. {name} -> {participants[name]}");
            }
        }
    }

    class Participant
    {
        public string Name { get; set; }
        public int Points { get; set; }

        public Participant(string name, int points)
        {
            Name = name;
            Points = points;
        }

        public void UpdatePoints(int newPoints)
        {
            if(newPoints > Points) Points = newPoints;
        }
    }

    class Contest
    {
        public string Name { get; set; }
        public List<Participant> Participants = new List<Participant>();

        public Contest(string name, List<Participant> participants)
        {
            Name = name;
            Participants = participants;
        }
    }
}