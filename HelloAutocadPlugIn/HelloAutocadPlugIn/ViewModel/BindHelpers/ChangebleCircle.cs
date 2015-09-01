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
    /// Оболочка для <see cref="Circle"/>, с возможностью измения позиции покоординатно
    /// </summary>
    public class ChangeableCircle
    {
        public Circle Circle { get; }

        public ChangeableCircle(Circle circle)
        {
            Circle = circle;
        }

        /// <summary>
        /// Cirle.Id в строковом виде, без ()
        /// </summary>
        public string Id
        {
            get
            {
                string id = Circle.Id.ToString();
                return id.Trim('(', ')');
            }
        }

        public double Radius
        {
            get { return Circle.Radius; }
            set
            {
                if ((Circle.Radius == value) ||
                    (value <= 0))
                    return;

                Circle.Radius = value;
            }
        }

        public double CenterX
        {
            get { return Circle.Center.X; }
            set
            {
                if (Circle.Center.X == value)
                    return;

                // Установить положение, с изменением одной координаты
                Circle.Center = new Point3d(value, Circle.Center.Y, Circle.Center.Z);
            }
        }

        public double CenterY
        {
            get { return Circle.Center.Y; }
            set
            {
                if (Circle.Center.Y == value)
                    return;

                Circle.Center = new Point3d(Circle.Center.X, value, Circle.Center.Z);
            }
        }

        public double CenterZ
        {
            get { return Circle.Center.Z; }
            set
            {
                if (Circle.Center.Z == value)
                    return;

                Circle.Center = new Point3d(Circle.Center.X, Circle.Center.Y, value);
            }
        }
    }
}
