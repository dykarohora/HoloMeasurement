using HoloMeasurement.Sizer.Impl;
using HoloMeasurement.UserOperation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UniRx;
using HoloToolkit.Unity;

namespace HoloMeasurement.AppManager
{
    public class SizerManager : Singleton<SizerManager>
    {
        [SerializeField]
        private LineSizer _lineSizer;

        [SerializeField]
        private PolygonSizer _polygonSizer;

        private IPointSettable _sizer;
        public IPointSettable Sizer { get { return _sizer; } }

        private void Start()
        {
            AppStateManager.Instance.CurrentState
                .Subscribe(state =>
                {
                    switch (state)
                    {
                        case AppState.Line:
                            _sizer = _lineSizer;
                            break;
                        case AppState.Polygon:
                            _sizer = _polygonSizer;
                            break;
                        case AppState.Solid:
                            break;
                    }
                })
                .AddTo(gameObject);
        }
    }
}
