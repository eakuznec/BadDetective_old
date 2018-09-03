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
            for (int i=0; i< logicMap.owners.Count; i++)
            {
                iLogicMapContainer owner = logicMap.owners[i];
                if(owner == null)
                {
                    logicMap.owners.RemoveAt(i);
                    i--;
                }
                else
                {
                    //if (owner is TriggerZone)
                    //{
                    //    try
                    //    {
                    //        GUILayout.Label(((TriggerZone)owner).gameObject.name);
                    //    }
                    //    catch
                    //    {
                    //        logicMap.owners.RemoveAt(i);
                    //        i--;
                    //    }
                    //}
                    //else if (owner is InteractionObject)
                    //{
                    //    try
                    //    {
                    //        GUILayout.Label(((InteractionObject)owner).gameObject.name);
                    //    }
                    //    catch
                    //    {
                    //        logicMap.owners.RemoveAt(i);
                    //        i--;
                    //    }
                    //}
                    //else if (owner is Location)
                    //{
                    //    try
                    //    {
                    //        GUILayout.Label(((Location)owner).gameObject.name);
                    //    }
                    //    catch
                    //    {
                    //        logicMap.owners.RemoveAt(i);
                    //        i--;
                    //    }
                    //}
                    //else if (owner is Encounter)
                    //{
                    //    try
                    //    {
                    //        GUILayout.Label(((Encounter)owner).gameObject.name);
                    //    }
                    //    catch
                    //    {
                    //        logicMap.owners.RemoveAt(i);
                    //        i--;
                    //    }
                    //}
                    //else if (owner is NPC)
                    //{
                    //    try
                    //    {
                    //        GUILayout.Label(((NPC)owner).gameObject.name);
                    //    }
                    //    catch
                    //    {
                    //        logicMap.owners.RemoveAt(i);
                    //        i--;
                    //    }
                    //}
                }
            }
            if (eUtils.isPrefab(logicMap))
            {
                GUILayout.Label("---Для редактирования вынесите на сцену!!!---");
                GUILayout.Space(5);
            }
            else
            {
                if (GUILayout.Button("Edit"))
                {
                    if (LogicMapEditor.editor != null)
                    {
                        LogicMapEditor.editor.Close();
                    }
                    LogicMapEditor.logicMap = logicMap;
                    LogicMapEditor.ShowEditor();
                }
            }
            base.OnInspectorGUI();
        }
    }
}