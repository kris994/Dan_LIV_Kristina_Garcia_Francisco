using System;

namespace Dan_LIV_Kristina_Garcia_Francisco
{
    class Automobile : MotorVehicle
    {
        #region Property
        public string RegistrationNumber { get; set; }
        public int DoorNumber { get; set; }
        public int TankVolume { get; set; }
        public string TransportType { get; set; }
        public string Producer { get; set; }
        public int TrafficNumber { get; set; }
        #endregion

        private readonly string[] allTransportType = { "A", "B", "C"};
        private readonly string[] allProducer = { "Toyota", "Golf", "Ford"};

        public Automobile() : base()
        {
        }

        public void Recolor(string color, int trafficNumber)
        {
            this.Color = color;
            this.TrafficNumber = trafficNumber;
        }

        public override void Create()
        {
            Automobile auto = new Automobile
            {
                MotorVolume = Math.Round(rng.NextDouble() * 1000, 2),
                Weight = rng.Next(1000, 2001),
                Category = allCategory[rng.Next(0, allCategory.Length)],
                MotorType = allMotorType[rng.Next(0, allMotorType.Length)],
                Color = allColors[rng.Next(0, allColors.Length)],
                MotorNumber = rng.Next(30, 101),
                RegistrationNumber = rng.Next(1000, 10000).ToString(),
                DoorNumber = rng.Next(2, 6),
                TankVolume = 40,
                TransportType = allTransportType[rng.Next(0, allTransportType.Length)],
                Producer = allProducer[rng.Next(0, allProducer.Length)],
                TrafficNumber = rng.Next(1000, 10000)
            };

            Raceing.raceCDE.AddCount();
            Program.allAutomobiles.Add(auto);
        }

        public override void Move(object obj)
        {
            Automobile auto = (Automobile)obj;
            Console.WriteLine(auto.Color + " " + auto.Producer + " started moving.");
        }

        public override void Stop(object obj)
        {
            Automobile auto = (Automobile)obj;
            Console.WriteLine(auto.Color + " " + auto.Producer + " turned off the motor.");
        }
    }
}
