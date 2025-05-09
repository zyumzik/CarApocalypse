using UnityEngine;

namespace FiniteStateMachine
{
    public class TimeTrigger
    {
        private bool _value;
        private float _triggerDuration;
        private float _timeElapsed;

        public TimeTrigger(StateMachine stateMachine, float triggerDuration)
        {
            _triggerDuration = triggerDuration;
            stateMachine.AddTrigger(this);
        }

        public void Update()
        {
            if (!_value) return;

            _timeElapsed += Time.deltaTime;
            if (_timeElapsed >= _triggerDuration)
            {
                _timeElapsed = 0;
                _value = false;
            }
        }

        public void Activate() => _value = true;

        public static implicit operator bool(TimeTrigger trigger) => trigger._value;
    }
}
