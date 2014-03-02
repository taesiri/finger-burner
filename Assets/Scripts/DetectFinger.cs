using UnityEngine;

namespace Assets.Scripts
{
    public class DetectFinger : MonoBehaviour
    {
        public GameObject FingerPoint;
        public int TotalNumberOfFingers = 10;
        private GameObject[] _fingerCollection;

        private void Start()
        {
            _fingerCollection = new GameObject[TotalNumberOfFingers];

            for (int i = 0; i < TotalNumberOfFingers; i++)
            {
                _fingerCollection[i] = Instantiate(FingerPoint, Vector3.back*100, Quaternion.identity) as GameObject;
                _fingerCollection[i].renderer.enabled = false;
            }
        }

        private void Update()
        {
#if UNITY_ANDROID || UNITY_IPHONE
            // At this stage we are not going to handle peoples with more than 10 fingers
            if (Input.touchCount > 0 && Input.touchCount <= 10)
            {
                for (int i = 0; i < Input.touchCount; i++)
                {
                    Ray ray = camera.ScreenPointToRay(Input.touches[i].position);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        _fingerCollection[i].renderer.enabled = true;
                        _fingerCollection[i].renderer.transform.position = new Vector3(hit.point.x, hit.point.y, 8);
                    }
                }

                for (int i = Input.touchCount; i < TotalNumberOfFingers; i++)
                {
                    _fingerCollection[i].renderer.enabled = false;
                }
            }
            else
            {
                for (int i = 0; i < TotalNumberOfFingers; i++)
                {
                    _fingerCollection[i].renderer.enabled = false;
                }
            }
#endif
        }
    }
}