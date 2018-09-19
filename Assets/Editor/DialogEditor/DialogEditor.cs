using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using BadDetective.Dialog;

public class DialogEditor : EditorWindow
{
    public static DialogEditor editor;
    public GameObject prefab;

    public static Dialog dialog;
    public string defaultName = "Dialog";
    public List<DialogPhraseNode> nodes = new List<DialogPhraseNode>();
    public List<DialogLinkNode> links = new List<DialogLinkNode>();
    public int id;

    private bool moveMod = false;
    public bool linkMod = false;
    public DialogLinkNode selectLink;

    public Vector2 mousePos;

    [MenuItem("Window/Dialog Editor")]
    public static void ShowEditor()
    {
        // Get existing open window or if none, make a new one:
        editor = (DialogEditor)EditorWindow.GetWindow(typeof(DialogEditor));
    }

    private void Awake()
    {
        if (dialog != null)
        {
            LoadDialog();
        }
    }

    void OnGUI()
    {
        Control();
        DrawLinks();
        DrawNodes();
        Repaint();
    }

    private void DrawLinks()
    {
        foreach (DialogLinkNode link in links)
        {
            link.DrawLink();
        }
    }

    private void DrawNodes()
    {
        if (dialog == null)
        {
            return;
        }
        EditorStyles.textField.wordWrap = true;

        BeginWindows();
        for (int i = 0; i < nodes.Count; i++)
        {
            if (dialog.startPhrase == null)
            {
                dialog.startPhrase = nodes[i].phrase;
            }
            Color baseColor = GUI.contentColor;
            if (nodes[i].phrase == dialog.startPhrase)
            {
                GUI.color = Color.cyan;
            }
            else
            {
                GUI.color = baseColor;

            }
            nodes[i].windowRect = GUILayout.Window(nodes[i].id, nodes[i].windowRect, WindowFunction, nodes[i].windowTitle);
        }
        for (int i = 0; i < links.Count; i++)
        {
            links[i].DrawWindow();
        }
        EndWindows();
    }

    void WindowFunction(int id)
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            if (nodes[i].id == id)
            {
                nodes[i].DrawWindow();
            }
        }
        GUI.DragWindow();
    }

    private void Control()
    {
        Event e = Event.current;
        mousePos = e.mousePosition;
        int select = -1;
        if (e.button == 0)
        {
            if (e.type == EventType.MouseDown)
            {
                for (int i = 0; i < nodes.Count; i++)
                {
                    if (nodes[i].windowRect.Contains(mousePos))
                    {
                        select = nodes[i].id;
                        Selection.activeGameObject = nodes[i].phrase.gameObject;
                        foreach(DialogChooseNode chooseNode in nodes[i].chooseNodes)
                        {
                            if (chooseNode.windowRect.Contains(mousePos))
                            {
                                Selection.activeGameObject = chooseNode.choose.gameObject;
                                break;
                            }
                        }
                        break;
                    }
                }
                if(select == -1)
                {
                    for(int i=0; i < links.Count; i++)
                    {
                        if (links[i].windowRect.Contains(mousePos))
                        {
                            Selection.activeGameObject = links[i].link.gameObject;
                            select = links[i].id;
                            break;
                        }
                    }
                }
                if (select == -1)
                {
                    Selection.activeGameObject = dialog.gameObject;
                    moveMod = true;
                }
            }
            else if (e.type == EventType.MouseDrag)
            {
                if (moveMod)
                {
                    for (int i = 0; i < nodes.Count; i++)
                    {
                        nodes[i].phrase.nodePosition.position += e.delta;
                    }
                }
            }
            else if (e.type == EventType.MouseUp)
            {
                for (int i = 0; i < nodes.Count; i++)
                {
                    if (nodes[i].windowRect.Contains(mousePos))
                    {
                        select = nodes[i].id;
                        break;
                    }
                }

                if (moveMod)
                {
                    moveMod = false;
                }
                if (linkMod)
                {
                    if (select != -1)
                    {
                        DialogPhraseNode dialogPhraseNode = null;
                        foreach (DialogPhraseNode node in nodes)
                        {
                            if (node.id == select)
                            {
                                dialogPhraseNode = node;
                                break;
                            }
                        }
                        selectLink.outputPhrase = dialogPhraseNode;
                        selectLink.link.output = dialogPhraseNode.phrase;
                        dialogPhraseNode.inputLinks.Add(selectLink);
                        linkMod = false;
                        Selection.activeGameObject = selectLink.link.gameObject;
                        selectLink = null;
                    }
                    else
                    {
                        selectLink.DeleteLink();
                        linkMod = false;
                        selectLink = null;
                    }
                }
            }
        }
        else if (e.button == 1)
        {
            if (e.type == EventType.MouseDown)
            {
                if (linkMod)
                {
                    selectLink.DeleteLink();
                    linkMod = false;
                    selectLink = null;
                }
                else
                {
                    for (int i = 0; i < nodes.Count; i++)
                    {
                        if (nodes[i].windowRect.Contains(mousePos))
                        {
                            select = nodes[i].id;
                            break;
                        }
                    }
                        if (select == -1)
                        {
                            for (int i = 0; i < links.Count; i++)
                            {
                                if (links[i].windowRect.Contains(mousePos))
                                {
                                select = links[i].id;
                                break;
                                }
                            }
                        }
                    if (select == -1)
                    {
                        GenericMenu menuWindow = new GenericMenu();
                        menuWindow.AddItem(new GUIContent("Create new phrase"), false, ContextCallback, "createPhrase");
                        menuWindow.AddSeparator("");
                        menuWindow.AddItem(new GUIContent("Save dialog"), false, ContextCallback, "saveDialog");
                        menuWindow.ShowAsContext();
                        e.Use();
                    }
                    else
                    {
                        foreach(DialogPhraseNode node in nodes)
                        {
                            if (node.windowRect.Contains(mousePos))
                            {
                                GenericMenu menuWindow = new GenericMenu();
                                if (node.id == select && node.chooseOpen)
                                {
                                    foreach (DialogChooseNode chooseNode in node.chooseNodes)
                                    {
                                        if (chooseNode.windowRect.Contains(mousePos))
                                        {
                                            menuWindow.AddItem(new GUIContent("Add link"), false, ContextCallback, string.Format("link_{0}_{1}", node.id, chooseNode.id));
                                            menuWindow.AddSeparator("");
                                            break;
                                        }
                                    }
                                }
                                menuWindow.AddItem(new GUIContent("Set started phrase"), false, ContextCallback, "startPhrase");
                                menuWindow.AddSeparator("");
                                menuWindow.AddItem(new GUIContent("Delete phrase"), false, ContextCallback, "deletePhrase");
                                menuWindow.ShowAsContext();
                                e.Use();
                                break;
                            }
                        }
                        foreach(DialogLinkNode link in links)
                        {
                            if (link.windowRect.Contains(mousePos))
                            {
                                GenericMenu menuWindow = new GenericMenu();
                                menuWindow.AddItem(new GUIContent("Delete link"), false, ContextCallback, "deleteLink");
                                menuWindow.ShowAsContext();
                                e.Use();
                                break;
                            }
                        }
                    }
                }
            }
        }
    }

    private void ContextCallback(object obj)
    {
        string clb = obj.ToString();

        if (clb == "createPhrase")
        {
            CreatePhrase(mousePos, null);
        }
        else if (clb == "startPhrase")
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i].windowRect.Contains(mousePos))
                {
                    dialog.startPhrase = nodes[i].phrase;
                    break;
                }
            }
        }
        else if (clb == "deletePhrase")
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i].windowRect.Contains(mousePos))
                {
                    nodes[i].DeleteNode();
                    break;
                }
            }
        }
        else if (clb.Contains("link_"))
        {
            string node = clb.Substring(clb.IndexOf("_") + 1);
            node = node.Substring(0, node.IndexOf("_"));
            int nodeId = -1;
            int.TryParse(node, out nodeId);
            string choose = clb.Substring(clb.LastIndexOf("_") + 1);
            int chooseId = -1;
            int.TryParse(choose, out chooseId);
            foreach (DialogPhraseNode phrase in nodes)
            {
                if (phrase.id == nodeId)
                {
                    foreach (DialogChooseNode chooseNode in phrase.chooseNodes)
                    {
                        if (chooseNode.id == chooseId)
                        {
                            chooseNode.CreateLinkNode(null);
                            break;
                        }
                    }
                }
            }
        }
        else if (clb.Contains("deleteLink"))
        {
            for(int i=0; i<links.Count; i++)
            {
                if (links[i].windowRect.Contains(mousePos))
                {
                    links[i].DeleteLink();
                    break;
                }
            }
        }
    }

    public DialogPhraseNode CreatePhrase(Vector2 position, DialogPhrase phrase)
    {
        DialogPhraseNode phraseNode = new DialogPhraseNode();
        phraseNode.id = id;
        id++;
        if (phrase != null)
        {
            phraseNode.phrase = phrase;
        }
        else
        {
            GameObject dialogPhrase = new GameObject(string.Format("{0}_DialogPhrase", dialog.dialogName));
            phraseNode.phrase = dialogPhrase.AddComponent<DialogPhrase>();
            dialog.phrases.Add(dialogPhrase.GetComponent<DialogPhrase>());
            dialogPhrase.transform.parent = dialog.transform;
        }
        phraseNode.windowRect = new Rect(position, new Vector2(400, 170));
        nodes.Add(phraseNode);
        if (dialog.startPhrase == null)
        {
            dialog.startPhrase = phraseNode.phrase;
        }
        Selection.activeGameObject = phraseNode.phrase.gameObject;
        return phraseNode;
    }

    private void ClearAll()
    {
        nodes.Clear();
        links.Clear();
        id = 0;
        moveMod = false;
        linkMod = false;
        selectLink = null;
    }

    public void LoadDialog()
    {
        ClearAll();
        BuildDialog();
        Selection.activeGameObject = dialog.gameObject;
    }

    public void BuildDialog()
    {
        List<DialogChooseNode> chooseNodes = new List<DialogChooseNode>();
        foreach (DialogPhrase phrase in dialog.phrases)
        {
            DialogPhraseNode phraseNode = CreatePhrase(phrase.nodePosition.position, phrase);
            foreach(DialogChoose choose in phrase.chooses)
            {
                DialogChooseNode chooseNode = phraseNode.CreateChooseNode(choose);
                chooseNodes.Add(chooseNode);
            }
        }
        foreach (DialogChooseNode chooseNode in chooseNodes)
        {
            foreach (DialogLink link in chooseNode.choose.links)
            {
                DialogLinkNode linkNode = chooseNode.CreateLinkNode(link);
                foreach (DialogPhraseNode phraseNode in nodes)
                {
                    if (phraseNode.phrase == link.output)
                    {
                        linkNode.outputPhrase = phraseNode;
                        phraseNode.inputLinks.Add(linkNode);
                        break;
                    }
                }
            }
        }

    }
}
