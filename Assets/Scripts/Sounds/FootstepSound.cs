using UnityEngine;

public class FootstepSound : MonoBehaviour
{

    [SerializeField] private AudioSource m_audioSource;

    public AudioClip[] audioClips;

    private void Awake()
    {
        if (m_audioSource == null)
            m_audioSource = NewAudioSource();
    }

    private void OnFootstep(AnimationEvent animationEvent)
    {
        if (audioClips.Length > 0)
        {
            var index = Random.Range(0, audioClips.Length);
            m_audioSource.PlayOneShot(audioClips[index]);
        }
    }

    private AudioSource NewAudioSource()
    {
        GameObject audioObject = new GameObject("AudioSource");
        audioObject.transform.SetParent(transform);
        audioObject.transform.localPosition = Vector3.zero;
        AudioSource source = audioObject.AddComponent<AudioSource>();
        source.loop = false;
        source.playOnAwake = false;
        source.spatialBlend = 1f;
        source.minDistance = 1f;
        source.maxDistance = 20f;
        return source;
    }

}
