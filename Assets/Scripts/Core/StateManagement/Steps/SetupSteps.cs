namespace Core.StateManagement
{
    public class ShuffleDeckStep : BaseGameStep
    {
        public override string StateName => "Shuffle Deck";
        
        protected override void OnEnter()
        {
            // Trigger deck shuffle
            // Complete immediately or wait for shuffle animation
            CompleteStep();
        }
    }
    
    public class DealPlayerCardsStep : BaseGameStep
    {
        public override string StateName => "Deal Player Cards";
        
        protected override void OnEnter()
        {
            // Trigger dealing cards to player
            // Complete when dealing animation finishes
            CompleteStep();
        }
    }
    
    public class DrawWildCardStep : BaseGameStep
    {
        public override string StateName => "Draw Wild Card";
        
        protected override void OnEnter()
        {
            // Draw and reveal wild card
            CompleteStep();
        }
    }
    
    public class PlaceBetStep : BaseGameStep
    {
        public override string StateName => "Place Bet";
        
        protected override void OnEnter()
        {
            // Enable betting UI
            // This step completes when player places bet
        }
        
        public void OnBetPlaced()
        {
            CompleteStep();
        }
    }
}
