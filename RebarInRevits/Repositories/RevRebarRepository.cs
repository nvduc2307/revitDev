using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using RevitDev.RebarInRevits.IRepositories;
using RevitDev.RebarInRevits.Models;
using RevitDev.Utils;

namespace RevitDev.RebarInRevits.Repositories
{
    public class RevRebarRepository : IRevRebarRepository
    {
        public Rebar CreateSingleRebar(Document document, RevRebar revRebar)
        {
            var hostFakeColum = ElementInRevit.CreateHost(BuiltInCategory.OST_StructuralColumns); ;
            Rebar rebar = null;
            using (var ts = new Transaction(document, "name transaction"))
            {
                ts.SkipOutSiteHost();
                ts.Start();
                //--------
                rebar = Rebar.CreateFromCurves(
                    document,
                    revRebar.Style,
                    revRebar.BarType,
                    revRebar.StartHook,
                    revRebar.EndHook,
                    hostFakeColum,
                    revRebar.Normal,
                    revRebar.Polygon,
                    revRebar.StartHookOrientation,
                    revRebar.EndHookOrientation,
                    revRebar.IsUseExistingShapeIfPosible,
                    revRebar.IsCreateNewShape);
                //--------
                ts.Commit();
            }
            return rebar;
        }
    }
}
