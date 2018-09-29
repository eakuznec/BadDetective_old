using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BadDetective
{
    [CustomEditor(typeof(Game))]
    public class eGame : Editor
    {
        public override void OnInspectorGUI()
        {
            Game game = (Game)target;
            GUILayout.Label(game.GetGameState().ToString());
            base.OnInspectorGUI();
        }
    }
}