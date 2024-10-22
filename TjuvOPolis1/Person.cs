using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TjuvOPolis1
{


    public class Person
    {
        public int X { get; set; }
        public int Y { get; set; } 
        public int XDirection { get; set; } 
        public int YDirection { get; set; } 
        public List<string> Inventory { get; set; } 

        public Person(int x, int y)
        {
            X = x;
            Y = y;
            Inventory = new List<string>();
            SetRandomDirection(); // Slumpen
        }

        public void SetRandomDirection()
        {
            Random rand = new Random();
            int direction = rand.Next(0, 6);
            switch (direction)
            {
                case 0: XDirection = 1; YDirection = 0; break;
                case 1: XDirection = -1; YDirection = 0; break;
                case 2: XDirection = 0; YDirection = 1; break;
                case 3: XDirection = 0; YDirection = -1; break;
                case 4: XDirection = 1; YDirection = 1; break;
                case 5: XDirection = -1; YDirection = -1; break;
            }
        }

        public void Move(int width, int height)
        {
            X = (X + XDirection + width) % width; // Gör så att personen kan "wrap-around" när de går utanför staden
            Y = (Y + YDirection + height) % height;
        }
    }

    public class Citizen : Person
    {
        public Citizen(int x, int y) : base(x, y)
        {
            Inventory.AddRange(new List<string> { "Nycklar", "Mobiltelefon", "Pengar", "Klocka" });
        }
    }

    public class Thief : Person
    {
        public Thief(int x, int y) : base(x, y)
        {
        }

        public void Rob(Citizen citizen, Thief thief)
        {
            if (citizen.Inventory.Count > 0)
            {
                Random rand = new Random();
                int itemIndex = rand.Next(0, citizen.Inventory.Count);
                string item = citizen.Inventory[itemIndex];
                thief.Inventory.Add(item);
                citizen.Inventory.RemoveAt(itemIndex);

                Console.SetCursorPosition(0, 26); // Detta skrivs så att texten hamnar på samma ställe när programmet körs.
                Console.WriteLine($"Tjuven rånar medborgaren och tar: {item}");
            }
        }
    }

    public class Police : Person
    {
        public Police(int x, int y) : base(x, y)
        {
        }


        public void Arrest(Thief thief)
        {

            Inventory.AddRange(thief.Inventory);
            thief.Inventory.Clear();

            Console.SetCursorPosition(0, 25); // Detta skrivs så att texten hamnar på samma ställe när programmet körs.
            Console.WriteLine("Polisen tar tjuven och beslagtar alla saker.");
        }
    }

}

