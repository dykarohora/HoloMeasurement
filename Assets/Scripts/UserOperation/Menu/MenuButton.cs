#if NETFX_CORE || NET_4_0
using System;
#endif

using HoloMeasurement.AppManager;
using HoloToolkit.Unity.InputModule;
using UniRx;
using UnityEngine;

namespace HoloMeasurement.UserOperation.Menu
{
    public class MenuButton : MonoBehaviour, IInputClickHandler
    {
        [SerializeField]
        private AppState _buttonType;

        private Subject<AppState> _onClickedButton = new Subject<AppState>();
        public IObservable<AppState> OnClickAsObservable {
            get { return _onClickedButton; }
        }

        public void OnInputClicked(InputClickedEventData eventData)
        {
            _onClickedButton.OnNext(_buttonType);
        }

        public void SetButtonColor(AppState currentState)
        {
            if(_buttonType == currentState)
            {
                GetComponent<Renderer>().material.color = Color.green;
            } else
            {
                GetComponent<Renderer>().material.color = Color.gray;
            }
        }
    }
}
