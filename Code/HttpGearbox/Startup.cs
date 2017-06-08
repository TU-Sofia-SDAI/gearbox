using GearboxComputer;
using GearboxContracts;
using System;
using System.Net;

namespace HttpGearbox
{
    public class Startup
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please, enter correct values!");
            Console.WriteLine("Enter the REST url:");
            string urlToHit = Console.ReadLine();

            var gearbox = new FiveSpeedGearBox.FiveSpeedGearBox();
            var listener = new Listener(urlToHit);
            var communicator = new Communicator();

            EngineType desiredType;

            Console.WriteLine("Enter engine type: (petrol, diesel)");
            switch (Console.ReadLine())
            {
                case "petrol":
                    desiredType = EngineType.Gasoline;
                    break;
                case "diesel":
                    desiredType = EngineType.Diesel;
                    break;
                default:
                    desiredType = EngineType.Gasoline;
                    break;
            }

            var gearboxComputer = new GearboxComputerLogic(EngineType.Diesel, gearbox, listener, communicator);

            while (true)
            {
                Console.WriteLine("Enter current gear:");
                int currentGear = int.Parse(Console.ReadLine());

                gearboxComputer.SetGear(currentGear);

                Console.WriteLine("Enter current accelerationLevel:");
                int accLevel = int.Parse(Console.ReadLine());

                DrivingMode mode;
                Console.WriteLine("Enter driving mode: (normal, sport, economy)");
                switch (Console.ReadLine())
                {
                    case "normal":
                        mode = DrivingMode.Normal;
                        break;
                    case "sport":
                        mode = DrivingMode.Sport;
                        break;
                    case "economy":
                        mode = DrivingMode.Economycal;
                        break;
                    default:
                        mode = DrivingMode.Normal;
                        break;
                }

                Console.WriteLine("Enter current RPM:");
                int rpm = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter current speed:");
                int speed = int.Parse(Console.ReadLine());

                gearboxComputer.Calculate(
                new GearboxComputerData
                {
                    AccelerationLevel = accLevel,
                    DrivingMode = mode,
                    RPM = rpm,
                    VehicleSpeed = speed
                });

                Console.WriteLine("================================");
            }
        }
    }
    public class Listener : IGearboxComputerListener
    {
        private string url;
        public Listener(string url)
        {
            this.url = url;
        }
        public void Receive(int currentGear, int nextGear)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                moduleName = "Gearbox",
                value = new { currentGear = currentGear, nextGear = nextGear }
            });
            string result = "";
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                result = client.UploadString(this.url, "POST", json);
            }
            Console.WriteLine(result);
            Console.WriteLine("Current gear is {0}, and gearbox computer suggests {1}", currentGear, nextGear);
        }
    }

    public class Communicator : ICarComunication
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }

        public void Warn(string message)
        {
            Console.WriteLine(message);
        }
    }
}
