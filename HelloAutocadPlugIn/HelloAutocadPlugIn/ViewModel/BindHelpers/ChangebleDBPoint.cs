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
    /// Оболочка для <see cref="DBPoint"/>, с возможностью измения позиции покоординатно
    /// </summary>
    public class ChangeableDBPoint
    {
        public DBPoint DBPoint { get; }

        public ChangeableDBPoint(DBPoint point)
        {
            DBPoint = point;
        }

        public string Id
        {
            get
            {
                string id = DBPoint.Id.ToString();
                return id.Trim('(', ')');
            }
        }

        public double PositionX
        {
            get { return DBPoint.Position.X; }
            set
            {
                if (DBPoint.Position.X == value)
                    return;

                DBPoint.Position = new Point3d(value, DBPoint.Position.Y, DBPoint.Position.Z);
            }
        }

        public double PositionY
        {
            get { return DBPoint.Position.Y; }
            set
            {
                if (DBPoint.Position.Y == value)
                    return;

                DBPoint.Position = new Point3d(DBPoint.Position.X, value, DBPoint.Position.Z);
            }
        }

        public double PositionZ
        {
            get { return DBPoint.Position.Z; }
            set
            {
                if (DBPoint.Position.Z == value)
                    return;

                DBPoint.Position = new Point3d(DBPoint.Position.X, DBPoint.Position.Y, value);
            }
        }
    }
}
