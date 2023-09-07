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
            
        }
    }
}
