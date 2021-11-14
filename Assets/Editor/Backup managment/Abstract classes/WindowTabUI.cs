namespace UnityBackupManagment
{
    using UnityEngine;
    internal abstract class WindowTabUI
    {
        internal abstract string TabName { get; }
        protected MainWindow mainWindow;
        protected Vector2 scrollPosition;

        protected WindowTabUI(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }
        internal abstract void OnGUI();
        protected void StartScrollView(float width, float height)
        {
            GUILayout.BeginHorizontal("Box");
            GUILayout.FlexibleSpace();
            scrollPosition = GUILayout.BeginScrollView(
                scrollPosition, GUILayout.ExpandWidth(true), GUILayout.Height(height), GUILayout.MinWidth(width));
            GUILayout.BeginHorizontal("Box");
        }
        protected void StopScrollView()
        {
            GUILayout.EndHorizontal();
            GUILayout.EndScrollView();
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
    }
}