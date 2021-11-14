using System;
using System.Reflection;
using UnityEngine;
using Object = UnityEngine.Object;

namespace EditorUtils
{
    internal class MissingOrNullObjectErrorFinder : ErrorsFinder
    {
        public override bool CanValidate(FieldInfo fieldInfo)
        {
            return fieldInfo.FieldType.IsSubclassOf(typeof(Object));
        }

        public override string Find(Object script, FieldInfo fieldInfo)
        {
            var value = fieldInfo.GetValue(script);
            string error;
            if (!string.IsNullOrEmpty(error = FindMissingReference(fieldInfo, value))) return error;
            
            // // If the field can be null, we don't have to assign it
            // if (Attribute.IsDefined(fieldInfo, typeof(CanBeNullAttribute))) return string.Empty;

            // If the value is null, and it is visible in the inspector
            return value == null ? "Value wasn't assigned" : string.Empty;
        }

        private static string FindMissingReference(FieldInfo fieldInfo, object value)
        {
            if (!fieldInfo.FieldType.IsSubclassOf(typeof(Object)) || value == null) return string.Empty;
            try
            {
                var dummy = ((Object) value).name; // Try to use value
            }
            catch (Exception exception) when ( exception is MissingReferenceException  ||
                                               exception is UnassignedReferenceException || 
                                               exception is MissingComponentException) { return exception.Message;}
            return string.Empty;
        }
    }
}