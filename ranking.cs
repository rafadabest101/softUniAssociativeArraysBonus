namespace softUniAssociativeArraysBonus
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> contests = new Dictionary<string, string>();
            List<User> users = new List<User>();

            string contest = Console.ReadLine();
            while(contest != "end of contests")
            {
                string name = contest.Split(':')[0];
                string password = contest.Split(':')[1];

                contests.Add(name, password);
                contest = Console.ReadLine();
            }

            string submission = Console.ReadLine();

            while(submission != "end of submissions")
            {
                string contestName = submission.Split("=>")[0];
                string password = submission.Split("=>")[1];
                string username = submission.Split("=>")[2];
                int points = int.Parse(submission.Split("=>")[3]);

                if(contests.ContainsKey(contestName) && contests[contestName] == password)
                {
                    User user = new User(username, new Dictionary<string, int>());

                    bool userExists = false;
                    foreach(User testUser in users)
                    {
                        if(testUser.Name == username)
                        {
                            userExists = true;
                            if(testUser.contests.ContainsKey(contestName)) testUser.Update(contestName, points);
                            else testUser.contests.Add(contestName, points);
                            break;
                        }
                    }
                    if(!userExists)
                    {
                        users.Add(user);
                        user.contests.Add(contestName, points);
                    }
                }
                submission = Console.ReadLine();
            }

            Console.WriteLine($"Best candidate is {GetUserWithMaxPoints(users).Name} " +
                $"with total {GetUserWithMaxPoints(users).GetMaxPoints()} points.");
            Console.WriteLine("Ranking: ");

            users = users.OrderBy(u => u.Name).ToList();

            foreach(User user in users) Console.WriteLine(user.ToString());
        }

        static User GetUserWithMaxPoints(List<User> users)
        {
            User userWithMaxPoints = new User("", new Dictionary<string, int>());
            int maxPoints = 0;
            foreach(User user in users)
            {
                if(user.GetMaxPoints() >= maxPoints)
                {
                    maxPoints = user.GetMaxPoints();
                    userWithMaxPoints = user;
                }
            }
            return userWithMaxPoints;
        }
    }
    class User
    {
        public string Name { get; set; }
        public Dictionary<string, int> contests = new Dictionary<string, int>();

        public User(string name, Dictionary<string, int> contests)
        {
            Name = name;
            this.contests = contests;
        }

        public override string ToString()
        {
            string result = $"{Name}";
            contests = contests.OrderByDescending(con => con.Value).ToDictionary(con => con.Key, con => con.Value);
            foreach(string name in contests.Keys)
            {
                result += $"\n#  {name} -> {contests[name]}";
            }
            return result;
        }

        public void Update(string contestName, int points)
        {
            if(points > contests[contestName]) contests[contestName] = points;
        }

        public int GetMaxPoints()
        {
            int sum = 0;
            foreach(int points in contests.Values)
            {
                sum += points;
            }
            return sum;
        }
    }
}