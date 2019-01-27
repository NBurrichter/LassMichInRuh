using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverLastedSeconds : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<Text>().text = $"You lasted {SanityController.instance.TimeSane} seconds!";
    }
}
