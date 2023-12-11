﻿using Autodesk.Revit.DB;
using System.Linq;

namespace RevitDev.Utils
{
    public static class SkipWarning
    {
        public static void SkipOutSiteHost(this Transaction ts)
        {
            var op = ts.GetFailureHandlingOptions();
            var preproccessor = new SkipWarningOutSiteHost();
            op.SetFailuresPreprocessor(preproccessor);
            ts.SetFailureHandlingOptions(op);
        }
        private class SkipWarningOutSiteHost : IFailuresPreprocessor
        {
            FailureProcessingResult IFailuresPreprocessor.PreprocessFailures(FailuresAccessor failuresAccessor)
            {
                var failMessage = failuresAccessor.GetFailureMessages();
                var failToList = failMessage.ToList();

                if (failToList.Any(item => BuiltInFailures.RebarFailures.CantMakeFillets == item.GetFailureDefinitionId() || item.GetFailureDefinitionId() == BuiltInFailures.RebarFailures.RebarShapeFailure))
                {
                    failuresAccessor.DeleteAllWarnings();
                    return FailureProcessingResult.ProceedWithCommit;
                }
                if (failToList.Any(item =>
                     item.GetFailureDefinitionId() == BuiltInFailures.RebarFailures.OutSideOfHost))
                {
                    var fma = failToList.First(item =>
                        item.GetFailureDefinitionId() == BuiltInFailures.RebarFailures.OutSideOfHost);
                    failuresAccessor.DeleteWarning(fma);
                    return FailureProcessingResult.ProceedWithCommit;
                }
                return FailureProcessingResult.Continue;
            }
        }
    }
}
