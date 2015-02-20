using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ProxyActivator
{
    class State
    {
        public String Text;
        public Color Color;
        public State(string Text, Color Color)
        {
            this.Text = Text;
            this.Color = Color;
        }
    }
}
