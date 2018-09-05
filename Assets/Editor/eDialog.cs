using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BadDetective.Dialog
{
    [CustomEditor(typeof(Dialog))]
    public class eDialog : Editor
    {
        public override void OnInspectorGUI()
        {
            Dialog dialog = (Dialog)target;
            if (GUILayout.Button("Edit"))
            {
                if (dialog != null)
                {
                    DialogEditor.Init();
                    if (eUtils.isPrefab(dialog))
                    {
                        DialogEditor.editor.prefab = dialog.gameObject;
                    }
                    else
                    {
                        DialogEditor.editor.prefab = ((Dialog)PrefabUtility.GetPrefabParent(dialog)).gameObject;
                    }
                    DialogEditor.editor.LoadDialog();
                }

            }
            base.OnInspectorGUI();
        }
    }
}
