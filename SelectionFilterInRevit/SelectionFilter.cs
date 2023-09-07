using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
using HcBimUtils.DocumentUtils;

namespace RevitDev.SelectionFilterInRevit
{
    public class SelectionFilter
    {
        public class ParkingSelectionFilter : ISelectionFilter
        {
            public bool AllowElement(Element element)
            {
                if (element.Category == null)
                {
                    return false;
                }
                if (element.Category.ToBuiltinCategory() == BuiltInCategory.OST_Parking)
                {
                    return true;
                }
                return false;
            }

            public bool AllowReference(Reference refer, XYZ point)
            {
                return false;
            }
        }
        public class FloorSelectionFilter : ISelectionFilter
        {
            public bool AllowElement(Element element)
            {
                if (element.Category == null)
                {
                    return false;
                }
                if (element.Category.ToBuiltinCategory() == BuiltInCategory.OST_Floors)
                {
                    return true;
                }
                return false;
            }

            public bool AllowReference(Reference refer, XYZ point)
            {
                return false;
            }
        }
        public class BeamSelectionFilter : ISelectionFilter
        {
            public bool AllowElement(Element element)
            {
                if (element.Category == null)
                {
                    return false;
                }
                if (element.Category.ToBuiltinCategory() == BuiltInCategory.OST_StructuralFraming)
                {
                    return true;
                }
                return false;
            }

            public bool AllowReference(Reference refer, XYZ point)
            {
                return false;
            }
        }

        public class TopoSurfaceSelectionFilter : ISelectionFilter
        {
            public bool AllowElement(Element element)
            {
                if (element.Category == null)
                {
                    return false;
                }
                if (element.Category.ToBuiltinCategory() == BuiltInCategory.OST_Topography)
                {
                    return true;
                }
                return false;
            }

            public bool AllowReference(Reference refer, XYZ point)
            {
                return false;
            }
        }

        public class ModelLineSelectionFilter : ISelectionFilter
        {
            public bool AllowElement(Element element)
            {
                if (element.Category == null)
                {
                    return false;
                }
                if (element.Category.ToBuiltinCategory() == BuiltInCategory.OST_Lines & element is ModelLine)
                {
                    return true;
                }
                return false;
            }

            public bool AllowReference(Reference refer, XYZ point)
            {
                return false;
            }
        }
    }
}
