using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPITraining_lab2._4
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            List<Duct> ducts = new FilteredElementCollector(doc)
                .OfClass(typeof(Duct))
                .Cast<Duct>()
                .ToList();

            int ductsLevel1 = 0;
            int ductsLevel2 = 0;

            foreach (var duct in ducts)
            {
                string level = duct.get_Parameter(BuiltInParameter.RBS_START_LEVEL_PARAM).AsValueString();
                if (level == "Level 1")
                {
                    ductsLevel1++;
                }
                else if (level == "Level 2")
                {
                    ductsLevel2++;
                }
            }

            TaskDialog.Show("Количество воздуховодов", $"Количество воздуховодов на 1 этаже: {ductsLevel1}{Environment.NewLine}" 
                                                     + $"Количество воздуховодов на 2 этаже: {ductsLevel2}");
            return Result.Succeeded;
        }
    }
}
