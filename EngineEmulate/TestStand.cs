using System;

/// <summary>
/// Summary description for Class1
/// </summary>

namespace EngineEmulate
{
    class OverheatTestStand : ITestStand
    {
        public void StartEngineTesting(int outTemperature, IEngine engine)
        {
            int time = 0;
            if (engine.temperatureOverheat > outTemperature)
            {
                double temperatureLast = outTemperature; 
                while (engine.tnow < engine.temperatureOverheat)
                {
                    time++;
                    engine.TimeStep(outTemperature);
                    Console.WriteLine($"Время:{time} Предыдущая:{temperatureLast} Темппература:{engine.tnow}");

                    if (engine.tnow - temperatureLast <= 0.00001d)
                    {
                        time = -1;
                        break;
                    }
                    temperatureLast = engine.tnow;
                }
            }
            PrintResults(time);
        }


        private void PrintResults(int result)
        {
            if (result < 0)
            {
                Console.WriteLine("Двигатель не перегревается или его скорость перегрева критически мала.");
            }
            else if (result == 0)
            {
                Console.WriteLine($"Двигатель перегрет изначально.");
            }
            else
            {
                Console.WriteLine($"Двигатель перегревается через {result} сек.");
            }
        }
    }
}