using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BadDetective.UI
{
    public abstract class FilePanel: MonoBehaviour
    {
        public void Open()
        {
            InterfaceManager.GetInstantiate().filesPanel.AddFile(this);
        }

        public void Close()
        {
            InterfaceManager.GetInstantiate().filesPanel.RemoveFile(this);
        }
    }
}
