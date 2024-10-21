using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TjuvOPolis1
{

    public class City
    {
        private int width = 100;
        private int height = 25;
        private List<Person> persons;
        private int numberOfRobbedCitizens = 0;
        private int numberOfArrestedThieves = 0;

        public City(int numberOfPolice, int numberOfThieves, int numberOfCitizens)
        {
            persons = new List<Person>();
            Random rand = new Random();


            for (int i = 0; i < numberOfPolice; i++)
            {
                persons.Add(new Police(rand.Next(width), rand.Next(height)));
            }

            for (int i = 0; i < numberOfThieves; i++)
            {
                persons.Add(new Thief(rand.Next(width), rand.Next(height)));
            }


            for (int i = 0; i < numberOfCitizens; i++)
            {
                persons.Add(new Citizen(rand.Next(width), rand.Next(height)));
            }
        }

        public void StartSimulation()
        {
            while (true)
            {
                foreach (var person in persons)
                {
                    person.Move(width, height);

                    // Kontrollera om det finns någon annan person på samma position
                    foreach (var otherPerson in persons)
                    {
                        if (person != otherPerson && person.X == otherPerson.X && person.Y == otherPerson.Y)
                        {
                            HandleMeeting(person, otherPerson);
                        }
                    }
                }
                Thread.Sleep(2000);
                DrawCity();

            }
        }

        // Hanterar möten mellan personer
        private void HandleMeeting(Person person1, Person person2)
        {
            if (person1 is Police police && person2 is Thief thief)
            {
                police.Arrest(thief);
                numberOfArrestedThieves++;
            }
            else if (person1 is Thief thief2 && person2 is Citizen citizen)
            {
                thief2.Rob(citizen, thief2);
                numberOfRobbedCitizens++;
            }
        }


        private void DrawCity()
        {
            Console.Clear();
            DrawSquare();

            foreach (var person in persons)
            {
                Console.SetCursorPosition(person.X, person.Y);
                if (person is Police) Console.Write("P");
                else if (person is Thief) Console.Write("T");
                else if (person is Citizen) Console.Write("M");
            }

            Console.SetCursorPosition(0, height + 1); // Flytta cursor till botten av rutan så statsen hamnar utanför
            Console.WriteLine($"\nAntal rånade medborgare: {numberOfRobbedCitizens}");
            Console.WriteLine($"Antal gripna tjuvar: {numberOfArrestedThieves}");
        }

        private void DrawSquare()
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (i == 0 || i == height - 1 || j == 0 || j == width - 1)
                    {
                        Console.SetCursorPosition(j, i);
                        Console.Write("|");
                    }
                }
            }
        }

    }
}



