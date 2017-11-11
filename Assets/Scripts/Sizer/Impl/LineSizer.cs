using UnityEngine;
using UniRx;
using HoloMeasurement.Figure;
using HoloMeasurement.Figure.Impl;
using HoloMeasurement.UserOperation;

namespace HoloMeasurement.Sizer.Impl
{
    public class LineSizer : BaseSizer
    {
        protected override void OnStart()
        {
            _pointList
                .ObserveAdd()
                .Where(_ => _pointList.Count == 2)
                .Subscribe(_ =>
                {
                    var previousPoint = _pointList[0];
                    var lastPoint = _pointList[1];
                    var line = GenerateLine(previousPoint, lastPoint);
                    CreateAggregationObject(lastPoint, previousPoint, line);
                    _pointList.Clear();
                })
                .AddTo(gameObject);
        }

        public override void SetPoint(GameObject prefab, Vector3 position)
        {
            base.SetPoint(prefab, position);
        }

        private void CreateAggregationObject(Point lastPoint, Point previousPoint, GameObject line)
        {
            var root = new GameObject();
            root.name = "Line";
            lastPoint.transform.parent = root.transform;
            previousPoint.transform.parent = root.transform;
            line.transform.parent = root.transform;

            var lineComponent = root.AddComponent<Line>();
            lineComponent.Initialize(previousPoint, lastPoint, line);
        }

        protected override void WindUpHalfwayObj()
        {
            foreach(var point in _pointList)
                Destroy(point.gameObject);

            _pointList.Clear();
        }
    }
}