using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixerHandler : MonoBehaviour
{
    public Mixer mixer;

    private void Awake()
    {
        mixer.Empty();
    }
}
