using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Dan_LIV_Kristina_Garcia_Francisco
{
    class Program
    {
        public static List<Automobile> allAutomobiles = new List<Automobile>();
        public static List<Truck> allTruck = new List<Truck>();
        public static List<Tractor> allTractor = new List<Tractor>();
        public static bool containsRedCar = false;

        static void Main(string[] args)
        {
            Automobile auto = new Automobile();
            Truck truck = new Truck();
            Tractor tractor = new Tractor();
            Raceing race = new Raceing();

            // Creating vehicle objects
            for (int i = 0; i < 2; i++)
            {
                auto.Create();
                truck.Create();
                tractor.Create();
            }

            // Countdown
            for (int i = 0; i < 6; i++)
            {
                Console.WriteLine(i);
                if (i == 5)
                {
                    break;
                }
                Thread.Sleep(1000);
            }
            
            // Starting threads
            for (int i = 0; i < allAutomobiles.Count; i++)
            {
                // Because i is not thread safe due to being located on the same memory location for every thread and it is incremented all the time.
                int temp = i;
                Thread thread1 = new Thread(() => race.CarRaceProcess(allAutomobiles[temp]));
                thread1.Start();
            }

            containsRedCar = Program.allAutomobiles.Any(car => car.Color == "Red");

            // Creating golf car
            Random rng = new Random();
            Automobile golf = new Automobile
            {
                Color = "Orange",
                TankVolume = 40,
                Producer = "Golf"
            };
            allAutomobiles.Add(golf);

            // Starting golf
            Console.WriteLine(golf.Color + " " + golf.Producer + " joined the race\n\n--------------------------");
            Thread thread2 = new Thread(() => race.CarRaceProcess(golf));
            thread2.Start();

            // Thread that changes the semaphore color every 2 seconds
            Thread thread3 = new Thread(race.ChangeSemaphoreColor)
            {
                IsBackground = true
            };
            thread3.Start();

            // Thread that reduces the Tank Volume every 1 second
            Thread thread4 = new Thread(race.TankDecrease)
            {
                IsBackground = true
            };
            thread4.Start();

            Console.ReadKey();
        }
    }
}
