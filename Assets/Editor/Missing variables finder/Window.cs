using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace EditorUtils
{
    internal class Window : EditorWindow
    {
        public Func<bool, IList<ErrorInfo>> findProblems;

        private IList<ErrorInfo> errors;
        private IList<string> namespacesToIgnore = new List<string>();
        private Vector2 scrollPosition;
        private string namespaceToAdd;
        private bool includePrefabs, displayNamespaces;

        public void Initialize(IList<string> namespacesToIgnore)
        {
            this.namespacesToIgnore = namespacesToIgnore;
        }
        public void Initialize(IList<ErrorInfo> errorInfos)
        {
            errors = errorInfos;
        }

        private void OnGUI()
        {
            GUILayout.Space(10f);
            if (GUILayout.Button(errors == null ? "Find" : "Refresh"))
            {
                errors = findProblems?.Invoke(includePrefabs);
            }
            GUILayout.BeginVertical();
            includePrefabs = GUILayout.Toggle(includePrefabs, "Include prefabs");
            DisplayNamespacesToIgnore();
            GUILayout.EndVertical();
            DisplayErrors();
        }
        private void DisplayNamespacesToIgnore()
        {
            displayNamespaces = EditorGUILayout.Foldout(displayNamespaces, "Namespaces to ignore:",
                true, EditorStyles.foldout);
            if (!displayNamespaces) return;
            float width = position.width - 20;
            AddNamespaceToIgnore(width);

            #region Scroll view
            StartScrollView(position.width - 20f, 150f);
            GUILayout.BeginVertical();
            for (int i = 0; i < namespacesToIgnore.Count; i++)
            {
                DisplayNamespaceTextFieldAndRemoveButton(ref i, width);
                GUILayout.Space(7.5f);
            }
            GUILayout.EndVertical();
            StopScrollView();
            #endregion
        }
        private void DisplayErrors()
        {
            if (errors == null) return;
            float width = position.width - 20;
            GUILayout.Label($"{errors.Count} error(s) found:", EditorStyles.boldLabel);
            GUILayout.Space(5f);
            StartScrollView(width, 350f);
            GUILayout.BeginVertical();
            foreach (var error in errors)
            {
                GUILayout.Space(7.5f);
                GUILayout.BeginHorizontal();
                GUILayout.Label(error.icon, GUILayout.Width(20f), GUILayout.Height(20f));
                error.display = EditorGUILayout.Foldout(error.display, error.path, true);
                GUILayout.EndHorizontal();
                if (!error.display) continue;
                GUILayout.Label(error.message, GUILayout.Width(width-10f));
                if (GUILayout.Button("Select object", GUILayout.Width(width - 10f)))
                {
                    if (PrefabUtility.IsPartOfPrefabAsset(error.referencedObject))
                    {
                        AssetDatabase.OpenAsset(error.referencedObject);
                    }
                    Selection.activeObject = error.referencedObject;
                }
            }
            GUILayout.EndVertical();
            StopScrollView();
        }
        private void DisplayNamespaceTextFieldAndRemoveButton(ref int i, float width)
        {
            GUILayout.BeginHorizontal();
            namespacesToIgnore[i] = GUILayout.TextField(namespacesToIgnore[i], GUILayout.Width(width - 50f));
            if (GUILayout.Button("-", GUILayout.Width(25f)))
            {
                namespacesToIgnore.RemoveAt(i);
                i--;
            }
            GUILayout.EndHorizontal();
        }
        private void AddNamespaceToIgnore(float width)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Add", GUILayout.Width(75f));
            namespaceToAdd = GUILayout.TextField(namespaceToAdd, GUILayout.Width(width - 110f));
            if (!string.IsNullOrWhiteSpace(namespaceToAdd) && GUILayout.Button("+", GUILayout.Width(25f)))
            {
                namespacesToIgnore.Add(namespaceToAdd);
                namespaceToAdd = string.Empty;
            }

            GUILayout.EndHorizontal();
        }
        private void StartScrollView(float width, float height)
        {
            GUILayout.BeginHorizontal("Box");
            GUILayout.FlexibleSpace();
            scrollPosition = GUILayout.BeginScrollView(
                scrollPosition, GUILayout.Height(height), GUILayout.Width(width));
            GUILayout.BeginHorizontal("Box");
        }
        private void StopScrollView()
        {
            GUILayout.EndHorizontal();
            GUILayout.EndScrollView();
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
    }
}