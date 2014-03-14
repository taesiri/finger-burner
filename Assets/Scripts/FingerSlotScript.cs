using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class FingerSlotScript : MonoBehaviour
    {
        public CoreGameplayManager Mother;
        public bool IsDown = false;
        private float _lastTime;
        private bool _isStarted;
        private FingerState _state;
        private Color _oldMAttColor;
        private Material _oldMaterial;

        // PlaceHolder
        public Material FireSatetMaterial;
        public Material IceStateMaterial;


        public AnimationCurve OnFireCurve;
        public AnimationCurve OnIceCurve;

        private Transform _transform;

        private void Start()
        {
            _oldMAttColor = renderer.material.color;
            _oldMaterial = renderer.material;

            _transform = transform;
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
            CheckGameState();
            SceneRender();
        }

        private void SceneRender()
        {
            switch (_state)
            {
                case FingerState.Default:

                    break;
                case FingerState.GoOnFire:
                    renderer.material.color = new Color(OnFireCurve.Evaluate(Time.time), 0, 1);
                    break;
                case FingerState.GoOnIce:
                    renderer.material.color = new Color(OnIceCurve.Evaluate(Time.time), 0, 1);
                    break;
            }
        }

        private void CheckGameState()
        {
            if (IsDown)
            {
                if (_state == FingerState.Fire)
                {
                    Debug.Log("You Lost the Game");
                }
            }
            else
            {
                if (_state == FingerState.Ice)
                {
                    Debug.Log("You Lost the Game");
                }
            }
        }

        private IEnumerator StateChanger(FingerState newState, float delay)
        {
            OnStateChanging(newState);
            _state = newState;

            yield return new WaitForSeconds(delay);

            switch (newState)
            {
                case FingerState.GoOnFire:
                    _state = FingerState.Fire;
                    OnStateChanged(_state);
                    break;

                case FingerState.GoOnIce:
                    _state = FingerState.Ice;
                    OnStateChanged(_state);
                    break;
            }
        }

        public void ChangeButtonSateWithDelay(FingerState newState, float delay)
        {
            StartCoroutine(StateChanger(newState, delay));
        }

        public void OnStateChanging(FingerState state)
        {
            switch (state)
            {
                case FingerState.Default:

                    break;
                case FingerState.GoOnFire:
                    //GoOnFire();
                    break;
                case FingerState.Fire:
                    //GoOnFire();
                    break;
                case FingerState.Ice:
                    //GoOnIce();
                    break;
                case FingerState.GoOnIce:
                    //GoOnIce();
                    break;
            }
        }

        public void OnStateChanged(FingerState state)
        {
            switch (state)
            {
                case FingerState.Default:
                    renderer.material.color = _oldMAttColor;
                    break;
                case FingerState.Fire:
                    renderer.material = FireSatetMaterial;
                    if (IsDown)
                        Mother.ScoreDown(10);
                    else
                        Mother.ScoreUp(10);
                    AfterOnFire();
                    break;
                case FingerState.Ice:
                    renderer.material = IceStateMaterial;

                    AfterOnIce();
                    break;
            }
        }

        private void AfterOnFire()
        {
            Debug.Log("OnFire");
            StartCoroutine(BackToDefault(3.0f));
        }

        private IEnumerator BackToDefault(float delay)
        {
            OnStateChanging(FingerState.Default);
            yield return new WaitForSeconds(delay);
            _state = FingerState.Default;
            OnStateChanged(FingerState.Default);
        }

        private void AfterOnIce()
        {
            Debug.Log("OnIce");
        }

        public void OnButtonStays()
        {
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

            switch (_state)
            {
                case FingerState.Default:
                    renderer.material.color = Color.green;
                    break;
                case FingerState.GoOnFire:
                    break;
                case FingerState.Fire:
                    break;
                case FingerState.GoOnIce:
                    break;
                case FingerState.Ice:
                    break;
            }

            Debug.Log("Button Pushed Down");
        }

        public void OnButtonUp()
        {
            IsDown = false;
            _isStarted = false;

            switch (_state)
            {
                case FingerState.Default:
                    renderer.material.color = _oldMAttColor;
                    break;

                case FingerState.GoOnFire:
                    break;
                case FingerState.Fire:
                    break;
                case FingerState.GoOnIce:
                    break;
                case FingerState.Ice:
                    break;
            }


            Debug.Log("Button Pushed Up");
        }

        public void ForceOnButtonUp()
        {
            Debug.Log("Button Pushed Up (FORCED)");
            OnButtonUp();
        }
    }
}