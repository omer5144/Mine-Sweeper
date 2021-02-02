using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper.model
{
    class ControlPanelArgument
    {
        // Control Panel Argument:
        // this class represent the argument passes from Game to Control Panel

        public int Bombs { get; } // number of bombs remain
        public Situation Situation { get; } // the current win/lose/none situation

        public ControlPanelArgument(int bombs, Situation situation)
        {
            Bombs = bombs;
            Situation = situation;
        }
    }
}
