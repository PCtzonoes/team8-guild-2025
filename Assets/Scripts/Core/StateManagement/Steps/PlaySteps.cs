namespace Core.StateManagement
{
    public class DealOpponentCardsStep : BaseGameStep
    {
        public override string StateName => "Deal Opponent Cards";
        
        protected override void OnEnter()
        {
            // Deal cards to opponent
            CompleteStep();
        }
    }
    
    public class PlayerUsePowerStep : BaseGameStep
    {
        public override string StateName => "Player Use Power";
        
        protected override void OnEnter()
        {
            // Enable power usage UI
            // Complete when player uses power or skips
        }
        
        public void OnPowerUsed()
        {
            CompleteStep();
        }
        
        public void OnPowerSkipped()
        {
            CompleteStep();
        }
    }
    
    public class PlayerPlayCardStep : BaseGameStep
    {
        public override string StateName => "Player Play Card";
        
        protected override void OnEnter()
        {
            // Enable card selection
        }
        
        public void OnCardPlayed()
        {
            CompleteStep();
        }
    }
    
    public class OpponentUsePowerStep : BaseGameStep
    {
        public override string StateName => "Opponent Use Power";
        
        protected override void OnEnter()
        {
            // AI decides to use power
            // Complete after AI action
            CompleteStep();
        }
    }
    
    public class RevealHiddenCardStep : BaseGameStep
    {
        public override string StateName => "Reveal Hidden Card";
        
        protected override void OnEnter()
        {
            // Reveal opponent's card
            CompleteStep();
        }
    }
    
    public class TrickEndStep : BaseGameStep
    {
        public override string StateName => "Trick End";
        
        protected override void OnEnter()
        {
            // Calculate trick winner, update scores
            CompleteStep();
        }
    }
    
    public class GameEndStep : BaseGameStep
    {
        public override string StateName => "Game End";
        
        protected override void OnEnter()
        {
            // Check if game should end or continue
            // This might loop back to earlier play steps or complete
            CompleteStep();
        }
    }
}
