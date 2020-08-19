﻿using System;

namespace Dan_LIV_Kristina_Garcia_Francisco
{
    /// <summary>
    /// Truck class that inherits from Motor Vehicle
    /// </summary>
    class Truck : MotorVehicle
    {
        #region Property
        public double LoadCapacity { get; set; }
        public double Hight { get; set; }
        public int SeatNumber { get; set; }
        #endregion

        public Truck() : base()
        {
        }

        /// <summary>
        /// Method used to load a truck
        /// </summary>
        public void Load()
        {
            Console.WriteLine("Fill up the truck");
        }

        /// <summary>
        /// Method used to unload a truck
        /// </summary>
        public void Unload()
        {
            Console.WriteLine("Remove items from the truck");
        }

        /// <summary>
        /// Creates a specific truck
        /// </summary>
        public override void Create()
        {
            Truck truck = new Truck
            {
                MotorVolume = Math.Round(rng.NextDouble() * 1000, 2),
                Weight = rng.Next(1000, 2001),
                Category = allCategory[rng.Next(0, allCategory.Length)],
                MotorType = allMotorType[rng.Next(0, allMotorType.Length)],
                Color = allColors[rng.Next(0, allColors.Length)],
                MotorNumber = rng.Next(50, 101),
                LoadCapacity = Math.Round(rng.NextDouble() * 10000, 2),
                Hight = Math.Round(rng.NextDouble() * 100, 2),
                SeatNumber = rng.Next(2, 7),
            };

            Program.allTruck.Add(truck);
        }
    }
}
