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
    }
}