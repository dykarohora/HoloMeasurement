using HoloMeasurement.Figure;
using HoloMeasurement.UserOperation;
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace HoloMeasurement.Sizer.Impl
{
    public class PolygonSizer : BaseSizer
    {
        private List<GameObject> _lineList;

        private void Start()
        {
            _pointList
                .ObserveAdd()
                .Where(_ => _pointList.Count >= 2)
                .Subscribe(eventArg =>
                {
                    var lastPoint = eventArg.Value;
                    var previousPoint = _pointList[eventArg.Index - 1];
                    var line = GenerateLine(previousPoint, lastPoint);
                    _lineList.Add(line);
                })
                .AddTo(gameObject);
        }

        public override void SetPoint(GameObject prefab, Vector3 position)
        {
            base.SetPoint(prefab, position);
        }

        private void CreateAggregationObject()
        {
            var root = new GameObject();
            root.name = "Polygon";

            var lastLine = GenerateLine(_pointList[_pointList.Count - 1], _pointList[0]);

            foreach (var line in _lineList)
            {
                line.transform.parent = root.transform;
            }

            foreach (var point in _pointList)
            {
                point.transform.parent = root.transform;
            }
        }
    }
}
