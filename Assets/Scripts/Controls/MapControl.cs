using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective.Control
{
    public class MapControl : MonoBehaviour
    {
        private bool moveMod;
        [SerializeField]
        private float moveTimeDelay = 0.1f;
        private float timeDelay;
        private Vector2 mousePosition;
        [SerializeField]
        private float deltaMousePosition;
        [SerializeField]
        private float sensitivity = 0.8f;
        [SerializeField]
        private float maxZoom = -10;
        [SerializeField]
        private float minZoom = -20;

        private Camera mainCamera;
        private List<Tier> tiers;

        private void Start()
        {
            mainCamera = Camera.main;
            tiers = MapManager.GetInstantiate().instanceRing.GetTiers();
        }

        void Update()
        {
            Game game = Game.GetInstantiate();
            MapManager mapManager = MapManager.GetInstantiate();
            if (game.GetGameState() == GameState.IN_GAME)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    moveMod = true;
                    mousePosition = Input.mousePosition;
                }
                else if (Input.GetMouseButtonUp(1))
                {
                    moveMod = false;
                    timeDelay = 0;
                }
                else if (Input.GetMouseButton(1))
                {
                    if (moveMod)
                    {
                        timeDelay += Time.deltaTime;
                        if(timeDelay > moveTimeDelay)
                        {
                            float deltaX = Input.mousePosition.x - mousePosition.x;
                            if (Mathf.Abs(deltaX) > deltaMousePosition)
                            {
                                foreach(Tier tier in tiers)
                                {
                                    tier.GetComponent<Transform>().localEulerAngles += new Vector3(0, deltaX*sensitivity, 0);
                                }
                            }
                            float deltaY = Input.mousePosition.y - mousePosition.y;
                            if (Mathf.Abs(deltaY) > deltaMousePosition)
                            {
                                mainCamera.transform.localPosition += new Vector3(0, 0, deltaY * sensitivity *0.1f);
                                if (mainCamera.transform.localPosition.z > maxZoom)
                                {
                                    mainCamera.transform.localPosition = new Vector3(mainCamera.transform.localPosition.x, mainCamera.transform.localPosition.y, maxZoom);
                                }
                                else if (mainCamera.transform.localPosition.z < minZoom)
                                {
                                    mainCamera.transform.localPosition = new Vector3(mainCamera.transform.localPosition.x, mainCamera.transform.localPosition.y, minZoom);
                                }
                            }
                            mousePosition = Input.mousePosition;
                        }
                    }
                }
                if (Input.mouseScrollDelta.y != 0)
                {
                    float delta = Input.mouseScrollDelta.y;
                    int index = mapManager.GetTierIndex(mapManager.curTier);
                    if (index > 0 && delta > 0)
                    {
                        mapManager.SetTier(--index);
                    }
                    else if (delta<0 && index < mapManager.ring.GetTiers().Count - 1)
                    {
                        mapManager.SetTier(++index);
                    }
                }
            }
        }
    }
}
