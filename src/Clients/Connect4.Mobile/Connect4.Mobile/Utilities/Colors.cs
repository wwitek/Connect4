﻿using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.Mobile.Utilities
{
    public class Colors
    {
        public CCColor4B StartColor => new CCColor4B(1, 36, 76);

        public CCColor4B EndColor => new CCColor4B(4, 79, 162);

        public CCColor4B RedColor => new CCColor4B(196, 2, 51);

        public CCColor4B YellowColor => new CCColor4B(255, 211, 0);

        public CCColor4B CircleLighterColor => new CCColor4B(178, 216, 255);

        public CCColor4B CircleBorderLight => new CCColor4B(1, 36, 76);
    }
}