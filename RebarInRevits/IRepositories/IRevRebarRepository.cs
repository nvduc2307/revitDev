using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using System.Collections.Generic;

namespace RevitDev.RebarInRevits.IRepositories
{
    public interface IRevRebarRepository
    {
        Rebar CreateRebar(List<Curve> polygon, BuiltInCategory builtInCategoryHost);
        Rebar CreateRebarLayoutAsMaximumSpacing(List<Curve> polygon, double spacingFt, double lengthArrFt, BuiltInCategory builtInCategoryHost);
        Rebar CreateRebarLayoutAsFixedNumber(List<Curve> polygon, int qty, double lengthArrFt, BuiltInCategory builtInCategoryHost);
        List<Rebar> CreateRebarTapper(List<Curve> polygon1, List<Curve> polygon2, double spacingFt, BuiltInCategory builtInCategoryHost);
    }
}
