using UnityEngine;

namespace Assets.Scripts
{
    public class CoreGameplayManager : MonoBehaviour
    {
        public GameObject[] Listeners;

        public void Start()
        {
        }


        public void Update()
        {
        }

        public void OnFingersLeavesScreenEvet(GameEvent gevent, GameObject sender)
        {
            Debug.Log("Finger Off the screen!");
        }


        public void OnFingerTouchesButtonBegan(GameEvent gevent, GameObject sender)
        {
            var script = sender.GetComponent<PushMeScript>();

            if (script)
            {
                script.OnButtonDown();
            }
            else
            {
                Debug.LogError("Unable to Acces PushMe Script");
            }
        }


        public void OnFingerTouchesButtonCanceled(GameEvent gevent, GameObject sender)
        {
            var script = sender.GetComponent<PushMeScript>();

            if (script)
            {
                script.OnButtonStays();
            }
            else
            {
                Debug.LogError("Unable to Acces PushMe Script");
            }
        }

        public void OnFingerTouchesButtonStays(GameEvent gevent, GameObject sender)
        {
            var script = sender.GetComponent<PushMeScript>();

            if (script)
            {
                script.OnButtonStays();
            }
            else
            {
                Debug.LogError("Unable to Acces PushMe Script");
            }
        }

        public void OnFingerTouchesButtonEnd(GameEvent gevent, GameObject sender)
        {
            var script = sender.GetComponent<PushMeScript>();

            if (script)
            {
                script.OnButtonUp();
            }
            else
            {
                Debug.LogError("Unable to Acces PushMe Script");
            }
        }
    }
}