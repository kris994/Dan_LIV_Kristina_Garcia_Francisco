using System;
using System.Threading;

namespace Dan_LIV_Kristina_Garcia_Francisco
{
    class Raceing
    {
        public static readonly CountdownEvent ready = new CountdownEvent(1);
        private static readonly Random rng = new Random();
        private static string semaphoreColor = "";
        private readonly string[] colorOptions = { "Green", "Red" };

        /// <summary>
        /// Ongoing car race
        /// </summary>
        /// <param name="auto">The car in the race</param>
        public void CarRaceProcess(Automobile auto)
        {
            ready.Signal();
            auto.Move(auto);
            Thread.Sleep(10);

            if (semaphoreColor == "Red")
            {
                Console.WriteLine(auto.Color + " " + auto.Producer + " waiting on red light.");
            }

            Thread.Sleep(3);
        }

        /// <summary>
        /// Tank Volume decreasing every one second
        /// </summary>
        public void TankDecrease()
        {
            while(true)
            {
                for (int i = 0; i< Program.allAutomobiles.Count; i++)
                {
                    Program.allAutomobiles[i].TankVolume = Program.allAutomobiles[i].TankVolume - rng.Next(2, 5);
                    Console.WriteLine(Program.allAutomobiles[i].TankVolume);
                }
                Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// Changes the color of the semaphore
        /// </summary>
        public void ChangeSemaphoreColor()
        {
            for (int i = 0; i < 3; i++)
            {
                if (i == 2)
                {
                    i = 0;
                }
                semaphoreColor = colorOptions[i];
                Console.WriteLine(semaphoreColor);
                Thread.Sleep(2000);
            }
        }
    }
}
