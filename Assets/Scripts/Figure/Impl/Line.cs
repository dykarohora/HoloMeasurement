using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HoloMeasurement.Figure.Impl
{
    public class Line : BaseFigure
    {
        private Point _start;
        private Point _end;

        public Line(Point start, Point end)
        {
            _start = start;
            _start.IsLineElem = true;
            _end = end;
            _end.IsLineElem = true;
        }


    }
}