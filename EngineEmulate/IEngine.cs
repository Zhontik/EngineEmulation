using System;
using System.Collections.Generic;
using System.Text;

namespace EngineEmulate
{
    interface IEngine
    {

        int temperatureOverheat { get; set; }
        double tnow { get; set; }

        /// <summary>
        /// Takes a time step of 1 second;
        /// Change speed and temerature using internal rules;
        /// </summary>
        /// <param name="outTemperature"> Temperature outside. </param>
        void TimeStep(int outTemperature);
        
        /// <summary>
        /// Print all the parametres about the engine.
        /// </summary>
        void Info();
    }
}
