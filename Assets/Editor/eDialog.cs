using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BadDetective.Dialog
{
    [CustomEditor(typeof(Dialog))]
    public class eDialog : Editor
    {
        public bool showDialogStates;

        public override void OnInspectorGUI()
        {
            Dialog dialog = (Dialog)target;
            if (eUtils.isPrefab(dialog))
            {
                EditorGUILayout.LabelField("---Для редактирования вытащите диалог на сцену!---");
            }
            else
            {
                if (GUILayout.Button("Edit"))
                {
                    if (dialog != null)
                    {
                        if (DialogEditor.editor == null)
                        {
                            DialogEditor.ShowEditor();
                        }
                        DialogEditor.dialog = dialog;
                        DialogEditor.editor.LoadDialog();
                    }

                }
            }
            base.OnInspectorGUI();
            GUILayout.Space(10);
            eUtils.DrawQuestStateList(dialog.dialogStates, dialog.transform, ref showDialogStates, "Dialog States");
        }
    }
}
