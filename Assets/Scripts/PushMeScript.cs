using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class PushMeScript : MonoBehaviour
    {
        public CoreGameplayManager Mother;
        public bool IsDown = false;
        private float _lastTime;
        private bool _isStarted;
        private PushMeState _state;
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
                case PushMeState.Default:

                    break;
                case PushMeState.GoOnFire:
					renderer.material.SetColor("_Color", new Color(OnFireCurve.Evaluate(Time.time),0,1));
				    break;
                case PushMeState.GoOnIce:
					renderer.material.SetColor("_Color", new Color(OnIceCurve.Evaluate(Time.time),0,1));
				break;
            }
        }

        private void CheckGameState()
        {
            if (IsDown)
            {
                if (_state == PushMeState.Fire)
                {
                    Debug.Log("You Lost the Game");
                }
            }
            else
            {
                if (_state == PushMeState.Ice)
                {
                    Debug.Log("You Lost the Game");
                }
            }
        }

        private IEnumerator StateChanger(PushMeState newState, float delay)
        {
            OnStateChanging(newState);
            _state = newState;

            yield return new WaitForSeconds(delay);

            switch (newState)
            {
                case PushMeState.GoOnFire:
                    _state = PushMeState.Fire;
                    OnStateChanged(_state);
                    break;

                case PushMeState.GoOnIce:
                    _state = PushMeState.Ice;
                    OnStateChanged(_state);
                    break;
            }
        }

        public void ChangeButtonSateWithDelay(PushMeState newState, float delay)
        {
            StartCoroutine(StateChanger(newState, delay));
        }

        public void OnStateChanging(PushMeState state)
        {
            switch (state)
            {
                case PushMeState.Default:

                    break;
                case PushMeState.GoOnFire:
                    //GoOnFire();
                    break;
                case PushMeState.Fire:
                    //GoOnFire();
                    break;
                case PushMeState.Ice:
                    //GoOnIce();
                    break;
                case PushMeState.GoOnIce:
                    //GoOnIce();
                    break;
            }
        }

        public void OnStateChanged(PushMeState state)
        {
            switch (state)
            {
                case PushMeState.Default:
                    renderer.material.color = _oldMAttColor;
                    break;
                case PushMeState.Fire:
                    renderer.material = FireSatetMaterial;
                    if (IsDown)
                        Mother.ScoreDown(10);
                    else
                        Mother.ScoreUp(10);
                    AfterOnFire();
                    break;
                case PushMeState.Ice:
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
            OnStateChanging(PushMeState.Default);
            yield return new WaitForSeconds(delay);
            _state = PushMeState.Default;
            OnStateChanged(PushMeState.Default);
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
                case PushMeState.Default:
                    renderer.material.color = Color.green;
                    break;
                case PushMeState.GoOnFire:
                    break;
                case PushMeState.Fire:
                    break;
                case PushMeState.GoOnIce:
                    break;
                case PushMeState.Ice:
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
                case PushMeState.Default:
                    renderer.material.color = _oldMAttColor;
                    break;

                case PushMeState.GoOnFire:
                    break;
                case PushMeState.Fire:
                    break;
                case PushMeState.GoOnIce:
                    break;
                case PushMeState.Ice:
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