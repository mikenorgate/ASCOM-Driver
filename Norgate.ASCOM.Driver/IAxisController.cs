using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCOM.Norgate
{
    public interface IAxisController
    {
        double Position { get; set; }
        double SlewRate { get; }

        void Connect();

        void Disconnect();

        Task Slew(Orientation orientation, TimeSpan time);

        void StartSlew(Orientation orientation);

        void StopSlew();
    }
}
