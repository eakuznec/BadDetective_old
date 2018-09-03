using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using BadDetective.Dialog;

public class DialogPhraseNode : ScriptableObject
{
    public DialogPhrase phrase;
    public int id;
    public Rect windowRect;
    public string windowTitle;
    public bool chooseOpen;
    public List<DialogChooseNode> chooseNodes = new List<DialogChooseNode>();
    public List<DialogLinkNode> inputLinks = new List<DialogLinkNode>();

    public void DrawWindow()
    {
        windowRect.height = 150;

        if (phrase.speeker== null)
        {
            windowTitle = "Empty phrase";
        }
        else
        {
            windowTitle = phrase.speeker.characterName;
        }
        GUILayout.BeginHorizontal();
        GUILayout.BeginVertical(new GUILayoutOption[] { GUILayout.Width(100) });
        if (phrase.speeker != null)
        {
            if (phrase.speeker.characterAvatar != null)
            {
                GUILayout.Label(phrase.speeker.characterAvatar.texture, new GUILayoutOption[] { GUILayout.Width(100), GUILayout.Height(100) });
            }
            else
            {
                GUILayout.Label("", new GUILayoutOption[] { GUILayout.Width(100), GUILayout.Height(100) });
            }
        }
        else
        {
            GUILayout.Label("", new GUILayoutOption[] { GUILayout.Width(100), GUILayout.Height(100) });
        }
        GUILayout.EndVertical();
        GUILayout.BeginVertical();
        GUILayout.BeginHorizontal();
        SerializedObject soPhrase = new SerializedObject(phrase);
        EditorGUILayout.PropertyField(soPhrase.FindProperty("speekerType"), GUIContent.none, new GUILayoutOption[] { GUILayout.Width(100)});
        if(phrase.speekerType== DialogSpeekerType.SPECIFIC)
        {
            GUILayout.FlexibleSpace();
            EditorGUILayout.PropertyField(soPhrase.FindProperty("speeker"), GUIContent.none, new GUILayoutOption[] { GUILayout.Width(150) });
        }
        soPhrase.ApplyModifiedProperties();
        GUILayout.EndHorizontal();
        phrase.phraseText = EditorGUILayout.TextArea(phrase.phraseText, new GUILayoutOption[] { GUILayout.ExpandHeight(true), GUILayout.Width(250) });
        GUILayout.Label(string.Format("Effects: {0}", phrase.effects.Count));
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();
        if (GUILayout.Button(string.Format("Chooses ({0})", phrase.chooses.Count)))
        {
            EditorGUI.FocusTextInControl("");
            chooseOpen = !chooseOpen;
        }
        if (chooseOpen)
        {
            float h = 70;
            for (int i = 0; i < chooseNodes.Count; i++)
            {
                chooseNodes[i].DrawWindow(ref i, ref windowRect, ref h);
            }
            windowRect.height += 24;
            if (GUILayout.Button("Add choose"))
            {
                EditorGUI.FocusTextInControl("");
                CreateChooseNode(null);
            }
        }

    }

    public DialogChooseNode CreateChooseNode(DialogChoose choose)
    {
        DialogChooseNode chooseNode = new DialogChooseNode();
        if (choose != null)
        {
            chooseNode.choose = choose;
            chooseNode.windowRect = choose.nodePosition;
        }
        else
        {
            GameObject goChoose = new GameObject("Choose");
            goChoose.transform.parent = phrase.transform;
            DialogChoose newChoose = goChoose.AddComponent<DialogChoose>();
            phrase.chooses.Add(newChoose);
            chooseNode.choose = newChoose;
        }
        chooseNode.id = DialogEditor.editor.id;
        DialogEditor.editor.id++;
        chooseNode.parentNode = this;
        chooseNodes.Add(chooseNode);
        Selection.activeGameObject = chooseNode.choose.gameObject;
        return chooseNode;
    }

    public void DeleteNode()
    {
        DialogEditor editor = DialogEditor.editor;
        editor.dialog.phrases.Remove(phrase);
        for(int i=0; i < inputLinks.Count; i++)
        {
            inputLinks[i].DeleteLink();
            i--;
        }
        for(int i=0; i< chooseNodes.Count; i++)
        {
            chooseNodes[i].DeleteNode(ref i);
        }
        if(editor.dialog.startPhrase == phrase)
        {
            editor.dialog.startPhrase = null;
        }
        DestroyImmediate(phrase.gameObject);
        editor.nodes.Remove(this);
    }
}
