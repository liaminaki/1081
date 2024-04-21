using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineController : MonoBehaviour
{
    public PlayableDirector Timeline;

     // Function to pause the Timeline
    public void PauseTimeline()
    {
        // Pause the Timeline by setting its speed to 0
        Timeline.playableGraph.GetRootPlayable(0).SetSpeed(0);
    }

    // Function to resume the Timeline
    public void ResumeTimeline()
    {
        // Resume the Timeline by setting its speed back to 1
        Timeline.playableGraph.GetRootPlayable(0).SetSpeed(1);
        
    }
}
