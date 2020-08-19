using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dan_LIV_Kristina_Garcia_Francisco
{
    class Program
    {
        public static List<Automobile> allAutomobiles = new List<Automobile>();
        public static List<Truck> allTruck = new List<Truck>();
        public static List<Tractor> allTractor = new List<Tractor>();

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

            // Creating golf car
            Random rng = new Random();
            Automobile golf = new Automobile
            {
                Color = "Orange",
                TankVolume = rng.Next(50, 101),
                Producer = "Golf"
            };
            allAutomobiles.Add(golf);

            // Starting golf
            Console.WriteLine(golf.Color + " " + golf.Producer + " joined the race\n\n--------------------------");
            Thread thread2 = new Thread(() => race.CarRaceProcess(golf));
            thread2.Start();

            // Thread that changes the semaphore color
            Thread thread3 = new Thread(race.ChangeSemaphoreColor);
            thread3.IsBackground = true;
            thread3.Start();

            Console.ReadKey();
        }
    }
}
