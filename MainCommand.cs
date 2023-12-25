using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using HcBimUtils.DocumentUtils;
using Nice3point.Revit.Toolkit.External;
using RevitDev.RebarInRevits.IRepositories;
using RevitDev.RebarInRevits.Models;
using RevitDev.RebarInRevits.Repositories;

namespace RevitDev
{
    [Transaction(TransactionMode.Manual)]
    public class MainCommand : ExternalCommand
    {
        private IRevRebarRepository IRevRebarRepository;
        public override void Execute()
        {
            AC.GetInformation(UiDocument);
            try
            {

                using (var ts = new TransactionGroup(AC.Document, "name transaction"))
                {
                    ts.Start();
                    //--------
                    var revRebar = new RevRebar()
                    {

                    };
                    var revRebarRepository = new RevRebarRepository(AC.Document, revRebar);
                    //--------
                    ts.Commit();
                }
            }
            catch (System.Exception)
            {

                throw;
            }
            // test commit to origin main
            // first commit to origin develop
        }
    }
}
