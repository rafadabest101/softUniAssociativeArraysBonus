namespace softUniAssociativeArraysBonus
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, List<Dragon>> dragonsByType = new Dictionary<string, List<Dragon>>();
            int n = int.Parse(Console.ReadLine());

            for(int i = 1; i <= n; i++)
            {
                string[] data = Console.ReadLine().Split().ToArray();
                string type = data[0];
                string name = data[1];
                int damage;
                if(data[2] == "null") damage = 45;
                else damage = int.Parse(data[2]);
                int health;
                if(data[3] == "null") health = 250;
                else health = int.Parse(data[3]);
                int armor;
                if(data[4] == "null") armor = 10;
                else armor = int.Parse(data[4]);

                Dragon dragon = new Dragon(name, damage, health, armor);

                bool dragonExists = false;
                foreach(string tp in dragonsByType.Keys)
                {
                    foreach(Dragon dr in dragonsByType[tp])
                    {
                        if(dr.Name == name && tp == type)
                        {
                            dragonExists = true;

                            dr.Damage = dragon.Damage;
                            dr.Health = dragon.Health;
                            dr.Armor = dragon.Armor;

                            break;
                        }
                    }
                }
                if(!dragonExists)
                {
                    if(!dragonsByType.ContainsKey(type)) dragonsByType.Add(type, new List<Dragon>() { dragon });
                    else dragonsByType[type].Add(dragon);
                }
            }

            foreach(string type in dragonsByType.Keys)
            {
                Console.WriteLine($"{type}::" +
                    $"({dragonsByType[type].Average(d => d.Damage):f2}/" +
                    $"{dragonsByType[type].Average(d => d.Health):f2}/" +
                    $"{dragonsByType[type].Average(d => d.Armor):f2})");
                dragonsByType[type] = dragonsByType[type]
                        .OrderBy(d => d.Name)
                        .ToList();
                foreach(Dragon dragon in dragonsByType[type])
                {
                    Console.WriteLine(dragon);
                }
            }
        }
    }

    class Dragon
    {
        public string Name { get; set; }
        public int Damage { get; set; }
        public int Health { get; set; }
        public int Armor { get; set; }

        public override string ToString()
        {
            return $"-{Name} -> damage: {Damage}, health: {Health}, armor: {Armor}";
        }

        public Dragon(string name, int damage, int health, int armor)
        {
            Name = name;
            Damage = damage;
            Health = health;
            Armor = armor;
        }
    }
}