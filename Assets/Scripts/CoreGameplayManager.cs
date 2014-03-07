using System;
using System.Reflection.Emit;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts
{
    public class CoreGameplayManager : MonoBehaviour
    {
        public GUISkin ContentSkin;
        public PushMeScript[] Listeners;
        public float TimeInterval = 1.0f;
        public Random CoreGenerator = new Random(DateTime.Now.Millisecond);
        private float _lastTick;
        private bool _isStarted = false;
        private int _len = 0;

        private int _gameScore = 0;

        public void Start()
        {
            _lastTick = Time.time;
            if (Listeners.Length <= 0)
            {
                Debug.LogError("Please Set Listeners List");
            }
            _len = Listeners.Length;
            _isStarted = true;
        }


        public void Update()
        {
            if (_isStarted)
            {
                if (Time.time - _lastTick > TimeInterval)
                {
                    _lastTick = Time.time;

                    var itemInex = CoreGenerator.Next(0, _len);
                    Listeners[itemInex].ChangeButtonSateWithDelay(PushMeState.GoOnFire, 2.0f);
                    Debug.Log(string.Format("Buttons Goes on fire - id:{0}", itemInex));
                }
                else
                {
                }
            }
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

        public void ScoreUp(int amount)
        {
            _gameScore += amount;
        }

        public void ScoreDown(int amount)
        {
            _gameScore -= amount;
        }

        public void OnGUI()
        {
            GUI.Label(new Rect(25, 25, 250, 50), _gameScore.ToString(), ContentSkin.label);
        }
    }
}