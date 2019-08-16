using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoodManager : MonoBehaviour {

	public enum moods{
		Passive,
		Transition,
		Aggressive
	};

	public delegate void onMoodChangeDelegate();
	onMoodChangeDelegate OnMoodChange;
	public moods currentMood;


	void Start()
	{
		currentMood = moods.Passive;
	}

	public moods setMood(moods mood)
	{
		OnMoodChange();
		currentMood = mood;

		return currentMood;
	}

	public moods GetCurrentMood()
	{
		return currentMood;
	}

}
