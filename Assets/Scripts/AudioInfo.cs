using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioInfo : MonoBehaviour {

    Slider slider;

    private static int Offset = -80;
    public static float MasterVolume {
        get {
            float value;
            if(!MasterMixer.GetFloat("MasterVolume", out value))
                throw new System.Exception("Couldn't get Master volume");
            return value - Offset;
        }
        set {
            if (!MasterMixer.SetFloat("MasterVolume", value + Offset))
                throw new System.Exception("Couldn't set Master volume");
        }
    }
    public static AudioMixer MasterMixer { get; private set; }

	// Use this for initialization
	void Start () {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(ValueChanged);
        MasterMixer = Resources.Load<AudioMixer>("MasterMixer");
        slider.value = MasterVolume;
    }

    void ValueChanged(float val) {
        MasterVolume = val;
    }
}
