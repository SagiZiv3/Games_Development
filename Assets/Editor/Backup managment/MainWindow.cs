namespace UnityBackupManagment
{
    using UnityEditor;
    using UnityEngine;
    internal class MainWindow : EditorWindow
    {
        public event System.Action OnEnableEvent, OnDisableEvent;
        private WindowTabUI[] windowTabs;
        private string[] tabs;
        private byte selectedTab;

        internal void Initialize(params WindowTabUI[] windowTabs)
        {
            this.windowTabs = windowTabs;
            tabs = new string[windowTabs.Length];
            for (int i = 0; i < windowTabs.Length; i++)
            {
                tabs[i] = windowTabs[i].TabName;
            }
        }

        private void OnGUI()
        {
            selectedTab = (byte)GUILayout.Toolbar(selectedTab, tabs);
            windowTabs[selectedTab].OnGUI();
        }
        private void OnEnable()
        {
            OnEnableEvent?.Invoke();
        }
        private void OnDisable()
        {
            OnDisableEvent?.Invoke();
        }
    }
}