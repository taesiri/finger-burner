using System;

namespace Assets.Scripts
{
    public class GameEvent : EventArgs
    {
        public int EventId = 0;

    }

    public class FingerTouchesScreenEvent : GameEvent
    {
        public new int EventId = 1;
    }

    public class FingersLeavesScreenEvet : GameEvent
    {
        public new int EventId = 2;
    }

    public class FingerTouchesButton : GameEvent
    {
        public new int EventId = 3;
    }

    public class FingerLeavesButton : GameEvent
    {
        public new int EventId = 4;
    }
}