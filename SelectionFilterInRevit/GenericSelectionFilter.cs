using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
using HcBimUtils.DocumentUtils;

namespace RevitDev.SelectionFilterInRevit
{
    public class GenericSelectionFilter: ISelectionFilter
    {
        private BuiltInCategory _category;
        public GenericSelectionFilter(BuiltInCategory category) 
        {
            _category = category;
        }
        public bool AllowElement(Element elem)
        {
            if (elem.Category == null) return false;
            if (elem.Category.ToBuiltinCategory() == _category) return true;
            return false;
        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            return false;
        }
    }
}
