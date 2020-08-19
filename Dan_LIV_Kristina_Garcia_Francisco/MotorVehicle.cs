using System;

namespace Dan_LIV_Kristina_Garcia_Francisco
{
    /// <summary>
    /// The Motor Vehicle class that is inherited by other motor vehicles
    /// </summary>
    abstract class MotorVehicle
    {
        #region Property
        public double MotorVolume { get; set; }
        public int Weight { get; set; }
        public string Category { get; set; }
        public string MotorType { get; set; }
        public string Color { get; set; }
        public int MotorNumber { get; set; }
        #endregion

        #region Loacal Variables
        /// <summary>
        /// Array of colors
        /// </summary>
        protected readonly string[] allColors = { "Red", "Blue", "Orange", "Yellow" };
        /// <summary>
        /// Array of categories
        /// </summary>
        protected readonly string[] allCategory = { "M", "N", "L", "T" };
        /// <summary>
        /// Array of motor types
        /// </summary>
        protected readonly string[] allMotorType = { "DC", "BLDC", "PMSM", "SRM" };
        /// <summary>
        /// Random value generator
        /// </summary>
        public static Random rng = new Random();
        #endregion

        public MotorVehicle()
        {

        }

        /// <summary>
        /// Creates a specific vehicle
        /// </summary>
        public abstract void Create();

        /// <summary>
        /// Method that puts a vehicle into motion
        /// </summary>
        /// <param name="obj">vehicle we are running</param>
        public virtual void Move(object obj)
        {
            Console.WriteLine("Turn on the motor and start going.");
        }

        /// <summary>
        /// Method that stops the vehicles motor
        /// </summary>
        /// <param name="obj">vehicle we are running</param>
        public virtual void Stop(object obj)
        {
            Console.WriteLine("Turn off the motor and stopped moving.");
        }
    }
}
