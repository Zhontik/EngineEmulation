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
            //engine.Info();

            TestStand Stand1 = new TestStand();
            int result = Stand1.Start(Tstart, engine);

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

            Console.ReadKey();
        }
    }
}
