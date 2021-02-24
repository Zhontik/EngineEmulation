using System;
//Internal Combustion Engine(has a lot of variables)

namespace EngineEmulate
{
    class InterCombEngine : IEngine
    {
        private float I;
        private int[] M;
        private int[] V;
        public int temperatureOverheat { get; set; }
        private float heatMomentum;
        private float heatVelocity;
        private float coldTemp;


        public double tnow { get; set; }
        private float Mnow;
        private float instantSpeed;

        public InterCombEngine(float I, int[] M, int[] V, int temperatureOverheat, float heatMomentum, float heatVelocity, float coldTemp)
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
            this.temperatureOverheat = temperatureOverheat;
            this.heatMomentum = (heatMomentum < 0) ? 0 : heatMomentum;
            this.heatVelocity = (heatVelocity < 0) ? 0 : heatVelocity;
            this.coldTemp = (coldTemp < 0) ? 0 : coldTemp;
            Mnow = M[0];
        }

        private double Heat(float v)
        {
            //Console.WriteLine($"{Mnow} {heatMomentum} {heatVelocity}");
            return Mnow * heatMomentum + v * v * heatVelocity;
        }

        private double Cold(double TSubstracted)
        {

            return coldTemp * TSubstracted;
        }

        private void Accelerate()
        {

            int i = 0;
            while(instantSpeed >= V[i])
            {
                i++;
            }

            // estimate momentum using linear function
            float k = (M[i] - M[i - 1]) / (V[i] - V[i - 1]);
            float c = M[i] - k * V[i];
            Mnow = k * instantSpeed + c;

            instantSpeed += Mnow / I; 

            int maxSpeed = V[V.Length - 1];
            if (instantSpeed >= maxSpeed)
            {
                instantSpeed = maxSpeed;
            }
        }

        public void TimeStep(int outTemperature)
        {
            if(instantSpeed == 0) 
            {
                tnow = outTemperature;
            }

            Accelerate();
            tnow += Heat(instantSpeed) + Cold(outTemperature - tnow);
            //Console.WriteLine($"speed:{instantSpeed} Темппература:{tnow}");
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
            Console.WriteLine($"\nТемпература перегрева: {temperatureOverheat}");
            Console.WriteLine($"Коэффициент передачи тепла от момента: {heatMomentum}");
            Console.WriteLine($"Коэффициент передачи тепла от скорости: {heatVelocity}");
            Console.WriteLine($"Коэффициент охлаждения: {coldTemp}");
        }
    }
}
