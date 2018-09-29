using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective
{
    public class MapManager : MonoBehaviour
    {
        private static MapManager instance;
        [HideInInspector]
        public Pathfinder pathfinder;
        public Ring ring;
        [HideInInspector]
        public Ring instanceRing;
        public Tier curTier;
        public float deltaPosTier = 0.1f;
        public float timePosTier = 0.5f;
        public float pointLabelTime = 3f;
        public float pointRadius = 0.2f;

        [Header("Labels of Point")]
        public Sprite officeSprite;
        public Sprite overOfficeSprite;
        public Sprite homeSprite;
        public Sprite overHomeSprite;
        public Sprite eventSprite;
        public Sprite eventOverSprite;

        public static MapManager GetInstantiate()
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MapManager>();
            }
            if (instance == null)
            {
                Game game = Game.GetInstantiate();
                instance = Instantiate(game.mapManager, game.transform);
            }
            return instance;
        }

        private void Awake()
        {
            GetInstantiate();

            instanceRing = FindObjectOfType<Ring>();
            if(instanceRing == null)
            {
                instanceRing = Instantiate(ring);
            }
            //pathfinder
            if (pathfinder == null)
            {
                pathfinder = FindObjectOfType<Pathfinder>();
            }
            if (pathfinder == null)
            {
                pathfinder = instance.gameObject.AddComponent<Pathfinder>();
            }
        }

        public void SetTier(int index)
        {
            List<Tier> tiers = instanceRing.GetTiers();
            if (index < tiers.Count)
            {
                for (int i=0; i<tiers.Count; i++)
                {
                    float pos = 0;
                    if (i > index)
                    {
                        pos = (index-i);
                    }
                    else if (i<index)
                    {
                        pos = 6.5f + 0.5f * (index - i);
                    }
                    tiers[i].SetPosition(pos);
                }
                curTier = tiers[index];
            }
        }
        
        public int GetTierIndex(Tier tier)
        {
            return instanceRing.GetTiers().IndexOf(tier);
        }

        public int GetTierIndex(PointOnMap point)
        {
            List<Tier> tiers = instanceRing.GetTiers();
            int tierIndex = -1;
            for (int i = 0; i < tiers.Count; i++)
            {
                List<PointOnMap> points = tiers[i].GetPoints();
                for (int j = 0; j < points.Count; j++)
                {
                    if (points[j] == point)
                    {
                        tierIndex = i;
                        break;
                    }
                }
            }
            return tierIndex;
        }
    }
}
