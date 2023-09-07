using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using System.Linq;
using static HcBimUtils.WarmingUtils.FailureUtil;
using FAMILY = Autodesk.Revit.DB.Family;

namespace RevitDev.FamilyInRevits
{
    public class FamilyInRevits
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public FamilyInRevits(string path, string name)
        {
            this.Path = path;
            this.Name = name;
        }
        public FamilyInstance Push(Document doc, FamilySymbol symbol, XYZ basePoint)
        {
            FamilyInstance result = null;
            using (Transaction ts = new Transaction(doc, "Push Family"))
            {
                var op = ts.GetFailureHandlingOptions();
                var preproccessor = new DiscardAndResolveAllWarning();
                op.SetFailuresPreprocessor(preproccessor);
                ts.SetFailureHandlingOptions(op);
                ts.Start();
                symbol.Activate();
                result = doc.Create.NewFamilyInstance(basePoint, symbol, StructuralType.NonStructural);
                ts.Commit();
            }
            return result;
        }
        public FamilyInstance Push(Document doc, FamilySymbol symbol, XYZ basePoint, View view)
        {
            FamilyInstance result = null;
            using (Transaction ts = new Transaction(doc, "Push Family"))
            {
                var op = ts.GetFailureHandlingOptions();
                var preproccessor = new DiscardAndResolveAllWarning();
                op.SetFailuresPreprocessor(preproccessor);
                ts.SetFailureHandlingOptions(op);
                ts.Start();
                symbol.Activate();
                result = doc.Create.NewFamilyInstance(basePoint, symbol, view);
                ts.Commit();
            }
            return result;
        }
        public FamilyInstance Push(Document doc, FamilySymbol symbol, XYZ basePoint, double angle, View view)
        {
            FamilyInstance result = null;
            using (Transaction ts = new Transaction(doc, "Push Family"))
            {
                var op = ts.GetFailureHandlingOptions();
                var preproccessor = new DiscardAndResolveAllWarning();
                op.SetFailuresPreprocessor(preproccessor);
                ts.SetFailureHandlingOptions(op);
                ts.Start();
                symbol.Activate();
                result = doc.Create.NewFamilyInstance(basePoint, symbol, view);
                result.Location.Rotate(Line.CreateBound(basePoint, new XYZ(basePoint.X, basePoint.Y, basePoint.Z + 1)), angle);
                ts.Commit();
            }
            return result;
        }
        public FamilySymbol Load(Document document)
        {
            var fileName = $"{Name}.rfa";
            using (Transaction t = new Transaction(document, "Insert family"))
            {
                t.Start();
                FAMILY family = null;
                FilteredElementCollector a = new FilteredElementCollector(document).OfClass(typeof(FAMILY));
                int n = a.Count<Element>(e => e.Name.Contains(fileName));
                if (0 > n)
                    document.LoadFamily(Path, out family);
                t.Commit();
            }
            return new FilteredElementCollector(document)
                .WhereElementIsElementType()
                .OfClass(typeof(FamilySymbol))
                .Where(x => x.Name.Contains(Name))
                .Cast<FamilySymbol>()
                .FirstOrDefault();
        }
    }
}
