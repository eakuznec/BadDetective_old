using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BadDetective.Dialog
{
    public class DialogLinkNode : ScriptableObject
    {
        public DialogLink link;
        //public Rect windowRect;
        public DialogChooseNode parentChoose;
        public DialogPhraseNode parentPhrase;
        public DialogPhraseNode outputPhrase;
        Vector3 startPos;
        public int id;

        public Rect windowRect
        {
            get
            {
                return link.nodePosition;
            }
            set
            {
                link.nodePosition = value;
            }
        }

        public void DrawWindow()
        {
            windowRect = new Rect(startPos.x, startPos.y-10, 20, 20);
            GUIStyle currentStyle = new GUIStyle(GUI.skin.box);
            currentStyle.normal.background = eUtils.MakeTex((int)windowRect.width, (int)windowRect.height, Color.yellow);
            if (Selection.activeGameObject != link.gameObject)
            {
                currentStyle.normal.background = eUtils.MakeTex((int)windowRect.width, (int)windowRect.height, Color.gray);
            }
            GUI.Box(windowRect, string.Format("{0}", link.conditions.Count), currentStyle);
        }

        public void DrawLink()
        {
            if (parentPhrase.chooseOpen)
            {
                float h = parentChoose.windowRect.y + (parentChoose.windowRect.height / (parentChoose.choose.links.Count+1) * (parentChoose.choose.links.IndexOf(link) + 1));
                startPos = new Vector2(parentChoose.windowRect.xMax, h);
            }
            else
            {
                int linkCount = 0;
                int linkIndex = 0;
                foreach(DialogChoose choose in parentPhrase.phrase.chooses)
                {
                    foreach(DialogLink chooseLink in choose.links)
                    {
                        linkCount++;
                        if(chooseLink == link)
                        {
                            linkIndex = linkCount;
                        }
                    }
                }
                float h = parentPhrase.windowRect.y + (parentPhrase.windowRect.height / (linkCount+1) * linkIndex);
                startPos = new Vector2(parentPhrase.windowRect.xMax, h);
            }
            Vector3 endPos;
            if(outputPhrase != null)
            {
                int linkCount = outputPhrase.inputLinks.Count;
                int linkIndex = outputPhrase.inputLinks.IndexOf(this);
                float h = outputPhrase.windowRect.y + outputPhrase.windowRect.height / (linkCount + 1) * (linkIndex + 1);
                endPos = new Vector2(outputPhrase.windowRect.x, h);
            }
            else if(DialogEditor.editor.linkMod)
            {
                endPos = DialogEditor.editor.mousePos;
            }
            else
            {
                endPos = new Vector3();
            }
            Vector3 startTan = startPos + Vector3.right * 50;
            Vector3 endTan = endPos + Vector3.left * 50;
            Color color = Color.black;
            if (startPos.x > endPos.x)
            {
                color = Color.grey;
            }
            if (Selection.activeGameObject == link.gameObject)
            {
                color = Color.yellow;
            }
            Handles.DrawBezier(startPos, endPos, startTan, endTan, color, null, 3);
        }

        public void DeleteLink()
        {
            parentChoose.choose.links.Remove(link);
            if (outputPhrase != null)
            {
                outputPhrase.inputLinks.Remove(this);
            }
            DialogEditor.editor.links.Remove(this);
            link.Delete();
        }
    }
}
