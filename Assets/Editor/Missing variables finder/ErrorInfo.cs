using System.Reflection;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace EditorUtils
{
        internal class ErrorInfo
        {
            public Object referencedObject;
            public Texture2D icon;
            public string path;
            public string message;
            public bool display;
            public static ErrorInfo BuildErrorObject(Component script, FieldInfo fieldInfo, string errorMessage)
            {
                return new ErrorInfo
                {
                    referencedObject = script,
                    path = $"{script.name}/{script.GetType()}/{fieldInfo.Name}",
                    message = errorMessage,
                    icon = AssetPreview.GetMiniThumbnail(script.gameObject)
                };
            }
            public static ErrorInfo BuildMissingReferenceErrorObject(GameObject gameObject)
            {
                var pathBuilder = new StringBuilder(gameObject.name);
                var transform = gameObject.transform.parent;
                while (transform)
                {
                    pathBuilder.Insert(0, $"{transform.name}/");
                    transform = transform.parent;
                }
                return new ErrorInfo
                {
                    referencedObject = gameObject,
                    path = pathBuilder.ToString(),
                    message = "Missing reference!",
                    icon = AssetPreview.GetMiniThumbnail(gameObject)
                };
            }
    }
}