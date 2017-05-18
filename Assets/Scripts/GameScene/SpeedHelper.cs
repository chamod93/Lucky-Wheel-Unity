using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.GameScene
{
    class SpeedHelper
    {
        private static readonly float TIME_LIMIT = 8f;
        private static readonly float REDUCE_SPEED = 0.5f;

        private static readonly int STATE_ORIGIN = 0;
        private static readonly int STATE_CHANGING = 1;
        private static readonly int STATE_NEW_SPEED = 2;
        private static readonly int STATE_CHANGING_TO_ORIGIN = 3;

        private static int state;

        private float originSpeed;
        private float elapsedTime;
        private float speed;
        private float acceleration;

        public SpeedHelper(float originSpeed)
        {
            this.originSpeed = originSpeed;
            elapsedTime = 0;
            state = STATE_ORIGIN;
            acceleration = (originSpeed * REDUCE_SPEED - originSpeed) / TIME_LIMIT;
        }

        public float GetUpdateSpeed(float deltaTime)
        {
            elapsedTime = elapsedTime + deltaTime;
            if (elapsedTime >= TIME_LIMIT)
            {
                NextState();
            }
            return GetSpeed();
        }

        private void NextState()
        {
            state = state + 1;
            if (state > STATE_CHANGING_TO_ORIGIN)
            {
                state = STATE_ORIGIN;
            }
        }

        private float GetSpeed()
        {
            if (state == STATE_ORIGIN)
            {
                return originSpeed;
            }
            else if (state == STATE_CHANGING)
            {
                return originSpeed + acceleration * elapsedTime;
            }
            else if (state == STATE_NEW_SPEED)
            {
                return originSpeed * REDUCE_SPEED;
            }
            else if (state == STATE_CHANGING_TO_ORIGIN)
            {
                return originSpeed - acceleration * elapsedTime;
            }
            return originSpeed;
        }
    }
}
