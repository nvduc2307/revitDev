﻿using Autodesk.Revit.DB;
using System.Collections.Generic;
using System.Linq;

namespace RevitDev.FilterElements
{
    public static class FilterElement
    {
        public static List<T> GetElementsFromCategory<T>(this Document document, View view, BuiltInCategory builtInCategory, bool isType)
        {
            var results = new List<T>();
            if (isType)
            {
                results = new FilteredElementCollector(document, view.Id)
                .WhereElementIsElementType()
                .OfCategory(builtInCategory)
                .Cast<T>()
                .Where(x => x != null)
                .ToList();
            }
            else
            {
                results = new FilteredElementCollector(document, view.Id)
                .WhereElementIsNotElementType()
                .OfCategory(builtInCategory)
                .Cast<T>()
                .Where(x => x != null)
                .ToList();
            }
            return results;
        }
        public static List<T> GetElementsFromCategory<T>(this Document document, BuiltInCategory builtInCategory, bool isType)
        {
            var results = new List<T>();
            if (isType)
            {
                results = new FilteredElementCollector(document)
                .WhereElementIsElementType()
                .OfCategory(builtInCategory)
                .Cast<T>()
                .Where(x => x != null)
                .ToList();
            }
            else
            {
                results = new FilteredElementCollector(document)
                .WhereElementIsNotElementType()
                .OfCategory(builtInCategory)
                .Cast<T>()
                .Where(x => x != null)
                .ToList();
            }
            return results;
        }
        public static List<T> GetElementsFromClass<T>(this Document document, bool isType)
        {
            var results = new List<T>();
            if (isType)
            {
                results = new FilteredElementCollector(document)
                .WhereElementIsElementType()
                .OfClass(typeof(T))
                .Cast<T>()
                .Where(x => x != null)
                .ToList();
            }
            else
            {
                results = new FilteredElementCollector(document)
                .WhereElementIsNotElementType()
                .OfClass(typeof(T))
                .Cast<T>()
                .Where(x => x != null)
                .ToList();
            }
            return results;
        }
        public static List<T> GetElementsFromClass<T>(this Document document)
        {
            var results = new List<T>();
            if (typeof(T) != typeof(View) && typeof(T) != typeof(View3D))
            {
                results = new FilteredElementCollector(document)
                .OfClass(typeof(T))
                .Cast<T>()
                .Where(x => x != null)
                .ToList();
            }
            return results;
        }
        public static List<T> GetViewsFromClass<T>(this Document document, bool isTemplate)
        {
            var results = new List<T>();
            if (typeof(T) == typeof(View) || typeof(T) == typeof(View3D))
            {
                if (isTemplate)
                {
                    var views = new FilteredElementCollector(document)
                    .OfClass(typeof(View))
                    .Cast<View>()
                    .Where(x => x != null)
                    .Where(x => x.IsTemplate)
                    .ToList();
                    results = views as List<T>;
                }
                else
                {
                    var views = new FilteredElementCollector(document)
                    .OfClass(typeof(View))
                    .Cast<View>()
                    .Where(x => x != null)
                    .Where(x => !x.IsTemplate)
                    .ToList();
                    results = views as List<T>;
                }
            }
            return results;
        }
    }
}
