using System.Collections;
using UnityEngine;

namespace Core.StateManagement.States
{
    /// <summary>
    /// Final state - game is complete
    /// </summary>
    [CreateAssetMenu(fileName = "GameEndState", menuName = "Scripts/GameStates/ScriptableObjects/GameEndState", order = 1)]
    public class GameEndState : GameState
    {
        protected override GameStateManager StateManager { get; set; }
        //public new string StateName = "game_end";

        public override IEnumerator PerformStateRoutine(RoundManager roundManager)
        {
            Defaulter defaulter = FindAnyObjectByType<Defaulter>();
            defaulter.FuckGoBack();
            _playerContinued = false;
            Debug.LogWarning("win_round_" + Properties.RoundsPlayed);
            if (Properties.IsPlayerRoundWinner == true)
            {
                dialogueEvents.TriggerDialogueByName("win_round_" + Properties.RoundsPlayed);
            }
            else
            {
                dialogueEvents.TriggerDialogueByName("lose_round_" + Properties.RoundsPlayed);
            }
            yield return WaitForDialogueEnd();
            StateManager.NextState();
        }
    }
}
