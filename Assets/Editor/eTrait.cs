using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BadDetective
{
    [CustomEditor (typeof(Trait))]
    public class eTrait : Editor
    {
        public override void OnInspectorGUI()
        {
            Trait trait = (Trait)target;
            TraitManager traitManager = TraitManager.GetInstantiate();
            List<Trait> allTraits = traitManager.GetTraits();
            GUIStyle boldStyle = new GUIStyle();
            boldStyle.fontStyle = FontStyle.Bold;
            if (!allTraits.Contains(trait))
            {
                if (PrefabUtility.GetPrefabType(trait) == PrefabType.Prefab)
                {
                    if (GUILayout.Button("!!! Registrate !!!"))
                    {
                        traitManager.Registrate(trait);
                    }
                }
                else
                {
                    EditorGUILayout.LabelField("---Обратитесь к префабу для регистрации в Game!---");
                }
                GUILayout.Space(10);
            }
            base.OnInspectorGUI();
            if(trait.type == TraitType.REMOVABLE)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("removePoint"));
            }
            else if(trait.type == TraitType.TEMPORARY)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("liveTime"));
            }
            GUILayout.Space(10);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("isHidden"));
            if (trait.isHidden)
            {
                List<string> options = new List<string>();
                options.Add("NULL");
                foreach(Trait t in allTraits)
                {
                    options.Add(t.traitName);
                }
                int index = EditorGUILayout.Popup(allTraits.IndexOf(trait.mimicryTrait) + 1, options.ToArray());
                if(index == 0)
                {
                    trait.mimicryTrait = null;
                }
                else
                {
                    trait.mimicryTrait = allTraits[index - 1];
                }
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}
