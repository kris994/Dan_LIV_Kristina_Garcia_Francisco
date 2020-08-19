using System;
using System.Threading;

namespace Dan_LIV_Kristina_Garcia_Francisco
{
    class Raceing
    {
        public static readonly CountdownEvent raceCDE = new CountdownEvent(1);
        private SemaphoreSlim gasStationQueue = new SemaphoreSlim(1);
        private EventWaitHandle winner = new AutoResetEvent(true);
        private EventWaitHandle infoText = new AutoResetEvent(false);
        private static readonly Random rng = new Random();
        private static string semaphoreColor = "";
        private readonly string[] colorOptions = { "Red", "Green" };
        private static int carCounter = 0;
        private bool noOne = false;
        private bool raceOver = false;

        /// <summary>
        /// Ongoing car race
        /// </summary>
        /// <param name="auto">The car in the race</param>
        public void CarRaceProcess(Automobile auto)
        {
            // Making sure all 3 cars start the race at the same time
            raceCDE.Signal();
            raceCDE.Wait();

            auto.Move(auto);
            while (auto.TankVolume > 0)
            {
                // Driving towards the semaphore
                Thread.Sleep(10000);
                Console.WriteLine("{0} {1} reached the semaphore, currently the light is {2}", auto.Color, auto.Producer, semaphoreColor);
                while (semaphoreColor == "Red")
                {
                    Thread.Sleep(0);
                }

                // Thread safe counter
                Interlocked.Increment(ref carCounter);

                // Driving towards the gas station
                Thread.Sleep(3000);
                if (auto.TankVolume < 15)
                {
                    auto.Stop(auto);
                    ChargingAtGasStation(auto);
                }
                else
                {
                    Console.WriteLine("{0} {1} passed the gas station with tank: {2}l.", auto.Color, auto.Producer, auto.TankVolume);
                }

                // Driving to the finish line
                Thread.Sleep(7000);
                auto.Stop(auto);
                break;
            }

            if (auto.TankVolume <= 0)
            {
                auto.Stop(auto);
                Console.WriteLine("{0} {1} is out of gas and left the race.", auto.Color, auto.Producer);
                Program.allAutomobiles.Remove(auto);
            }

            winner.WaitOne();
            FinalResult(auto);
        }

        /// <summary>
        /// Tank Volume decreasing every one second
        /// </summary>
        public void TankDecrease()
        {
            while(raceOver == false)
            {
                for (int i = 0; i< Program.allAutomobiles.Count; i++)
                {
                    Program.allAutomobiles[i].TankVolume = Program.allAutomobiles[i].TankVolume - rng.Next(1, 5);
                }
                Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// Changes the color of the semaphore
        /// </summary>
        public void ChangeSemaphoreColor()
        {
            Console.WriteLine("\t\t\t\t\tSemaphore is now active.");
            for (int i = 0; i < 3; i++)
            {
                if (carCounter == 3)
                {
                    Console.WriteLine("All cars passed the semaphore.");
                    carCounter = 0;
                    break;
                }

                if (i == 2)
                {
                    i = 0;
                }

                semaphoreColor = colorOptions[i];
                Console.WriteLine("\t\t\t\t\t{0} light.", semaphoreColor);
                Thread.Sleep(2000);
            }
        }

        /// <summary>
        /// Charging the car tank
        /// </summary>
        public void ChargingAtGasStation(Automobile auto)
        {
            gasStationQueue.Wait();
            Console.WriteLine("{0} {1} is charging their tank", auto.Color, auto.Producer);
            auto.TankVolume = 40;
            Console.WriteLine("{0} {1} finished charging their tank", auto.Color, auto.Producer);
            gasStationQueue.Release();
        }

        /// <summary>
        /// Prints out the final result depending on the circumstances
        /// </summary>
        /// <param name="auto">The car that crossed the finish line</param>
        public void FinalResult(Automobile auto)
        {
            // Thread safe counter
            Interlocked.Increment(ref carCounter);
            if (carCounter == 3)
            {
                raceOver = true;
            }

            if (carCounter == 1 && auto.TankVolume > 0)
            {
                Console.WriteLine("\n\t\t\t\t\tRanking board:\n\t\t\t\t\t{0}. {1} {2}", carCounter, auto.Color, auto.Producer);
            }
            else if (auto.TankVolume > 0)
            {
                Console.WriteLine("\t\t\t\t\t{0}. {1} {2}", carCounter, auto.Color, auto.Producer);
            }

            if (auto.Color == "Red" && noOne == false && auto.TankVolume > 0)
            {
                noOne = true;
                FastestRedCar(auto);
            }
            else if (carCounter == 3 && Program.containsRedCar == false)
            {
                Console.WriteLine("\nThere were no red cards in the race.");
            }
            winner.Set();

            if (carCounter == 3)
            {
                infoText.Set();
            }
        }

        public void FastestRedCar(Automobile auto)
        {
            winner.Set();
            infoText.WaitOne();
            Console.WriteLine("\nFastest red car is: {0} {1}.", auto.Color, auto.Producer);
        }
    }
}
