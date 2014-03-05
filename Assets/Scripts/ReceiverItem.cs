using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class ReceiverItem : MonoBehaviour
    {
        public GameObject Receiver;
        public string Action = "OnSignal";
        public float Delay;

        public IEnumerator SendWithDelay(MonoBehaviour sender)
        {
            yield return new WaitForSeconds(Delay);

            if (Receiver)
                Receiver.SendMessage(Action);
            else
            {
                Debug.LogWarning("There is no receiver");
            }
        }
    }
}