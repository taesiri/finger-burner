using UnityEngine;

namespace Assets.Scripts
{
    public class FingerPushDetector : MonoBehaviour
    {
        public CoreGameplayManager Mother;
        public GUISkin Skin;
        public int TotalNumberOfFingers = 10;


        public void Start()
        {
            if (!Mother)
            {
                Debug.LogError("Mother not found!");
            }
        }

        private void Update()
        {
#if UNITY_ANDROID || UNITY_IPHONE

            var tCount = Input.touchCount;
            if (tCount > 0 && tCount <= TotalNumberOfFingers)
            {
                for (int i = 0; i < tCount; i++)
                {
                    Debug.Log(Input.touches[i].phase);

                    Ray ray = Camera.main.ScreenPointToRay(Input.touches[i].position);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider.gameObject.tag == "BackPlane")
                        {
                        }
                        else if (hit.collider.gameObject.tag == "PushMe")
                        {
                            switch (Input.touches[i].phase)
                            {
                                case TouchPhase.Ended:
                                    Mother.OnFingerTouchesButtonEnd(new GameEvent(), hit.collider.gameObject);
                                    break;
                                case TouchPhase.Began:
                                    Mother.OnFingerTouchesButtonBegan(new GameEvent(), hit.collider.gameObject);
                                    break;

                                case TouchPhase.Moved:
                                    Mother.OnFingerTouchesButtonStays(new GameEvent(), hit.collider.gameObject);
                                    break;
                                case TouchPhase.Stationary:
                                    Mother.OnFingerTouchesButtonStays(new GameEvent(), hit.collider.gameObject);
                                    break;
                                case TouchPhase.Canceled:
                                    Mother.OnFingerTouchesButtonCanceled(new GameEvent(), hit.collider.gameObject);
                                    break;
                            }
                        }
                    }
                }
            }


#endif
        }
    }
}