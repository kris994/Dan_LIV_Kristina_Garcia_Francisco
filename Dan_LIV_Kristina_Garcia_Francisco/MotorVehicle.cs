using System;

namespace Dan_LIV_Kristina_Garcia_Francisco
{
    abstract class MotorVehicle
    {
        double MotorVolume { get; set; }
        public int Weight { get; set; }
        public string Category { get; set; }
        public string MotorType { get; set; }
        public string Color { get; set; }
        public int MotorNumber { get; set; }

        public virtual void Move()
        {
            Console.WriteLine("Turn on the motor and start going.");
        }
        public virtual void Stop()
        {
            Console.WriteLine("Turn off the motor.");
        }
    }
}
