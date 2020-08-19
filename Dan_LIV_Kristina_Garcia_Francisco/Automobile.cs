namespace Dan_LIV_Kristina_Garcia_Francisco
{
    class Automobile : MotorVehicle
    {
        public string RegistrationNumber { get; set; }
        public int DoorNumber { get; set; }
        public int TankVolume { get; set; }
        public string TransportType { get; set; }
        public string Producer { get; set; }
        public int TrafficNumber { get; set; }

        public void Recolor(string color, int trafficNumber)
        {
            this.Color = color;
            this.TrafficNumber = trafficNumber;
        }
    }
}
