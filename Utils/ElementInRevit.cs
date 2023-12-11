using Autodesk.Revit.DB;
using HcBimUtils.DocumentUtils;

namespace RevitDev.Utils
{
    public static class ElementInRevit
    {
        public static Element CreateHost()
        {
            Element result;
            using (var ts = new Transaction(AC.Document, "Create host"))
            {
                ts.Start();
                result = DirectShape.CreateElement(AC.Document, new ElementId(BuiltInCategory.OST_Floors));
                ts.Commit();
            }
            return result;
        }
        public static Element CreateHost(BuiltInCategory builtInCategory)
        {
            Element result;
            using (var ts = new Transaction(AC.Document, "Create host"))
            {
                ts.Start();
                result = DirectShape.CreateElement(AC.Document, new ElementId(builtInCategory));
                ts.Commit();
            }
            return result;
        }
    }
}
