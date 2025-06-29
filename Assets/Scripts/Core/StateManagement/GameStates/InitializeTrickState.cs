using System.Collections;
using UnityEngine;
using UnityEngine.Profiling;

namespace Core.StateManagement.GameStates
{
    [CreateAssetMenu(fileName = "InitializeTrickState", menuName = "Scripts/GameStates/ScriptableObjects/InitializeTrickState", order = 1)]
    public class InitializeTrickState : GameState
    {
        protected override GameStateManager StateManager { get; set; }
        //public new string StateName = "initialize_trick";

        public override GameState GetNextState()
        {
            if (Properties.TricksPlayed == 3)
            {
                
            }
            
            if (Properties.IsRoundEnded)
            {
                return null;
            }

            return this;
        }

        public override IEnumerator PerformStateRoutine(RoundManager roundManager)
        {
            Debug.Log("[GameStateManager] Executing state action...");

            Properties.TricksPlayed++;
            
            _completedPlayerAction = false;
            roundManager.InitializeTrick(Properties);
            yield return WaitForPlayerAction();
        
            yield return new WaitForSeconds(0.5f);
            
            _playerContinued = false;
            dialogueEvents.TriggerDialogueByName($"{(Properties.IsPlayerLastTrickWinner ? "win" : "lose")}_{Properties.TricksPlayed}");
            yield return WaitForDialogueEnd();
            Debug.Log("[GameStateManager] State routine complete, calling callback");
            StateManager.NextState();
        }
    }
}