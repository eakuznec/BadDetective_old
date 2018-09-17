using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using BadDetective.Dialog;

public class DialogPhraseNode : ScriptableObject
{
    public DialogPhrase phrase;
    public int id;
    //public Rect windowRect;
    public string windowTitle;
    public bool chooseOpen;
    public List<DialogChooseNode> chooseNodes = new List<DialogChooseNode>();
    public List<DialogLinkNode> inputLinks = new List<DialogLinkNode>();

    public Rect windowRect
    {
        get
        {
            return phrase.nodePosition;
        }
        set
        {
            phrase.nodePosition = value;
        }
    }

    public void DrawWindow()
    {
        phrase.nodePosition.height = 170;

        if (phrase.type== PhraseType.DIALOG_PHRASE)
        {
            windowTitle = "Dialog phrase";
        }
        else if (phrase.type == PhraseType.REPORT)
        {
            windowTitle = "File notes";
        }
        SerializedObject soPhrase = new SerializedObject(phrase);
        EditorGUILayout.PropertyField(soPhrase.FindProperty("type"), GUIContent.none, new GUILayoutOption[] { GUILayout.ExpandWidth(true) });
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
        if(phrase.type == PhraseType.DIALOG_PHRASE)
        {
            GUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(soPhrase.FindProperty("speekerType"), GUIContent.none, new GUILayoutOption[] { GUILayout.Width(100) });
            if (phrase.speekerType == DialogSpeekerType.SPECIFIC)
            {
                GUILayout.FlexibleSpace();
                EditorGUILayout.PropertyField(soPhrase.FindProperty("speeker"), GUIContent.none, new GUILayoutOption[] { GUILayout.Width(150) });
            }
            GUILayout.EndHorizontal();
        }
        if(phrase.type != PhraseType.REPORT)
        {
            phrase.phraseText = EditorGUILayout.TextArea(phrase.phraseText, new GUILayoutOption[] { GUILayout.ExpandHeight(true), GUILayout.Width(250) });
        }
        else
        {
            EditorGUILayout.LabelField("", new GUILayoutOption[] { GUILayout.ExpandHeight(true), GUILayout.Width(250) });
        }
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
                chooseNodes[i].DrawWindow(ref i, ref phrase.nodePosition, ref h);
            }
            phrase.nodePosition.height += 24;
            if (GUILayout.Button("Add choose"))
            {
                EditorGUI.FocusTextInControl("");
                CreateChooseNode(null);
            }
        }
        soPhrase.ApplyModifiedProperties();
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
        Dialog dialog = DialogEditor.dialog;
        dialog.phrases.Remove(phrase);
        for(int i=0; i < inputLinks.Count; i++)
        {
            inputLinks[i].DeleteLink();
            i--;
        }
        for(int i=0; i< chooseNodes.Count; i++)
        {
            chooseNodes[i].DeleteNode(ref i);
        }
        if(dialog.startPhrase == phrase)
        {
            dialog.startPhrase = null;
        }
        DestroyImmediate(phrase.gameObject);
        editor.nodes.Remove(this);
    }
}
