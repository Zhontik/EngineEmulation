using System;

namespace EngineEmulate
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Введите температуру окружающей среды:");
            int Tstart = Convert.ToInt32(Console.ReadLine());

            InterCombEngine engine = new InterCombEngine(
                10f,
                new int[] {20, 75, 100, 105, 75,  0},
                new int[] {0,  75, 150, 200, 250, 300},
                110,
                0.01f,
                0.0001f,
                0.1f);
            engine.Info();

            OverheatTestStand OverheatStand = new OverheatTestStand();
            OverheatStand.StartEngineTesting(Tstart, engine);

            

            Console.ReadKey();
        }
    }
}
