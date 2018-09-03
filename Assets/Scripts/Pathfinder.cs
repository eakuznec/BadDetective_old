using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective
{
    public class Pathfinder : MonoBehaviour
    {
        private void Start()
        {
            MapManager mapManager = MapManager.GetInstantiate();
            ClearAllPaths();
            foreach (Path path in mapManager.instanceRing.paths)
            {
                path.points[0].avaliablePaths.Add(path);
                path.points[1].avaliablePaths.Add(path);
            }
        }

        public Way GetWay(WayType type, PointOnMap start, PointOnMap final)
        {
            ClearAllWays();
            Way retVal = null;
            bool end = false;
            List<PointOnMap> closePoint = new List<PointOnMap>();
            List<PointOnMap> openPoint = new List<PointOnMap>();
            openPoint.Add(start);
            while (!end)
            {
                foreach (PointOnMap point in openPoint)
                {
                    foreach (Path path in point.GetAvaliablePaths(type))
                    {
                        if (openPoint.Contains(path.points[0]) && !openPoint.Contains(path.points[1]) && !closePoint.Contains(path.points[1]))
                        {
                            closePoint.Add(path.points[1]);
                        }
                        else if (!openPoint.Contains(path.points[0]) && openPoint.Contains(path.points[1]) && !closePoint.Contains(path.points[0]))
                        {
                            closePoint.Add(path.points[0]);
                        }
                    }
                }
                if(closePoint.Count == 0)
                {
                    break;
                }
                PointOnMap minTimePoint = null;
                foreach (PointOnMap point in closePoint)
                {
                    if (!point.shortWay.check)
                    {
                        Path shortPath = null;
                        PointOnMap neaborPoint = null;
                        float shortTime = 0;
                        foreach (Path path in point.GetAvaliablePaths(type))
                        {
                            if (openPoint.Contains(path.points[0]))
                            {
                                if(shortPath == null || (shortTime > path.timeToWay + path.points[0].shortWay.totalTime))
                                {
                                    shortPath = path;
                                    neaborPoint = path.points[0];
                                    shortTime = path.timeToWay + path.points[0].shortWay.totalTime;
                                }
                            }
                            else if (openPoint.Contains(path.points[1]))
                            {
                                if (shortPath == null || (shortTime > path.timeToWay + path.points[1].shortWay.totalTime))
                                {
                                    shortPath = path;
                                    neaborPoint = path.points[1];
                                    shortTime = path.timeToWay + path.points[1].shortWay.totalTime;
                                }
                            }
                        }
                        point.shortWay.totalTime = shortTime;
                        foreach(iWayPlace pom in neaborPoint.shortWay.pointsAndPaths)
                        {
                            point.shortWay.pointsAndPaths.Add(pom);
                        }
                        point.shortWay.pointsAndPaths.Add(shortPath);
                        point.shortWay.pointsAndPaths.Add(point);
                        point.shortWay.check = true;
                    }
                    if(minTimePoint == null || minTimePoint.shortWay.totalTime > point.shortWay.totalTime)
                    {
                        minTimePoint = point;
                    }
                }
                if(minTimePoint == final)
                {
                    end = true;
                    retVal = minTimePoint.shortWay;
                }
                else
                {
                    openPoint.Add(minTimePoint);
                    closePoint.Remove(minTimePoint);
                }
            }
            return retVal;
        }

        private void ClearAllPaths()
        {
            PointOnMap[] allPoints = FindObjectsOfType<PointOnMap>();
            foreach (PointOnMap point in allPoints)
            {
                point.avaliablePaths.Clear();
            }
        }

        private void ClearAllWays()
        {
            PointOnMap[] allPoints = FindObjectsOfType<PointOnMap>();
            foreach (PointOnMap point in allPoints)
            {
                point.ResetShortWay();
            }
        }
    }
}