using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BadDetective
{
    [CustomEditor(typeof(TraitContainer))]
    public class eTraitContainer : Editor
    {
        public override void OnInspectorGUI()
        {
            TraitContainer traitContainer = (TraitContainer)target;
            base.OnInspectorGUI();
            TraitManager traitManager = TraitManager.GetInstantiate();
            List<Trait> allTraits = traitManager.GetTraits();
            GUILayout.Space(10);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("isHidden"));
            if (traitContainer.isHidden)
            {
                List<string> options = new List<string>();
                options.Add("NULL");
                foreach (Trait t in allTraits)
                {
                    options.Add(t.traitName);
                }
                int index = EditorGUILayout.Popup(allTraits.IndexOf(traitContainer.mimicryTrait) + 1, options.ToArray());
                if (index == 0)
                {
                    traitContainer.mimicryTrait = null;
                }
                else
                {
                    traitContainer.mimicryTrait = allTraits[index - 1];
                }
            }
            serializedObject.ApplyModifiedProperties();
        }

    }
}