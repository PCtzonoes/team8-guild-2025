namespace Core.StateManagement
{
    public interface ITriggerHandler
    {
        void HandlePhaseChange(GamePhase newPhase, GamePhase previousPhase);
        void HandleStepChange(IGameState newStep, IGameState previousStep);
    }
}
