using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using RevitDev.RebarInRevits.Models;

namespace RevitDev.RebarInRevits.IRepositories
{
    public interface IRevRebarRepository
    {
        Rebar CreateSingleRebar(Document document, RevRebar revRebar);
    }
}
