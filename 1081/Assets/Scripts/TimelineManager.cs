using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{
    public List<PlayableDirector> timelines = new List<PlayableDirector>();
    private int current;

    void Start() {

        if (timelines.Count != 0) {
            PlayTimeline(0);
        }

    }

    public void PlayNext() {
        PlayTimeline(++current);
    }

    private void PlayTimeline(int index) {
        
        for (int i = 0; i < timelines.Count; i++) {

            PlayableDirector timeline = timelines[i];

            if (i == index) {
                timeline.Play();
                current = i;
            } 

            else {
                timeline.Stop();
            }

        } 
            
    }


}