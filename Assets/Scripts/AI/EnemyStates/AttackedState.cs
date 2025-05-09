using FiniteStateMachine;

namespace AI.EnemyStates
{
    public class AttackedState : State
    {
        private EnemyAC _enemyAc;
        
        public AttackedState(EnemyAC enemyAc)
        {
            _enemyAc = enemyAc;
        }

        public override void Enter()
        {
            _enemyAc.TriggerHit();
        }
    }
}