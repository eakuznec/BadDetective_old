using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BadDetective.LogicMap
{
    [CustomEditor (typeof(LogicMap))]
    public class eLogicMap : Editor
    {
        public override void OnInspectorGUI()
        {
            LogicMap logicMap = target as LogicMap;
            if (eUtils.isPrefab(logicMap))
            {
                GUILayout.Label("---Для редактирования вынесите на сцену!!!---");
                GUILayout.Space(5);
            }
            else
            {
                if (GUILayout.Button("Edit"))
                {
                    LogicMapEditor.logicMap = logicMap;
                    if (LogicMapEditor.editor == null)
                    {
                        LogicMapEditor.ShowEditor();
                    }
                    else
                    {
                        LogicMapEditor.editor.LoadLogicMap();
                    }
                }
            }
            base.OnInspectorGUI();
        }
    }
}