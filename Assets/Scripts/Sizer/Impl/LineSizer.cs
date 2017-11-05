using HoloMeasurement.AppManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using HoloMeasurement.Figure;

namespace HoloMeasurement.Sizer.Impl
{
    public class LineSizer : MonoBehaviour, IPointSettable
    {
        private ReactiveCollection<Point> _pointList = new ReactiveCollection<Point>();

        private void Start()
        {
            _pointList
                .ObserveAdd()
                .Where(_ =>
                {
                    return _pointList.Count % 2 == 0 ? true : false;
                })
                .Subscribe(_ =>
                {
                    var index = _pointList.Count - 1;
                    var lastPoint = _pointList[index];
                    var previoudPoint = _pointList[index - 1];
                    LineGenerate(lastPoint, previoudPoint);
                })
                .AddTo(gameObject);
        }

        public void SetPoint(GameObject prefab, Vector3 position)
        {
            var go = Instantiate(prefab, position, Quaternion.identity);
            var point = go.GetComponent<Point>();
            point.SetPosition(position);
            _pointList.Add(point);
        }

        private void LineGenerate(Point last, Point previous)
        {
            Debug.Log("Generate Line"); 
        }
    }
}