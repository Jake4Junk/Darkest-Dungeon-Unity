using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video; // Added for VideoPlayer
using System.Collections;

#if !UNITY_WEBGL
public class MoviePlayer : MonoBehaviour
{
    [SerializeField]
    private VideoPlayer videoPlayer; // Replacing MovieTexture with VideoPlayer

    private GameIntro gameIntro;
    private AudioSource audioSource;
    private IEnumerator videoCoroutine;
    private float vol;

    private void Awake()
    {
        gameIntro = GetComponentInParent<GameIntro>();
        videoPlayer = GetComponent<VideoPlayer>(); // Get the VideoPlayer component
        audioSource = GetComponent<AudioSource>();

        // Set up VideoPlayer
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoPlayer.EnableAudioTrack(0, true);
        videoPlayer.SetTargetAudioSource(0, audioSource);
    }

    private void Update()
    {
        if (Input.anyKey)
            FinishVideo();
    }

    public void Play()
    {
        if (videoPlayer == null)
        {
            gameIntro.FinishIntro();
            return;
        }

        gameObject.SetActive(true);
        videoPlayer.Play();
        audioSource.Play();
        vol = audioSource.volume;
        GetComponent<RawImage>().color = new Color(255, 255, 255, 0);
        //videoPlayer.filterMode = FilterMode.Point;
        audioSource.volume = 0;
        videoCoroutine = VideoEnd();
        StartCoroutine(videoCoroutine);
    }

    private void FinishVideo()
    {
        if (videoCoroutine != null)
        {
            StopCoroutine(videoCoroutine);
        }
        videoPlayer.Stop();
        audioSource.Stop();
        gameObject.SetActive(false);
        gameIntro.FinishIntro();
    }

    private IEnumerator VideoEnd()
    {
        while (videoPlayer.isPlaying)
        {
            // ... Rest of your VideoEnd coroutine ...
            yield return null;
        }

        FinishVideo();
    }
}
#endif
