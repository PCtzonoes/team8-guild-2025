using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.StateManagement.States
{
    /// <summary>
    /// State for pre-game narrative/intro dialogue
    /// </summary>
    [CreateAssetMenu(fileName = "InitializeRoundState", menuName = "Scripts/GameStates/ScriptableObjects/InitializeRoundState", order = 1)]
    public class InitializeRoundState : GameState
    {
        protected override GameStateManager StateManager { get; set; }
        //private PlayerHUD _playSpace;

        public override IEnumerator PerformStateRoutine(RoundManager roundManager)
        {
            PlayerHUD playSpace = FindObjectOfType<PlayerHUD>();
            playSpace.SummonPlaySpace();   
            _playerContinued = false;
            Properties.InitialPlayerHandSize = StateManager.tricksPerRound[Properties.RoundsPlayed];
            Properties.RoundsPlayed++;
            dialogueEvents.TriggerDialogueByName(StateName + "_" + Properties.RoundsPlayed);
            Properties.TricksPlayed = 0;
            Properties.IsRoundEnded = false;
            yield return WaitForDialogueEnd();
            StateManager.NextState();
        }

        //private void OnEnable()
        //{
        //    _playSpace = FindObjectOfType<PlayerHUD>();
        //}
    }
}
