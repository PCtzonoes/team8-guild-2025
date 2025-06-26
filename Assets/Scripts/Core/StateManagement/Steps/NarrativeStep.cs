namespace Core.StateManagement
{
    public class NarrativeStep : BaseGameStep
    {
        private string narrativeType;
        
        public override string StateName => $"Narrative ({narrativeType})";
        
        public NarrativeStep(string type)
        {
            narrativeType = type;
        }
        
        protected override void OnEnter()
        {
            // This step will be completed by dialogue system
            // The dialogue manager will call CompleteStep() when dialogue ends
        }
        
        public void OnDialogueComplete()
        {
            CompleteStep();
        }
    }
}
