using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public GameObject PFB_PlayerHitSound;
    public List<AudioClip> m_bulletSoundClips = new List<AudioClip>();
    public void PlaySound()
    {
        GameObject playerSound = (GameObject)Instantiate(PFB_PlayerHitSound, PlayerManager.Instance.m_player1.transform.position , Quaternion.identity);
        playerSound.GetComponent<AudioSource>().clip = m_bulletSoundClips[Random.Range(0, m_bulletSoundClips.Count)];
        playerSound.GetComponent<AudioSource>().Play();
        Destroy(playerSound, playerSound.GetComponent<AudioSource>().clip.length);
    }
}
