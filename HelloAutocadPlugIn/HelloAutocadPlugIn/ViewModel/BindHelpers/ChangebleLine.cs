using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;

namespace HelloAutocadPlugIn.ViewModel.BindHelpers
{
    /// <summary>
    /// Оболочка для <see cref="Line"/>, с возможностью измения позиции покоординатно
    /// </summary>
    public class ChangeableLine
    {
        public Line Line { get; }

        public string Id
        {
            get
            {
                string id = Line.Id.ToString();
                return id.Trim('(', ')');
            }
        }

        public ChangeableLine(Line line)
        {
            Line = line;
        }

        public double StartPointX
        {
            get { return Line.StartPoint.X; }
            set
            {
                if (Line.StartPoint.X == value)
                    return;

                Line.StartPoint = new Point3d(value, Line.StartPoint.Y, Line.StartPoint.Z);
            }
        }

        public double StartPointY
        {
            get { return Line.StartPoint.Y; }
            set
            {
                if (Line.StartPoint.Y == value)
                    return;

                Line.StartPoint = new Point3d(Line.StartPoint.X, value, Line.StartPoint.Z);
            }
        }

        public double StartPointZ
        {
            get { return Line.StartPoint.Z; }
            set
            {
                if (Line.StartPoint.Z == value)
                    return;

                Line.StartPoint = new Point3d(Line.StartPoint.X, Line.StartPoint.Y, value);
            }
        }

        public double EndPointX
        {
            get { return Line.EndPoint.X; }
            set
            {
                if (Line.EndPoint.X == value)
                    return;

                Line.EndPoint = new Point3d(value, Line.EndPoint.Y, Line.EndPoint.Z);
            }
        }

        public double EndPointY
        {
            get { return Line.EndPoint.Y; }
            set
            {
                if (Line.EndPoint.Y == value)
                    return;

                Line.EndPoint = new Point3d(Line.EndPoint.X, value, Line.EndPoint.Z);
            }
        }

        public double EndPointZ
        {
            get { return Line.EndPoint.Z; }
            set
            {
                if (Line.EndPoint.Z == value)
                    return;

                Line.EndPoint = new Point3d(Line.EndPoint.X, Line.EndPoint.Y, value);
            }
        }


    }
}
