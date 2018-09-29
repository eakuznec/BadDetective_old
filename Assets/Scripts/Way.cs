using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective
{
    public class Way : MonoBehaviour
    {
        public List<iWayPlace> pointsAndPaths = new List<iWayPlace>();
        public float totalTime;
        public bool check;

        public void copyFrom(Way other)
        {
            foreach(iWayPlace place in other.pointsAndPaths)
            {
                pointsAndPaths.Add(place);
            }
            totalTime = other.totalTime;
        }
    }
}