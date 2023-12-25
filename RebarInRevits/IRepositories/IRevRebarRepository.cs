using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using System.Collections.Generic;

namespace RevitDev.RebarInRevits.IRepositories
{
    public interface IRevRebarRepository
    {
        Rebar CreateRebar(List<Curve> polygon, BuiltInCategory builtInCategoryHost);
        Rebar CreateRebarLayoutAsMaximumSpacing(List<Curve> polygon, double spacingFt, double lengthArrFt);
        Rebar CreateRebarLayoutAsFixedNumber(List<Curve> polygon, int qty, double lengthArrFt);
        List<Rebar> CreateRebarTapper(List<Curve> polygon1, List<Curve> polygon2, double spacingFt, BuiltInCategory builtInCategoryHost);
    }
}
