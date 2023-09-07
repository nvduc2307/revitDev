using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using HcBimUtils.DocumentUtils;
using Nice3point.Revit.Toolkit.External;
using RevitDev.EventInReVit;
using System;
using System.Collections.Generic;
using System.Windows;

namespace RevitDev
{
    [Transaction(TransactionMode.Manual)]
    public class MainCommand : ExternalCommand
    {
        public List<Element> Elements { get; set; }
        public override void Execute()
        {
            try
            {
                AC.GetInformation(UiDocument);
                PatchDocument.Start(
                    AC.Application,
                    AC.UiApplication, 
                    RevitCommandId.LookupPostableCommandId(PostableCommand.ModelLine));
                PatchDocument.OnPostableCommandModelLineEnded += OnPostableCommandModelLineEnded;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void OnPostableCommandModelLineEnded(object sender, EventArgs e)
        {
            MessageBox.Show(PatchDocument.ElemIds.Count.ToString());
            PatchDocument.OnPostableCommandModelLineEnded -= OnPostableCommandModelLineEnded;
        }
    }
}
