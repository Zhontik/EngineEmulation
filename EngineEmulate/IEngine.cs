using System;
using System.Collections.Generic;
using System.Text;

namespace EngineEmulate
{
    interface IEngine
    {
        void Start(int outTemperature);

        void Info();
    }
}
