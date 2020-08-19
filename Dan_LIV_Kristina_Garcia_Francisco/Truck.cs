using System;

namespace Dan_LIV_Kristina_Garcia_Francisco
{
    class Truck : MotorVehicle
    {
        public double LoadCapacity { get; set; }
        public double Hight { get; set; }
        public int SeatNumber { get; set; }

        public void Load()
        {
            Console.WriteLine("Fill up the truck");
        }

        public void Unload()
        {
            Console.WriteLine("Remove items from the truck");
        }
    }
}
