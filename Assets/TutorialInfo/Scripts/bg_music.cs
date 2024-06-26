using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
public class RandomMusicPlayer : MonoBehaviour
{
    // Müzik parçalarını saklamak için bir dizi
    public AudioClip[] musicTracks;
    
    public GameObject bg_music;
    // AudioSource bileşenine erişmek için bir liste
    private List<AudioSource> audioSources = new List<AudioSource>();

    // Başlangıçta kullanılacak varsayılan ses düzeyi
    public float defaultVolume = 0.5f;

    //Slider
    public Slider volumeSlider;

    // Başlangıçta çalışır
    void Start()
    {
        
        // Her müzik parçası için bir AudioSource oluşturun ve listeye ekleyin
        foreach (AudioClip track in musicTracks)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = track;
            audioSource.volume = defaultVolume; // Varsayılan ses düzeyini ayarla
            audioSources.Add(audioSource);
        }

        // Rastgele bir müzik çal
        PlayRandomMusic();
    }

    // Rastgele bir müzik çalma
    public void PlayRandomMusic()
    {
        // Rastgele bir indeks seçin
        int randomIndex = Random.Range(0, musicTracks.Length);

        // Seçilen müziği çal
        audioSources[randomIndex].Play();
    }

    // Ses düzeyini ayarla
    public void SetVolume()
    {
        volumeSlider = GetComponent<Slider>();
        // Tüm AudioSource bileşenlerinin ses düzeyini ayarla
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.volume = volumeSlider.value;
        }
    }

    public void onoff()
    {
        if (bg_music.activeInHierarchy)
        {
            bg_music.SetActive(false);
        }
        else
        {
            bg_music.SetActive(true);
        }
    }

    
}
