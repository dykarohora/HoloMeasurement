using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UniRx;

namespace HoloMeasurement.Figure.Impl
{
    public class Polygon : BaseFigure
    {
        private List<Point> _pointList;
        private List<GameObject> _lineList;

        private bool _isInitialize = false;

        public void Initialize(IList<Point> pointList, IList<GameObject> lineList)
        {
            if (!_isInitialize)
            {
                _isInitialize = true;
                _pointList = new List<Point>(pointList);
                _lineList = new List<GameObject>(lineList);

                foreach(var point in _pointList)
                {
                    point.Position
                        .Subscribe(_ => ReculcPolygon(point))
                        .AddTo(gameObject);
                }
            }
        }

        public override void DeleteFigure()
        {
            foreach (var point in _pointList)
                Destroy(point);

            foreach (var line in _lineList)
                Destroy(line);

            Destroy(gameObject);
        }

        private void ReculcPolygon(Point movedPoint)
        {
            // 同じオブジェクトであることが重要なので等値演算子で判断
            var index = _pointList.FindIndex(pointElem => pointElem == movedPoint);
            var previousIndex = (index - 1 < 0) ? _pointList.Count - 1 : index - 1;
            var nextIndex = (index + 1 >= _pointList.Count) ? 0 : index + 1;
            // 前後のポイントを拾ってくる
            var movedPointPos = _pointList[index].Position.Value;
            var previousPointPos = _pointList[previousIndex].Position.Value;
            var nextPointPos = _pointList[nextIndex].Position.Value;
            // 前ポイントとのラインを修正する
            var centerPos = (movedPointPos + previousPointPos) * 0.5f;
            var direction = movedPointPos - previousPointPos;
            var distance = Vector3.Distance(movedPointPos, previousPointPos);
            // 後ポイントとのラインを修正する
        
        }
    }
}
