using HoloMeasurement.AppManager;
using UnityEngine;
using UniRx;

namespace HoloMeasurement.Menu
{
    public class MenuPresenter : MonoBehaviour
    {
        private void Start()
        {
            var buttons = GetComponentsInChildren<MenuButton>();
            // Model⇒View
            AppStateManager.Instance.CurrentState
                .Subscribe(state =>
                {
                    foreach(var button in buttons)
                    {
                        button.SetButtonColor(state);
                    }
                })
                .AddTo(gameObject);

            // View⇒Model
            foreach(var button in buttons)
            {
                button.OnClickAsObservable
                    .Subscribe(buttonType =>
                    {
                        AppStateManager.Instance.CurrentState.Value = buttonType;
                    })
                    .AddTo(gameObject);
            }
        }
    }

}