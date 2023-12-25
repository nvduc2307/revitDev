using Autodesk.Revit.DB;
using System.Collections.Generic;
using System.Linq;

namespace RevitDev.FilterElements
{
    public static class FilterElement
    {
        public static List<T> GetElements<T>(this Document document, bool isType)
        {
            var results = new List<T>();
            if (isType)
            {
                results = new FilteredElementCollector(document)
                .WhereElementIsElementType()
                .OfClass(typeof(T))
                .Cast<T>()
                .ToList();
            }
            else
            {
                results = new FilteredElementCollector(document)
                .WhereElementIsNotElementType()
                .OfClass(typeof(T))
                .Cast<T>()
                .ToList();
            }
            return results;
        }
    }
}
