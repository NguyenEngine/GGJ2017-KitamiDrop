using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletImpactListener : MonoBehaviour 
{
    public GameObject PFB_BulletSound;
    public List<AudioClip> m_bulletSoundClips = new List<AudioClip>();

    void OnEnable()
    {
        if (EventManager.Instance)
            EventManager.Instance.Subscribe<OnBulletHitsEnemyEvent>(OnBulletImpact);
    }

    void OnDisable()
    {
        if (EventManager.Instance)
            EventManager.Instance.Unsubscribe<OnBulletHitsEnemyEvent>(OnBulletImpact);
    }

    public void OnBulletImpact(OnBulletHitsEnemyEvent e)
    {
        GameObject bulletSound = (GameObject)Instantiate(PFB_BulletSound, e.m_position, Quaternion.identity);
        bulletSound.GetComponent<AudioSource>().clip = m_bulletSoundClips[Random.Range(0, m_bulletSoundClips.Count)];
        bulletSound.GetComponent<AudioSource>().Play();
        Destroy(bulletSound, bulletSound.GetComponent<AudioSource>().clip.length);
    }
}
