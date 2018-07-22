#if UNITY_EDITOR

namespace UWinForms.Unity.Views
{
    using System.Windows.Forms;
    using global::System;
    using UnityEditor;
    using UnityEngine;
    using Application = UnityEngine.Application;

    public class AppControlList : EditorWindow
    {
        private string _filter = "";
        private float _repaintWait;
        private Vector2 _scrollPosition;

        void Update()
        {
            if (_repaintWait < 1)
                _repaintWait += Time.deltaTime;
            else
            {
                Repaint();
                _repaintWait = 0;
            }
        }
        void OnGUI()
        {
            if (!Application.isPlaying) return;
            if (Control.uwfDefaultController == null)
            {
                GUILayout.Label("SWF.Control.DefaultController is null");
                return;
            }

            float width = position.width - 24;
            _scrollPosition = GUILayout.BeginScrollView(_scrollPosition);
            GUILayout.BeginVertical();
            _filter = GUILayout.TextField(_filter);
            GUILayout.Label("Modal Forms");

            for (int i = 0; i < Control.uwfDefaultController.ModalForms.Count; i++)
            {
                string c_type = Control.uwfDefaultController.ModalForms[i].GetType().ToString().Replace("System.Windows.Forms", "SWF");
                string c_name = Control.uwfDefaultController.ModalForms[i].Name;
                if (c_name == null) c_name = "";
                if (!String.IsNullOrEmpty(_filter))
                {
                    if (!c_type.ToLower().Contains(_filter.ToLower()) && !c_name.ToLower().Contains(_filter.ToLower()))
                        continue;
                }

                GUILayout.BeginHorizontal(GUILayout.Width(width));
                if (GUILayout.Button("...", GUILayout.Width(24)))
                {
                    var w = EditorMenu.ShowInspector();
                    (w as ControlInspector).DesignerObject = Control.uwfDefaultController.ModalForms[i];
                }
                GUILayout.Label(c_type, GUILayout.Width(160));
                GUILayout.Label(c_name, GUILayout.Width(220));
                GUILayout.EndHorizontal();
            }
            GUILayout.Space(24);
            GUILayout.Label("Forms");
            for (int i = 0; i < Control.uwfDefaultController.Forms.Count; i++)
            { 
                string c_type = Control.uwfDefaultController.Forms[i].GetType().ToString().Replace("System.Windows.Forms", "SWF");
                string c_name = Control.uwfDefaultController.Forms[i].Name;
                if (c_name == null) c_name = "";
                if (!String.IsNullOrEmpty(_filter))
                {
                    if (!c_type.ToLower().Contains(_filter.ToLower()) && !c_name.ToLower().Contains(_filter.ToLower()))
                        continue;
                }

                GUILayout.BeginHorizontal(GUILayout.Width(width));
                if (GUILayout.Button("...", GUILayout.Width(24)))
                {
                    var w = EditorMenu.ShowInspector();
                    (w as ControlInspector).DesignerObject = Control.uwfDefaultController.Forms[i];
                }
                GUILayout.Label(c_type, GUILayout.Width(160));
                GUILayout.Label(c_name, GUILayout.Width(220));
                GUILayout.EndHorizontal();
            }
            GUILayout.Space(24);
            GUILayout.Label("uwfContext");
            for (int i = 0; i < Control.uwfDefaultController.Contexts.Count; i++)
            {                   
                string c_type = Control.uwfDefaultController.Contexts[i].GetType().ToString().Replace("System.Windows.Forms", "SWF");
                string c_name = Control.uwfDefaultController.Contexts[i].Name;
                if (c_name == null) c_name = "";
                if (!String.IsNullOrEmpty(_filter))
                {
                    if (!c_type.ToLower().Contains(_filter.ToLower()) && !c_name.ToLower().Contains(_filter.ToLower()))
                        continue;
                }

                GUILayout.BeginHorizontal(GUILayout.Width(width));
                if (GUILayout.Button("...", GUILayout.Width(24)))
                {
                    var w = EditorMenu.ShowInspector();
                    (w as ControlInspector).DesignerObject = Control.uwfDefaultController.Contexts[i];
                }
                GUILayout.Label(c_type, GUILayout.Width(160));
                GUILayout.Label(c_name, GUILayout.Width(220));
                GUILayout.EndHorizontal();
            }
            GUILayout.Space(24);
            GUILayout.Label("Hovered Control");

            var hoveredControl = Control.uwfDefaultController.hoveredControl;
            if (hoveredControl != null)
            {
                string c_type = hoveredControl.GetType().ToString().Replace("System.Windows.Forms", "SWF");
                string c_name = hoveredControl.Name;
                if (c_name == null) c_name = "";

                GUILayout.BeginHorizontal(GUILayout.Width(width));
                if (GUILayout.Button("...", GUILayout.Width(24)))
                {
                    var w = EditorMenu.ShowInspector();
                    (w as ControlInspector).DesignerObject = hoveredControl;
                }
                GUILayout.Label(c_type, GUILayout.Width(160));
                GUILayout.Label(c_name, GUILayout.Width(220));
                GUILayout.EndHorizontal();
            }
            GUILayout.EndVertical();
            GUILayout.EndScrollView();
        }
    }
}

#endif