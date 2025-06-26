using UnityEngine;
using Core.StateManagement;

namespace Core
{
    public class DialogueManagerTriggerHandler : MonoBehaviour, ITriggerHandler
    {
        [Header("References")]
        [SerializeField] private GameStateManager stateManager;
        
        // Add reference to your dialogue system here
        // [SerializeField] private DialogueSystem dialogueSystem;
        
        private void Start()
        {
            if (stateManager == null)
                stateManager = FindObjectOfType<GameStateManager>();
                
            stateManager.RegisterTriggerHandler(this);
        }
        
        private void OnDestroy()
        {
            if (stateManager != null)
                stateManager.UnregisterTriggerHandler(this);
        }
        
        public void HandlePhaseChange(GamePhase newPhase, GamePhase previousPhase)
        {
            Debug.Log($"[DialogueManager] Phase changed: {previousPhase} -> {newPhase}");
            
            // Handle phase-specific dialogue setup if needed
            switch (newPhase)
            {
                case GamePhase.PreGame:
                    // Prepare pre-game dialogue
                    break;
                case GamePhase.PostGame:
                    // Prepare post-game dialogue
                    break;
            }
        }
        
        public void HandleStepChange(IGameState newStep, IGameState previousStep)
        {
            Debug.Log($"[DialogueManager] Step changed: {previousStep?.StateName ?? "None"} -> {newStep.StateName}");
            
            // Handle narrative steps specifically
            if (newStep is NarrativeStep narrativeStep)
            {
                HandleNarrativeStep(narrativeStep);
            }
        }
        
        private void HandleNarrativeStep(NarrativeStep narrativeStep)
        {
            string dialogueKey = GetDialogueKeyForNarrative(narrativeStep.StateName);
            
            if (!string.IsNullOrEmpty(dialogueKey))
            {
                StartDialogue(dialogueKey, narrativeStep);
            }
            else
            {
                // No dialogue for this narrative, complete immediately
                narrativeStep.OnDialogueComplete();
            }
        }
        
        private string GetDialogueKeyForNarrative(string narrativeType)
        {
            // Map narrative types to dialogue keys
            switch (narrativeType)
            {
                case "Narrative (PreGame Narrative)":
                    return "pregame_intro";
                case "Narrative (PostGame Narrative)":
                    return "postgame_outro";
                default:
                    return null;
            }
        }
        
        private void StartDialogue(string dialogueKey, NarrativeStep narrativeStep)
        {
            Debug.Log($"Starting dialogue: {dialogueKey}");
            
            // Example of how you might integrate with your dialogue system:
            // dialogueSystem.StartDialogue(dialogueKey, () => {
            //     narrativeStep.OnDialogueComplete();
            // });
            
            // For now, simulate dialogue completion after a delay
            StartCoroutine(SimulateDialogue(narrativeStep));
        }
        
        private System.Collections.IEnumerator SimulateDialogue(NarrativeStep narrativeStep)
        {
            // Simulate dialogue duration
            yield return new WaitForSeconds(2f);
            
            // Complete the narrative step
            narrativeStep.OnDialogueComplete();
        }
        
        // Method to be called by your actual dialogue system when dialogue ends
        public void OnDialogueEnd()
        {
            // Get current narrative step and complete it
            if (stateManager.CurrentStep is NarrativeStep currentNarrativeStep)
            {
                currentNarrativeStep.OnDialogueComplete();
            }
        }
        
        // Method to be called by your dialogue system when dialogue starts
        public void OnDialogueStart()
        {
            // Handle dialogue start if needed (pause game, disable input, etc.)
        }
    }
}
