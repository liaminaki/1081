using UnityEngine;
using UnityEngine.Playables;

public class GameplayController : MonoBehaviour
{
    public PopupController Objective; // Objective popup
    public PlayableDirector PlayableDirector; // Timeline container

    public void PauseTimeline() {
        // Pause the Timeline by setting its speed to 0
        PlayableDirector.playableGraph.GetRootPlayable(0).SetSpeed(0);
    }

    public void ResumeTimeline() {
        // Resume the Timeline by setting its speed back to 1
        PlayableDirector.playableGraph.GetRootPlayable(0).SetSpeed(1);
    }

    // Disable coins, shield and player from moving
    public void Pause() {
        Time.timeScale = 0f;
    }

    // Enable coins, shield and player from moving
    public void Resume() {
        Time.timeScale = 1f;
    }

    public void ShowObjective() {
        Objective.Show();
    }

    public void HideObjective() {
        Objective.Hide();
    }

}