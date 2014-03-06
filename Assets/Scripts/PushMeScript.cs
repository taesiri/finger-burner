using UnityEngine;

namespace Assets.Scripts
{
    public class PushMeScript : MonoBehaviour
    {
        public bool IsDown = false;
        private float _lastTime;
        private bool _isStarted;


        private Color _oldMAttColor;
        private void Start()
        {
            _oldMAttColor = renderer.material.color;
        }

        private void Update()
        {
            if (IsDown)
            {
                if (_isStarted)
                {
                    if (Time.time - _lastTime > 0.1f)

                    {
                        ForceOnButtonUp();
                        _isStarted = false;
                    }
                }
            }
        }


        public void OnButtonStays()
        {
            //Debug.Log("Stays");
            if (IsDown)
            {
                _isStarted = true;
                _lastTime = Time.time;
            }
        }

        public void OnButtonCanceled()
        {
            Debug.Log("Button Pushed Up (FORCED)");
            OnButtonUp();
        }

        public void OnButtonDown()
        {
            IsDown = true;
            renderer.material.color = Color.green;
            Debug.Log("Button Pushed Down");
        }

        public void OnButtonUp()
        {
            IsDown = false;
            _isStarted = false;
            Debug.Log("Button Pushed Up");
            renderer.material.color = _oldMAttColor;
        }

        public void ForceOnButtonUp()
        {
            Debug.Log("Button Pushed Up (FORCED)");
            OnButtonUp();
        }
    }
}