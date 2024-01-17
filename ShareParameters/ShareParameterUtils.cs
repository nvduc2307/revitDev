using Autodesk.Revit.DB;
using RevitDev.Messages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RevitDev.Utils.ShareParameters
{
    public static class ShareParameterUtils
    {
        public static void ProjectParametersAdd(this Document document, Autodesk.Revit.ApplicationServices.Application application, string path, List<ParamDefinition> paramCreationDataList)
        {
            try
            {
                using (var ts = new Transaction(document, "Add Parameters To Project"))
                {
                    ts.Start();
                    //--------
                    string parametersFilename = application.SharedParametersFilename;
                    application.SharedParametersFilename = path;
                    Dictionary<SharedParamGroup, Definitions> groupDefinitions = GetSharedParamFileGroupDefinitions(application.OpenSharedParameterFile(), SharedParamGroup.General);
                    List<string> existingParams = new List<string>();
                    existingParams = new FilteredElementCollector(document)
                        .OfClass(typeof(SharedParameterElement))
                        .Select(x => x.Name)
                        .Distinct()
                        .ToList();

                    CategorySetManager setManager = new CategorySetManager(document);

                    foreach (var paramCreationData in paramCreationDataList)
                    {
                        paramCreationData.CreateParameter(document, existingParams, setManager, groupDefinitions);
                    }

                    application.SharedParametersFilename = parametersFilename;
                    //--------
                    ts.Commit();
                }
            }
            catch (Exception)
            {
                IO.ShowWarning("khong thanh cong");
            }
        }

        private static Dictionary<SharedParamGroup, Definitions> GetSharedParamFileGroupDefinitions(DefinitionFile definitionFile, SharedParamGroup sharedParamGroup)
        {
            Dictionary<SharedParamGroup, Definitions> dictionary = new Dictionary<SharedParamGroup, Definitions>
            {
                {sharedParamGroup, definitionFile.Groups.get_Item(sharedParamGroup.ToString()).Definitions}
            };
            return dictionary;
        }
    }
}
