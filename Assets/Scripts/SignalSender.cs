using UnityEngine;

namespace Assets.Scripts
{
    public class SignalSender : MonoBehaviour
    {
        public bool OnlyOnce;
        public ReceiverItem[] Receivers;

        private bool _hasFired;

        public void SendSignals(MonoBehaviour sender)
        {
            if (!_hasFired || !OnlyOnce)
            {
                for (int i = 0; i < Receivers.Length; i++)
                {
                    sender.StartCoroutine(Receivers[i].SendWithDelay(sender));
                }
                _hasFired = true;
            }
        }
    }
}