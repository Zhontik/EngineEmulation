using System;
//Internal Combustion Engine(has a lot of variables)

namespace EngineEmulate
{
    class InterCombEngine
    {
        private float I;
        private int[] M;
        private int[] V;
        private int toverheat;
        public int Toverheat => toverheat;
        private float heatMomentum;
        private float heatVelocity;
        private float coldTemp;


        private float tnow;
        public float Tnow => tnow;
        private float Mnow;
        public int maxSpeed;

        public InterCombEngine(float I, int[] M, int[] V, int toverheat, float heatMomentum, float heatVelocity, float coldTemp)
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
            this.toverheat = toverheat;
            this.heatMomentum = (heatMomentum < 0) ? 0 : heatMomentum;
            this.heatVelocity = (heatVelocity < 0) ? 0 : heatVelocity;
            this.coldTemp = (coldTemp < 0) ? 0 : coldTemp;
            maxSpeed = V[V.Length - 1];
            Mnow = M[0];
        }

        public double Heat(float v)
        {
            //Console.WriteLine($"{Mnow} {heatMomentum} {heatVelocity}");
            return Mnow * heatMomentum + v * v * heatVelocity;
        }

        public double Cold(double TSubstracted)
        {

            return coldTemp * TSubstracted;
        }

        public float Accelerate(float v)
        {
            
            if(v > maxSpeed)
            {
                Mnow = M[M.Length - 1];
                return Mnow / I;
            }

            int i = 0;
            while(v >= V[i])
            {
                i++;
            }

            // estimate momentum using linear function
            float k = (M[i] - M[i - 1]) / (V[i] - V[i - 1]);
            float c = M[i] - k * V[i];
            Mnow = k * v + c;
            return Mnow/I;
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
            Console.WriteLine($"\nТемпература перегрева: {toverheat}");
            Console.WriteLine($"Коэффициент передачи тепла от момента: {heatMomentum}");
            Console.WriteLine($"Коэффициент передачи тепла от скорости: {heatVelocity}");
            Console.WriteLine($"Коэффициент охлаждения: {coldTemp}");
        }
    }
}
