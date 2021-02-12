using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] Canvas gameOverCanvas;
    [SerializeField] Canvas gunReticleCanvas;


    private void Start()
    {
        gameOverCanvas.enabled = false;
    }

    public void ProcessDeath()
    {
        Time.timeScale = 0;
        gameOverCanvas.enabled = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
