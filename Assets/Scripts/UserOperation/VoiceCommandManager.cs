using HoloMeasurement.AppManager;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

namespace HoloMeasurement.UserOperation
{
    public class VoiceCommandManager : MonoBehaviour, ISpeechHandler
    {

        private void Start()
        {
            InputManager.Instance.AddGlobalListener(gameObject);
        }

        // もうちょっといけてる方法考えたい
        public void OnSpeechKeywordRecognized(SpeechEventData eventData)
        {
            switch (eventData.RecognizedText)
            {
                case "Delete":
                    if (GazeManager.Instance.HitObject != null)
                    {
                        var deletable = GazeManager.Instance.HitObject.GetComponentInParent<IDeletable>();
                        if (deletable != null)
                        {
                            deletable.DeleteFigure();
                        }
                    }
                    break;
                case "Close":
                    var sizer = SizerManager.Instance.Sizer as IPolygonClosable;
                    if(sizer != null)
                    {
                        sizer.ClosePolygon();
                    }
                    break;
            }
        }
    }
}