﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using BadDetective.Dialog;

public class DialogChooseNode : ScriptableObject
{
    public DialogChoose choose;
    public int id;
    public Rect windowRect;
    public List<DialogLinkNode> outputLinks = new List<DialogLinkNode>();

    public DialogPhraseNode parentNode;

    public void DrawWindow(ref int num, ref Rect rect, ref float h)
    {
        DialogPhrase phrase = parentNode.phrase;
        windowRect = new Rect(rect.x, rect.y + h +82, rect.width, 70);
        GUILayout.BeginVertical();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Del", new GUILayoutOption[] { GUILayout.Width(45), GUILayout.Height(70) }))
        {
            EditorGUI.FocusTextInControl("");
            DeleteNode(ref num);
        }
        GUILayout.BeginVertical(new GUILayoutOption[] { GUILayout.Width(45) });
        GUILayout.FlexibleSpace();
        if (num > 0)
        {
            if (GUILayout.Button("Up"))
            {
                EditorGUI.FocusTextInControl("");
                phrase.chooses.Insert(num - 1, choose);
                phrase.chooses.RemoveAt(num + 1);
                parentNode.chooseNodes.Insert(num - 1, this);
                parentNode.chooseNodes.RemoveAt(num + 1);
            }
        }
        if (num < phrase.chooses.Count - 1)
        {
            if (GUILayout.Button("Down"))
        {
            EditorGUI.FocusTextInControl("");
                phrase.chooses.Insert(num + 2, choose);
                phrase.chooses.RemoveAt(num);
                parentNode.chooseNodes.Insert(num + 2, this);
                parentNode.chooseNodes.RemoveAt(num);
            }
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndVertical();
        GUILayout.BeginVertical();
        if (choose.next)
        {
            GUILayout.Label("Continue", new GUILayoutOption[] { GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true) });
            GUILayout.FlexibleSpace();
        }
        else
        {
            choose.chooseText = EditorGUILayout.TextArea(choose.chooseText, new GUILayoutOption[] { GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true) });
        }
        GUILayout.BeginHorizontal();
        GUILayout.Label(string.Format("Conditions: {0}", choose.conditions.Count));
        GUILayout.Label(string.Format("Effects: {0}", choose.effects.Count));
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
        choose.next = GUILayout.Toggle(choose.next, GUIContent.none, new GUILayoutOption[] { GUILayout.Width(20) });
        GUILayout.EndHorizontal();

        GUILayout.EndVertical();
        rect.height += windowRect.height;
        h += windowRect.height;
    }

    public void DeleteNode(ref int num)
    {
        for (int i = 0; i < outputLinks.Count; i++)
        {
            outputLinks[i].DeleteLink();
            i--;
        }
        parentNode.phrase.chooses.Remove(choose);
        DestroyImmediate(choose.gameObject);
        parentNode.chooseNodes.Remove(this);
        num--;
    }

    public DialogLinkNode CreateLinkNode(DialogLink link)
    {
        DialogLinkNode linkNode = new DialogLinkNode();
        GameObject goLink = new GameObject("Link");
        goLink.transform.parent = choose.transform;
        if (link != null)
        {
            linkNode.link = link;
            linkNode.windowRect = link.nodePosition;
        }
        else
        {
            DialogLink newLink = goLink.AddComponent<DialogLink>();
            choose.links.Add(newLink);
            linkNode.link = newLink;
            newLink.input = choose;
            Selection.activeGameObject = newLink.gameObject;
            DialogEditor.editor.selectLink = linkNode;
            DialogEditor.editor.linkMod = true;
        }
        outputLinks.Add(linkNode);
        linkNode.parentChoose = this;
        linkNode.parentPhrase = parentNode;
        linkNode.id = DialogEditor.editor.id;
        DialogEditor.editor.id++;
        DialogEditor.editor.links.Add(linkNode);
        return linkNode;
    }
}
