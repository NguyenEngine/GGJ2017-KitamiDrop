using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class AudioAnalyzer : Singleton<AudioAnalyzer>
{
    public bool m_isAnalyzing;
    public TextAsset TextFile;
    public List<AudioKeynote> m_audioKeynotes = new List<AudioKeynote>();
    public AudioSource m_audioSource;
    private int m_currentIndex;
    private float m_currentSamplePoint;
    private float m_samplesPerSecond;
    private float m_startingSamplePoint;
    private float m_highestSamplePoint;
    //private float m_audioDuration;

    [Serializable]
    public class AudioKeynote
    {
        public int m_keynoteIndex = -1;
        public IEvent m_audioEvent;
        public bool m_enabled;

        public AudioKeynote(int keynoteIndex, IEvent audioEvent, bool enabled)
        {
            m_keynoteIndex = keynoteIndex;
            m_audioEvent = audioEvent;
            m_enabled = enabled;
        }
    }

    public void Initialize()
    {
        readTextFileLines();
        m_currentIndex = 0;
        m_currentSamplePoint = 0;
        m_startingSamplePoint = m_audioKeynotes[0].m_keynoteIndex;
        m_highestSamplePoint = m_audioKeynotes[m_audioKeynotes.Count - 1].m_keynoteIndex;
        m_samplesPerSecond = (m_highestSamplePoint - m_startingSamplePoint) / m_audioSource.clip.length;
        m_isAnalyzing = true;
    }

    public void Stop()
    {
        m_isAnalyzing = false;
    }

    void Update()
    {
        if (m_isAnalyzing)
        {
            AudioKeynote currentNote = m_audioKeynotes[m_currentIndex];
            if (m_currentSamplePoint + m_startingSamplePoint >= currentNote.m_keynoteIndex)
            {
                if (currentNote != null)
                {
                    EventManager.Instance.PostImmediately(currentNote.m_audioEvent);
                }
                m_currentIndex++;

                if (m_currentIndex == m_audioKeynotes.Count)
                {
                    if (PlayerManager.Instance.m_player1.GetComponent<LifeScript>().IsAlive())
                    {
                        GameManager.Instance.YouWin();
                    }
                    m_currentIndex = 0;
                    m_currentSamplePoint = 0;
                }
            }

            m_currentSamplePoint += m_samplesPerSecond * Time.deltaTime;
        }
        else
        {
            m_currentIndex = 0;
            m_currentSamplePoint = 0;
        }
    }

    void readTextFileLines()
    {
        string[] linesInFile = TextFile.text.Split('\n');

        foreach (string line in linesInFile)
        {
            if (line.Contains("n="))
            {
                //Debug.Log(line);
                string[] splitRead = line.Split(' ');
                int index = int.Parse(splitRead[0]);
                bool enabled = (splitRead[1] == "On");
                int nodeIndex = int.Parse(splitRead[3].Replace("n=", ""));

                AudioKeynote note = new AudioKeynote(index, GetAudioEvent(nodeIndex, enabled), enabled);
                m_audioKeynotes.Add(note);
            }
        }

    }

    private IEvent GetAudioEvent(int nodeIndex, bool enabled)
    {
        switch (nodeIndex)
        {
            case 0:
                return new NodeEvents.Node0(enabled);
            case 24:
                return new NodeEvents.Node24(enabled);
            case 25:
                return new NodeEvents.Node25(enabled);
            case 26:
                return new NodeEvents.Node26(enabled);
            case 27:
                return new NodeEvents.Node27(enabled);
            case 28:
                return new NodeEvents.Node28(enabled);
            case 29:
                return new NodeEvents.Node29(enabled);
            case 30:
                return new NodeEvents.Node30(enabled);
            case 31:
                return new NodeEvents.Node31(enabled);
            case 32:
                return new NodeEvents.Node32(enabled);
            case 33:
                return new NodeEvents.Node33(enabled);
            case 34:
                return new NodeEvents.Node34(enabled);
            case 35:
                return new NodeEvents.Node35(enabled);
            case 36:
                return new NodeEvents.Node36(enabled);
            case 57:
                return new NodeEvents.Node57(enabled);
            default:
                Debug.LogError("No event with the node index " + nodeIndex + " was found.");
                return null;
        }
    }
}
