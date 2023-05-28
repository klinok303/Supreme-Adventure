using UnityEngine;

public class ReloadState : MonoBehaviour
{
    public State[] playerStates;

    byte curStatePointer = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (curStatePointer >= playerStates.Length - 1) curStatePointer = 0;
            else curStatePointer++;

            foreach (var state in playerStates)
            {
                foreach (var gameObj in state.stateSprites) gameObj.SetActive(false);
                foreach (var script in state.scripts) script.enabled = false;
            }

            Instantiate(playerStates[curStatePointer].stateParticle, transform.position, transform.rotation);

            foreach (var gameObj in playerStates[curStatePointer].stateSprites) gameObj.SetActive(true);
            foreach (var script in playerStates[curStatePointer].scripts) script.enabled = true;
        }
    }

    public byte GetStateID()
    {
        return curStatePointer;
    }
}
