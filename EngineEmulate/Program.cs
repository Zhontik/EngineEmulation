using System;

namespace EngineEmulate
{
    class InterCombEngine
    {
        private float I;
        private int[] M;
        private int[] V;
        public int Toverheat;
        private float HeatMomentum;
        private float HeatVelocity;
        private float ColdTemp;

        private int Mnow;
        public int maxSpeed;

        public InterCombEngine(float I, int[] M, int[] V, int Toverheat, float HeatMomentum, float HeatVelocity, float ColdTemp)
        {
            this.I = (I < 1) ? 1 : I;
            for(int i = 0; i < M.Length; i++)
            {
                if (M[i] < 0)
                {
                    M[i] = 1;
                }
            }
            this.M = M;
            this.V = V;
            this.Toverheat = Toverheat;
            this.HeatMomentum = (HeatMomentum < 0) ? 0 : HeatMomentum;
            this.HeatVelocity = (HeatVelocity < 0) ? 0 : HeatVelocity;
            this.ColdTemp = (ColdTemp < 0) ? 0 : ColdTemp;
            maxSpeed = V[V.Length - 1];
            Mnow = M[0];
        }

        public double Heat(float v)
        {
            return Mnow * HeatMomentum + v * v * HeatVelocity;
        }

        public double Cold(double TSubst)
        {
            return ColdTemp * TSubst;
        }

        public float Accelerate(float v)
        {
            int i;
            for(i = 0; i < V.Length; i++)
            {
                if (v > V[i])
                {
                    Mnow = M[i];
                }
            }
            return Mnow / I;
        }

        public void Info()
        {
            Console.WriteLine($"Момент инерции: {I}");
            Console.Write("Момент вращения: ");
            for(int i = 0; i < M.Length; i++)
            {
                Console.Write(M[i] + " ");
            }
            Console.Write("\nСкорость: ");
            for (int i = 0; i < V.Length; i++)
            {
                Console.Write(V[i] + " ");
            }
            Console.WriteLine($"\nТемпература перегрева: {Toverheat}");
            Console.WriteLine($"Коэффициент передачи тепла от момента: {HeatMomentum}");
            Console.WriteLine($"Коэффициент передачи тепла от скорости: {HeatVelocity}");
            Console.WriteLine($"Коэффициент охлаждения: {ColdTemp}");
        }
    }

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
                if(InstSpeed > engine.maxSpeed)
                {
                    InstSpeed = engine.maxSpeed;
                    if (engine.Cold(Tout - InstTemp) + engine.Heat(engine.maxSpeed) <= 0.000000001d)
                    {
                        return -1;
                    }              
                }
                InstTemp += engine.Heat(InstSpeed) + engine.Cold(Tout-InstTemp);
                //Console.WriteLine($"Время:{time} Скорость:{InstSpeed} Темппература:{InstTemp}");
            }
            return time;
        }
    }

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
