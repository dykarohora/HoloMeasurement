using HoloMeasurement.Figure;
using HoloMeasurement.Figure.Impl;
using HoloMeasurement.UserOperation;
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace HoloMeasurement.Sizer.Impl
{
    public class PolygonSizer : BaseSizer, IPolygonClosable
    {
        private List<GameObject> _lineList = new List<GameObject>();

        protected override void OnStart()
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

            _pointList
                .ObserveAdd()
                .Subscribe(eventArgs =>
                {
                    var point = eventArgs.Value;
                    point.Position
                    .Subscribe(movedPointPos => {
                        Vector3 centerPos;
                        Vector3 direction;
                        float distance;

                        if(_pointList.Count >= 2)
                        {
                            var index = _pointList.IndexOf(point);
                            if(index != 0)
                            {
                                var previousPointPos = _pointList[index - 1].Position.Value;
                                centerPos = (movedPointPos + previousPointPos) * 0.5f;
                                direction = movedPointPos - previousPointPos;
                                distance = Vector3.Distance(movedPointPos, previousPointPos);

                                var previousLine = _lineList[index - 1];
                                previousLine.transform.position = centerPos;
                                previousLine.transform.rotation = Quaternion.LookRotation(direction);
                                previousLine.transform.localScale = new Vector3(0.005f, 0.005f, distance);
                            }
                            if(index != _pointList.Count - 1)
                            {
                                var nextPointPos = _pointList[index + 1].Position.Value;
                                centerPos = (movedPointPos + nextPointPos) * 0.5f;
                                direction = nextPointPos - movedPointPos;
                                distance = Vector3.Distance(movedPointPos, nextPointPos);

                                var nextLine = _lineList[index];
                                nextLine.transform.position = centerPos;
                                nextLine.transform.rotation = Quaternion.LookRotation(direction);
                                nextLine.transform.localScale = new Vector3(0.005f, 0.005f, distance);
                            }
                        }
                    });
                })
                .AddTo(gameObject);
        }

        public override void SetPoint(GameObject prefab, Vector3 position)
        {
            base.SetPoint(prefab, position);
        }

        public void ClosePolygon()
        {
            if (_pointList.Count >= 3)
                CreateAggregationObject();
        }

        private void CreateAggregationObject()
        {
            var root = new GameObject();
            root.name = "Polygon";

            var lastLine = GenerateLine(_pointList[_pointList.Count - 1], _pointList[0]);
            _lineList.Add(lastLine);

            foreach (var line in _lineList)
            {
                line.transform.parent = root.transform;
            }

            foreach (var point in _pointList)
            {
                point.transform.parent = root.transform;
            }

            var polygonComponent = root.AddComponent<Polygon>();
            polygonComponent.Initialize(_pointList, _lineList);
            _pointList.Clear();
            _lineList.Clear();
        }

        protected override void WindUpHalfwayObj()
        {
            if (_pointList.Count >= 3)
            {
                CreateAggregationObject();
            } else
            {
                foreach (var point in _pointList)
                    Destroy(point.gameObject);

                foreach (var line in _lineList)
                    Destroy(line.gameObject);

                _pointList.Clear();
                _lineList.Clear();
            }
        }
    }
}
