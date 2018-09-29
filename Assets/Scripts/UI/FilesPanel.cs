using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BadDetective.UI
{
    public class FilesPanel : MonoBehaviour
    {
        private GameState prevGameState;
        List<FilePanel> files = new List<FilePanel>();

        public void AddFile(FilePanel file)
        {
            Game game = Game.GetInstantiate();
            files.Add(file);
            if(files[0] == file)
            {
                files[0] = Instantiate(files[0], transform);
                if(game.GetGameState() != GameState.IN_FILES_PANEL)
                {
                    prevGameState = game.GetGameState();
                    game.ChangeGameState(GameState.IN_FILES_PANEL);
                }
            }
        }

        public void RemoveFile(FilePanel file)
        {
            if (files.Contains(file))
            {
                Game game = Game.GetInstantiate();
                if(files[0] == file)
                {
                    Destroy(files[0].gameObject);
                    files.Remove(file);
                    if (files.Count > 0)
                    {
                        files[0] = Instantiate(files[0], transform);
                    }
                    else
                    {
                        game.ChangeGameState(prevGameState);
                    }
                }
                else
                {
                    files.Remove(file);
                }
            }
        }
    }
}
