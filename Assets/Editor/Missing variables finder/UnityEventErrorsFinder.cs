using System;
using System.Reflection;
using UnityEditor;
using UnityEngine.Events;
using Object = UnityEngine.Object;

namespace EditorUtils
{
    internal class UnityEventErrorsFinder : ErrorsFinder
    {
        public override bool CanValidate(FieldInfo fieldInfo)
        {
            return fieldInfo.FieldType.IsSubclassOf(typeof(UnityEventBase));
        }

        public override string Find(Object behaviour, FieldInfo unityEvent)
        {
            var property = new SerializedObject(behaviour).FindProperty(unityEvent.Name);
            var error = string.Empty;
            var persistentCalls = property.FindPropertyRelative("m_PersistentCalls.m_Calls");
            for (int i = persistentCalls.arraySize - 1; i >= 0; --i)
            {
                var call = persistentCalls.GetArrayElementAtIndex(i);
                var referencedObject = call.FindPropertyRelative("m_Target").objectReferenceValue;
                if (referencedObject == null)
                {
                    error = $"Target object is null at call #: {i + 1}";
                    break;
                }

                var objectFullName = referencedObject.GetType().AssemblyQualifiedName;
                if (Type.GetType($"{objectFullName}") == null)
                    error = $"Script has been deleted/renamed/moved at call #: {i + 1}";
                var methodName = call.FindPropertyRelative("m_MethodName").stringValue;
                if (FunctionExistAsPublicInObject(referencedObject, methodName)) continue; // All good with this call
                error = DoesFunctionExistAsPrivateInObject(referencedObject, methodName)
                    ? $"The method {methodName} was changed to private, at call #: {i + 1}"
                    : $"The method to invoke doesn't exist, at call #: {i + 1}";
                break;
            }

            return error;
        }

        private static bool FunctionExistAsPublicInObject(Object obj, string methodName)
        {
            try
            {
                var type = obj.GetType();
                var info = type.GetMethod(methodName);
                return info != null;
            }
            catch (AmbiguousMatchException)
            {
                return true;
            }
        }
        private static bool DoesFunctionExistAsPrivateInObject(Object obj, string methodName)
        {
            var type = obj.GetType();
            var info = type.GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic);
            return info != null;
        }
    }
}