﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace HoloMeasurement.Figure.Impl
{
    public class Line : BaseFigure
    {
        private Point _start;
        private Point _end;
        private GameObject _line;

        private bool _isInitialize = false;

        public void Initialize(Point start, Point end, GameObject line)
        {
            if (!_isInitialize)
            {
                _isInitialize = true;
                _start = start;
                _end = end;
                _line = line;

                _start.Position
                    .Subscribe(_ => ReculcLine())
                    .AddTo(gameObject);

                _end.Position
                    .Subscribe(_ => ReculcLine())
                    .AddTo(gameObject);
            }
        }

        private void ReculcLine()
        {
            var previousPos = _start.Position.Value;
            var lastPos = _end.Position.Value;

            var centerPos = (lastPos + previousPos) * 0.5f;
            var direction = lastPos - previousPos;
            var distance = Vector3.Distance(lastPos, previousPos);

            _line.transform.position = centerPos;
            _line.transform.rotation = Quaternion.LookRotation(direction);
            _line.transform.localScale = new Vector3(distance, 0.005f, 0.005f);
            _line.transform.Rotate(Vector3.down, 90.0f);
        }
    }
}