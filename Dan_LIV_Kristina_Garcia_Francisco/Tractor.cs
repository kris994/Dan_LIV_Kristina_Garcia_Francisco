using System;

namespace Dan_LIV_Kristina_Garcia_Francisco
{
    /// <summary>
    /// Tractor class that inherits from Motor Vehicle
    /// </summary>
    class Tractor : MotorVehicle
    {
        #region Property
        public double TireSize { get; set; }
        public int Wheelbase { get; set; }
        public string Drive { get; set; }
        #endregion

        #region Local Variables
        /// <summary>
        /// Array of drives
        /// </summary>
        private readonly string[] allDrive = { "A", "B", "C" };
        #endregion

        public Tractor() : base()
        {
        }

        /// <summary>
        /// Creates a specific tractor
        /// </summary>
        public override void Create()
        {
            Tractor tractor = new Tractor
            {
                MotorVolume = Math.Round(rng.NextDouble() * 1000, 2),
                Weight = rng.Next(1000, 2001),
                Category = allCategory[rng.Next(0, allCategory.Length)],
                MotorType = allMotorType[rng.Next(0, allMotorType.Length)],
                Color = allColors[rng.Next(0, allColors.Length)],
                MotorNumber = rng.Next(50, 101),
                TireSize = Math.Round(rng.NextDouble() * 100, 2),
                Wheelbase = rng.Next(2, 11),
                Drive = allDrive[rng.Next(0, allDrive.Length)],
            };

            Program.allTractor.Add(tractor);
        }
    }
}
