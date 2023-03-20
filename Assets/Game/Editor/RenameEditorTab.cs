 using System.Collections.Generic;
 using System.Linq;
 using UnityEngine;
 using UnityEditor;
 
 public class RenameEditorTab : EditorWindow
 {
     [MenuItem("Tools/B83/RenameEditorTabs")]
     static void Init()
     {
         GetWindow<RenameEditorTab>();
     }
 
     [System.Serializable]
     public class Window
     {
         public EditorWindow window;
         public bool changed = false;
         public GUIContent content;
     }
 
     Vector2 m_ScrollPos;
     List<Window> m_Windows = new List<Window>();
     [System.NonSerialized]
     HashSet<EditorWindow> m_EditorWindows = new HashSet<EditorWindow>();
     bool m_UpdateWindowTitles = false;
 
     private void OnEnable()
     {
         UpdateWindowList();
         m_UpdateWindowTitles = true;
         // delayed update
         EditorApplication.update += UpdateWindowTitles;
     }
     void UpdateWindowList()
     {
         m_EditorWindows.Clear();
         foreach (var win in m_Windows)
             m_EditorWindows.Add(win.window);
         foreach (var win in Resources.FindObjectsOfTypeAll<EditorWindow>())
         {
             if (!m_EditorWindows.Contains(win))
             {
                 Window newWindow = new Window {window = win, content = new GUIContent(win.titleContent)};
                 if (newWindow.content.text.Contains("Project"))
                 {
                     m_Windows.Add(newWindow);
                 }
               
               
             }
              
         }
         UpdateWindowTitles();
     }
     void UpdateWindowTitles()
     {
         EditorApplication.update -= UpdateWindowTitles;
         m_UpdateWindowTitles = false;
         foreach (var win in m_Windows)
         {
             if (win.changed && win.window)
             {
                 win.window.titleContent = win.content;
             }
         }
     }
     private void OnGUI()
     {
         if (GUILayout.Button("update window list"))
             UpdateWindowList();
         if (GUILayout.Button("save changes"))
             SaveTabNames();
         if (GUILayout.Button("load changes"))
             LoadTabNames();
         m_ScrollPos = GUILayout.BeginScrollView(m_ScrollPos);
         foreach (var win in m_Windows)
         {
             GUILayout.BeginHorizontal();
             if (win.window)
             {
                 win.changed = GUILayout.Toggle(win.changed, "", GUILayout.Width(16));
                 EditorGUILayout.ObjectField(win.window, typeof(EditorWindow), false, GUILayout.Width(200));
                 using (new EditorGUI.DisabledScope(!win.changed))
                 {
                     EditorGUI.BeginChangeCheck();
                     win.content.image = (Texture)EditorGUILayout.ObjectField(win.content.image, typeof(Texture), false, GUILayout.Width(32));
                     win.content.text = GUILayout.TextField(win.content.text);
                     if (EditorGUI.EndChangeCheck())
                     {
                         win.window.titleContent = win.content;
                         win.window.ShowTab(); 
                         win.window.Repaint();
                         ShowTab();
                     }
                 }
             }
             else
             {
                 GUILayout.Label("window has been closed");
             }
             if (GUILayout.Button("X", GUILayout.Width(30)))
             {
                 m_Windows.Remove(win);
                 m_EditorWindows.Remove(win.window);
                 GUIUtility.ExitGUI();
             }
             GUILayout.EndHorizontal();
         }
         GUILayout.EndScrollView();
     }

     private void SaveTabNames()
     {
         string saveContent = "";
         for (int i = 0; i < m_Windows.Count; i++)
         {
             if (i == m_Windows.Count - 1)
             {
                 saveContent += m_Windows[i].content.text ;
             }
             else
             {
                 saveContent += m_Windows[i].content.text+ "\n";
             }
         }
         //C:\Users\kilinc\AppData\LocalLow\DefaultCompany\EFT
         System.IO.File.WriteAllText(Application.persistentDataPath +"/editorTabSaves.text", saveContent);
     }

     private void LoadTabNames()
     {
         string loadContent = "";
         loadContent = System.IO.File.ReadAllText(Application.persistentDataPath + "/editorTabSaves.text");

         List<string> tabNameList = loadContent.Split("\n").ToList();
         for (int i = 0; i < tabNameList.Count; i++)
         {
             if (i < m_Windows.Count - 1)
             {
                 m_Windows[i].content.text = tabNameList[i];
                 m_Windows[i].window.titleContent =   m_Windows[i].content;
                 //m_Windows[i].content.text = tabNameList[i];
                 m_Windows[i].window.Repaint(); 
                 ShowTab();  
             }
           
         }
  
     }
 }