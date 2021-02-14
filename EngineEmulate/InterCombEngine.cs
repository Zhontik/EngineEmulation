using System;
//Internal Combustion Engine(has a lot of variables)

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
            for (int i = 0; i < M.Length; i++)
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
            for (i = 0; i < V.Length; i++)
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
            for (int i = 0; i < M.Length; i++)
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
}
