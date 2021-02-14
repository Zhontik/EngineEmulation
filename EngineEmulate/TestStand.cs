using System;

/// <summary>
/// Summary description for Class1
/// </summary>

namespace EngineEmulate
{
    class TestStand
    {
        public int Start(int Tout, InterCombEngine engine)
        {
            if (Tout >= engine.Toverheat)
            {
                return 0;
            }
            float InstSpeed = 0;
            double InstTemp = Tout;
            int time = 0;
            while (InstTemp < engine.Toverheat)
            {
                time++;
                InstSpeed += engine.Accelerate(InstSpeed);
                if (InstSpeed > engine.maxSpeed)
                {
                    InstSpeed = engine.maxSpeed;
                    if (engine.Cold(Tout - InstTemp) + engine.Heat(engine.maxSpeed) <= 0.000000001d)
                    {
                        return -1;
                    }
                }
                InstTemp += engine.Heat(InstSpeed) + engine.Cold(Tout - InstTemp);
                //Console.WriteLine($"Время:{time} Скорость:{InstSpeed} Темппература:{InstTemp}");
            }
            return time;
        }
    }
}