using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;

namespace HelloAutocadPlugIn.Enums
{
    //Group Codes in Numerical Order:
    //http://www.autodesk.com/techpubs/autocad/acadr14/dxf/group_codes_in_numerical_order_al_u05_c.htm
    /// <summary>
    /// Код Autocad элемента.
    /// </summary>
    public enum AdTypeCodes
    {
        Entity = 0, // Графические элементы
        Layer = 8 // Слой
    }
}
