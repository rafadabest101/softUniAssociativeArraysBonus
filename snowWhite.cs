namespace softUniAssociativeArraysBonus
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Dwarf> dwarves = new List<Dwarf>();
            string command = Console.ReadLine();

            while(command != "Once upon a time")
            {
                string[] description = command.Split(" <:> ");
                string name = description[0];
                string hatColor = description[1];
                int physics = int.Parse(description[2]);

                Dwarf dwarf = new Dwarf(name, hatColor, physics);
                bool dwarfWithNameExists = false;
                bool dwarfWithSameNameAndDiffHC = false;
                bool dwarfWithSameNameAndSameHC = false;
                foreach(Dwarf dw in dwarves)
                {
                    if(dw.Name == name)
                    {
                        dwarfWithNameExists = true;
                        if(dw.HatColor != hatColor)
                        {
                            dwarfWithSameNameAndDiffHC = true;
                        }
                        else
                        {
                            dwarfWithSameNameAndSameHC = true;
                            dw.Physics = dw.UpdatePhysics(dw.Physics, physics);
                        }
                    }
                }
                if(!dwarfWithNameExists) dwarves.Add(dwarf);
                else if(dwarfWithSameNameAndDiffHC && !dwarfWithSameNameAndSameHC) dwarves.Add(dwarf);

                command = Console.ReadLine();
            }

            dwarves = dwarves
                .OrderByDescending(d => d.Physics)
                .ThenByDescending(d => d.GetCountOfHatColor(d.HatColor, dwarves))
                .ToList();
            foreach(Dwarf dw in dwarves)
            {
                Console.WriteLine(dw);
            }
        }
    }

    class Dwarf
    {
        public string Name { get; set; }
        public string HatColor { get; set; }
        public int Physics { get; set; }

        public override string ToString()
        {
            return $"({HatColor}) {Name} <-> {Physics}";
        }

        public Dwarf(string name, string hatColor, int physics)
        {
            Name = name;
            HatColor = hatColor;
            Physics = physics;
        }

        public int UpdatePhysics(int ph1, int ph2)
        {
            return Math.Max(ph1, ph2);
        }

        public int GetCountOfHatColor(string hatColor, List<Dwarf> dwarves)
        {
            int count = 0;
            foreach(Dwarf dwarf in dwarves)
            {
                if(dwarf.HatColor == hatColor) count++;
            }
            return count;
        }
    }
}