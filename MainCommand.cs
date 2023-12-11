using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using HcBimUtils.DocumentUtils;
using Nice3point.Revit.Toolkit.External;
using RevitDev.EventInReVit;
using RevitDev.RebarInRevits.IRepositories;
using System;
using System.Collections.Generic;
using System.Windows;

namespace RevitDev
{
    [Transaction(TransactionMode.Manual)]
    public class MainCommand : ExternalCommand
    {
        private IRevRebarRepository IRevRebarRepository;
        public override void Execute()
        {

            // test commit to origin main
            // first commit to origin develop
        }
    }
}
