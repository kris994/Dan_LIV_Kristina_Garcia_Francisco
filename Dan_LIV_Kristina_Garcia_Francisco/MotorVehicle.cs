using System;
using System.Threading;

namespace Dan_LIV_Kristina_Garcia_Francisco
{
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

        protected readonly string[] allColors = { "Red", "Blue", "Orange", "Yellow" };
        protected readonly string[] allCategory = { "M", "N", "L", "T" };
        protected readonly string[] allMotorType = { "DC", "BLDC", "PMSM", "SRM" };

        public static Random rng = new Random();

        public MotorVehicle()
        {

        }

        public abstract void Create();

        public virtual void Move(object obj)
        {
            Console.WriteLine("Turn on the motor and start going.");
        }

        public virtual void Stop(object obj)
        {
            Console.WriteLine("Turn off the motor.");
        }
    }
}
