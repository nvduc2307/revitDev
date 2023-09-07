using Autodesk.Revit.DB;
using System.Linq;

namespace RevitDev.EventInReVit
{
    public static class OverrideGraphics
    {
        public static void SetTransparenColorElement(Document doc, Element ele, Color color, int transparency)
        {
            using (var ts = new Transaction(doc, "Set"))
            {
                ts.Start();
                var graphic = new OverrideGraphicSettings();
                graphic.SetSurfaceTransparency(transparency);
                graphic.SetSurfaceBackgroundPatternColor(color);
                FillPatternElement fillPattern = new FilteredElementCollector(doc).OfClass(typeof(FillPatternElement)).Cast<FillPatternElement>().FirstOrDefault();
                if (fillPattern != null) graphic.SetSurfaceBackgroundPatternId(fillPattern.Id);
                doc.ActiveView.SetElementOverrides(ele.Id, graphic);
                ts.Commit();
            }
        }
        public static void SetTransparenColorElement_Without_Transaction(Document doc, Element ele, Color color, int transparency)
        {
            var graphic = new OverrideGraphicSettings();
            graphic.SetSurfaceTransparency(transparency);
            graphic.SetSurfaceBackgroundPatternColor(color);
            FillPatternElement fillPattern = new FilteredElementCollector(doc).OfClass(typeof(FillPatternElement)).Cast<FillPatternElement>().FirstOrDefault();
            if (fillPattern != null) graphic.SetSurfaceBackgroundPatternId(fillPattern.Id);
            doc.ActiveView.SetElementOverrides(ele.Id, graphic);
        }
    }
}
