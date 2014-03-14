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

        public string[] ToolbarStrings = {"Additive Mode", "Delete Mode", "Movement"};
        public int ToolbarInt = 0;


        public enum EditorMode
        {
            GameMode = 0,
            AdditiveMode = 1,
            DeleteMode = 2,
            Movement = 3
        }

        private void Start()
        {
            SceneMode = EditorMode.AdditiveMode;
            if (!GameManger)
            {
                Debug.LogError("No Game Manager Attached");
            }
        }

        private void Update()
        {
            if (SceneMode != EditorMode.GameMode)
            {
#if UNITY_ANDROID || UNITY_IPHONE

                //Prevents touches through gui elements
                if (GUIUtility.hotControl == 0)
                {
                    if (_totalFingers < TotalNumberOfFingers)
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
                                    if (SceneMode == EditorMode.AdditiveMode)
                                    {
                                        if (hit.collider.gameObject.tag == "BackPlane")
                                        {
                                            CreateFingerSlot(new Vector3(hit.point.x, hit.point.y, 8));
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        Debug.Log(" No More Fingers Allowed!");
                    }
                }
#endif
            }
        }

        private void CreateFingerSlot(Vector3 position)
        {
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

        private void ClearScreen()
        {
            for (int i = 0; i < _listOfFinger.Count; i++)
            {
                Destroy(_listOfFinger[i].gameObject);
            }
            _listOfFinger.Clear();
            _totalFingers = 0;
        }

        private void OnGUI()
        {
            GUI.Label(new Rect(10, 80, 300, 50), string.Format("Fingers : {0}", _totalFingers), MenuSkin.label);

            if (SceneMode != EditorMode.GameMode)
            {
                ToolbarInt = GUI.Toolbar(new Rect(15, Screen.height - 100, 600, 60), ToolbarInt, ToolbarStrings, MenuSkin.button);
                UpdateSceneMode(ToolbarInt);

                if (GUI.Button(new Rect(Screen.width - 200, Screen.height - 100, 180, 60), "Clear Screen", MenuSkin.button))
                {
                    ClearScreen();
                }

                if (GUI.Button(new Rect(Screen.width - 220, 10, 200, 70), "Play Game", MenuSkin.button))
                {
                    if (_totalFingers > 0)
                    {
                        ReadyforPlayMode();
                        SceneMode = EditorMode.GameMode;
                    }
                }
            }
            else if (SceneMode == EditorMode.GameMode)
            {
                Debug.Log("InGameMode");
                if (GUI.Button(new Rect(Screen.width - 220, 10, 200, 70), "Edit Mode", MenuSkin.button))
                {
                    SceneMode = EditorMode.AdditiveMode;

                    GameManger.gameObject.SetActive(false);
                }
            }
        }

        private void UpdateSceneMode(int id)
        {
            switch (id)
            {
                case 0:
                    SceneMode = EditorMode.AdditiveMode;
                    break;
                case 1:
                    SceneMode = EditorMode.DeleteMode;
                    break;
                case 2:
                    SceneMode = EditorMode.Movement;
                    break;
            }
        }
    }
}