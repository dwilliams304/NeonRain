namespace Samples.State
{
    public abstract class StateNode<T>
    {
        public abstract void OnStateEnter(T stateMachine);
        public abstract void OnStateUpdate(T stateMachine);
        public abstract void OnStateExit(T stateMachine);
    }
}