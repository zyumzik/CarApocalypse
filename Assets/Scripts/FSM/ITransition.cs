namespace FiniteStateMachine
{
    public interface ITransition
    {
        IState ToState { get; }
        IPredicate Condition { get; }
    }
}
