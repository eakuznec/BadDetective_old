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
            serializedObject.ApplyModifiedProperties();
        }
    }
}
