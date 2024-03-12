using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Nice3point.Revit.Toolkit.External;

namespace RevitDev.Commands
{
    [Transaction(TransactionMode.Manual)]
    internal class Command1 : ExternalCommand
    {
        public override void Execute()
        {
            try
            {
                var activeView = Document.ActiveView;

                var columnFilter = new ElementCategoryFilter(BuiltInCategory.OST_Walls);
                using (var tsg = new TransactionGroup(Document, "tran gr"))
                {
                    tsg.Start();
                    //--------

                    using (var ts = new Transaction(Document, "name transaction"))
                    {
                        ts.Start();
                        //--------
                        activeView.SetCategoryHidden(columnFilter.CategoryId, false);
                        //--------
                        ts.Commit();
                    }
                    //--------
                    tsg.Assimilate();
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
