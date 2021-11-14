using System.Reflection;
using UnityEngine;

namespace EditorUtils
{
    public abstract class ErrorsFinder
    {
        public abstract bool CanValidate(FieldInfo fieldInfo);
        public abstract string Find(Object script, FieldInfo fieldInfo);
    }
}