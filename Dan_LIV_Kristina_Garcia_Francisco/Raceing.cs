using System;
using System.Threading;

namespace Dan_LIV_Kristina_Garcia_Francisco
{
    class Raceing
    {
        public static readonly CountdownEvent ready = new CountdownEvent(1);
        private static readonly Random rng = new Random();
        private readonly object tankVolumeAmount = new object();
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
            OngoingRace(10, auto);

            if (semaphoreColor == "Red")
            {
                Console.WriteLine(auto.Color + " " + auto.Producer + " waiting on red light.");
                OngoingRace(2, auto);
            }

            OngoingRace(3, auto);


        }

        /// <summary>
        /// Time spent in race
        /// </summary>
        /// <param name="time">The time spent</param>
        /// <param name="auto">The car that is spenign time</param>
        public void OngoingRace(int time, Automobile auto)
        {
            for (int i = 0; i < time+1; i++)
            {
                // Thread safe decreasing the tank volume
                lock (tankVolumeAmount)
                {
                    auto.TankVolume = auto.TankVolume - rng.Next(2, 5);
                    Console.WriteLine(auto.TankVolume);
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
