using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class State
{
    public string name;

    public GameObject[] stateSprites;
    public GameObject stateParticle;

    public MonoBehaviour[] scripts;
}
