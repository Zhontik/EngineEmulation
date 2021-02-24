using System;
using System.Collections.Generic;
using System.Text;

namespace EngineEmulate
{
    interface ITestStand
    {
        /// <summary>
        /// Test target engine using internal test;
        /// </summary>
        /// <param name="outTemperature"> Temperature outside. </param>
        /// <param name="engine"> Engine to test. </param>
        void StartEngineTesting(int outTemperature, IEngine engine);

    }
}
