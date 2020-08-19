﻿using System;
using System.Threading;

namespace Dan_LIV_Kristina_Garcia_Francisco
{
    /// <summary>
    /// The car race simulator
    /// </summary>
    class Raceing
    {
        #region Local variables
        /// <summary>
        /// Countdown until the race starts
        /// </summary>
        public static readonly CountdownEvent raceCDE = new CountdownEvent(1);
        /// <summary>
        /// Only one car can recharge their tank at a time
        /// </summary>
        private SemaphoreSlim gasStationQueue = new SemaphoreSlim(1);
        /// <summary>
        /// Print winnder ranking results as cars cross the finish line
        /// </summary>
        private EventWaitHandle winner = new AutoResetEvent(true);
        /// <summary>
        /// Write top red car result if it exists after the rank text
        /// </summary>
        private EventWaitHandle infoText = new AutoResetEvent(false);
        /// <summary>
        /// Create random values
        /// </summary>
        private static readonly Random rng = new Random();
        /// <summary>
        /// Save the current semaphore color
        /// </summary>
        private static string semaphoreColor = "";
        /// <summary>
        /// Array of possible semaphore colors
        /// </summary>
        private readonly string[] colorOptions = { "Red", "Green" };
        /// <summary>
        /// Counts the amount of cars
        /// </summary>
        private static int carCounter = 0;
        /// <summary>
        /// Counts the amount of cars that corossed the finish line
        /// </summary>
        private static int winCarCounter = 0;
        /// <summary>
        /// Checks if there is the Number One fastest red car
        /// </summary>
        private bool noOne = false;
        /// <summary>
        /// Checks if the race is over
        /// </summary>
        private bool raceOver = false;
        #endregion

        /// <summary>
        /// Ongoing car race
        /// </summary>
        /// <param name="auto">The car in the race</param>
        public void CarRaceProcess(Automobile auto)
        {
            // Making sure all 3 cars start the race at the same time
            raceCDE.Signal();
            raceCDE.Wait();

            // All cars start moving around the same time
            auto.Move(auto);

            // While the car tank has gas he can run the race
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
                break;
            }

            // Checks if the car is out of gas
            if (auto.TankVolume <= 0)
            {
                auto.Stop(auto);
                Console.WriteLine("{0} {1} is out of gas and left the race.", auto.Color, auto.Producer);
            }
            else if (auto.TankVolume > 0)
            {
                Console.WriteLine("{0} {1} crossed the finish line.", auto.Color, auto.Producer);
                // Reset the tank volume
                auto.TankVolume = 45;
                auto.Stop(auto);
            }

            winner.WaitOne();
            FinalResult(auto);
        }

        /// <summary>
        /// Tank Volume decreasing every one second as a backgroun process until all cars pass the finish line or give up
        /// </summary>
        public void TankDecrease()
        {
            // Do this while the race is ongoing
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
        /// Changes the color of the semaphore as a background process until all cars pass the semaphore
        /// </summary>
        public void ChangeSemaphoreColor()
        {
            Console.WriteLine("\t\t\t\t\tSemaphore is now active.");
            for (int i = 0; i < 3; i++)
            {
                // Checks if all cars passed the semaphore before stopping to work
                if (carCounter == 3)
                {
                    Console.WriteLine("All cars passed the semaphore.");
                    carCounter = 0;
                    break;
                }

                // Resets the for loop
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
        /// Charging the car tanks one by one
        /// </summary>
        public void ChargingAtGasStation(Automobile auto)
        {
            gasStationQueue.Wait();
            Console.WriteLine("{0} {1} is charging their tank", auto.Color, auto.Producer);
            auto.TankVolume = 45;
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

            // Increment only for cars that crossed the finish line
            if (auto.TankVolume > 0)
            {
                Interlocked.Increment(ref winCarCounter);
            }

            // Print ranking board
            if (winCarCounter == 1 && auto.TankVolume > 0)
            {

                Console.WriteLine("\n\t\t\t\t\tRanking board:\n\t\t\t\t\t{0}. {1} {2}", winCarCounter, auto.Color, auto.Producer);
            }
            else if (auto.TankVolume > 0)
            {
                Console.WriteLine("\t\t\t\t\t{0}. {1} {2}", winCarCounter, auto.Color, auto.Producer);
            }

            // Print top red car board
            if (auto.Color == "Red" && noOne == false && auto.TankVolume > 0)
            {
                noOne = true;
                FastestRedCar(auto);
            }
            else if (carCounter == 3 && Program.containsRedCar == false)
            {
                Console.WriteLine("\nThere were no red cars in the race.");
            }
         
            // Stop the backgroun porcess if all cars crossed the finish line
            if (carCounter == 3)
            {
                infoText.Set();
                raceOver = true;
            }

            winner.Set();
        }

        /// <summary>
        /// Fastest red card board
        /// </summary>
        /// <param name="auto">Shows the red cars as the pass the finish line</param>
        public void FastestRedCar(Automobile auto)
        {
            winner.Set();
            infoText.WaitOne();
            Console.WriteLine("\nFastest red car is: {0} {1}.", auto.Color, auto.Producer);
        }
    }
}
