﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Picket_Placer
{
    class Fence
    {

        public Fence()
        {

        }

        public Fence(double length, double width, double maxSpace)
        {
            double WallLength = length;
            double PicketWidth = width;
            double MaxSpace = maxSpace;
        }

        public double WallLength { get; set; }
        public double PicketWidth { get; set; }
        public double MaxSpace { get; set; }

    }
}
