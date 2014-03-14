using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class LevelEditorScript : MonoBehaviour
    {
        public GUISkin MenuSkin;
        public GameObject RingSlot;
        public int TotalNumberOfFingers = 10;
        public EditorMode SceneMode;
        private int _totalFingers = 0;
        public FingerSlotScript[] Listeners;
        public CoreGameplayManager GameManger;


        private List<FingerSlotScript> _listOfFinger = new List<FingerSlotScript>();

        public enum EditorMode
        {
            GameMode = 0,
            EditMode = 1
        }

        private void Start()
        {
            SceneMode = EditorMode.EditMode;
            if (!GameManger)
            {
                Debug.LogError("No Game Manager Attached");
            }
        }

        private void Update()
        {
            if (SceneMode == EditorMode.EditMode)
            {
#if UNITY_ANDROID || UNITY_IPHONE

                if (_totalFingers <= TotalNumberOfFingers)
                {
                    var tCount = Input.touchCount;
                    if (tCount > 0 && tCount <= TotalNumberOfFingers)
                    {
                        for (int i = 0; i < tCount; i++)
                        {
                            Ray ray = Camera.main.ScreenPointToRay(Input.touches[i].position);
                            RaycastHit hit;
                            if (Physics.Raycast(ray, out hit))
                            {
                                if (hit.collider.gameObject.tag == "BackPlane")
                                {
                                    CreateFingerSlot(new Vector3(hit.point.x, hit.point.y, 8));
                                }
                            }
                        }
                    }
                }
                else
                {
                    Debug.Log(" No More Fingers Allowed!");
                }
#endif
            }
        }

        private void CreateFingerSlot(Vector3 position)
        {
            //z = 8
            _totalFingers++;
            var newObject = Instantiate(RingSlot, position, Quaternion.identity) as GameObject;
            if (newObject != null)
            {
                var fscripot = newObject.GetComponent<FingerSlotScript>();
                fscripot.Mother = GameManger;
                _listOfFinger.Add(fscripot);
            }
        }

        private void ReadyforPlayMode()
        {
            GameManger.Listeners = new FingerSlotScript[_totalFingers];
            for (int i = 0; i < _listOfFinger.Count; i++)
            {
                GameManger.Listeners[i] = _listOfFinger[i];
            }

            GameManger.gameObject.SetActive(true);
        }

        private void OnGUI()
        {
            GUI.Label(new Rect(10, 80, 300, 50), string.Format("Fingers : {0}", _totalFingers), MenuSkin.label);

            if (SceneMode == EditorMode.EditMode)
            {
                if (GUI.Button(new Rect(10, 10, 200, 50), "Play Game"))
                {
                    SceneMode = EditorMode.GameMode;

                    ReadyforPlayMode();
                }
            }
            else if (SceneMode == EditorMode.GameMode)
            {
                if (GUI.Button(new Rect(10, 10, 200, 50), "Edit Mode"))
                {
                    SceneMode = EditorMode.EditMode;

                    GameManger.gameObject.SetActive(false);
                }
            }
        }
    }
}