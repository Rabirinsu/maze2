using System.Collections.Generic;
using UnityEngine;

public class FootStepManager : MonoBehaviour
{
    public List<AudioClip> stoneSteps = new List<AudioClip>();
    public List<AudioClip> sandSteps = new List<AudioClip>();

    private enum Surface { Sand, Stone }
    private Surface surface;
    private List<AudioClip> currentAudio;
    [SerializeField]private AudioSource sourceAudio;
    void Start()
    {
        SelectStepList();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PlayStep()
    {
        AudioClip clip = currentAudio[Random.Range(0, currentAudio.Count)];
        sourceAudio.PlayOneShot(clip);

    }
    private void SelectStepList()
    {
        switch (surface)
        {
            case Surface.Sand:
                currentAudio = sandSteps;
                break;
            case Surface.Stone:
                currentAudio = stoneSteps;
                break;

        }
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Sand")
        {
            surface = Surface.Sand;
        }

        if (hit.transform.tag == "Stone")
        {
            surface = Surface.Stone;
        }
        SelectStepList();
    }
}
