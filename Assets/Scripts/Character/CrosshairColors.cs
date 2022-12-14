using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CrosshairColors : MonoBehaviour
{
    [SerializeField] Image crosshair, hitmarker;
    [SerializeField] Color standardColor = Color.green;
    [SerializeField] Color targetingEnemyColor = Color.red;
    
    [SerializeField] float tweenDuration = 0f;
    [SerializeField] float delay = 0.02f;

    float timer = 0;

    Player player;
    bool enemyDetected = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();

        SetCrosshairColor(standardColor);
    }

    void Update()
    {
        if (timer > 0) {
            timer -= Time.deltaTime;
        }

        if (player != null) {
            if (!enemyDetected) {
                if (player.CursorOverEnemy) {
                    enemyDetected = true;
                    timer = delay;
                    SetCrosshairColor(targetingEnemyColor);
                }
            }else if (enemyDetected && timer <= 0) {
                if (!player.CursorOverEnemy) {
                    enemyDetected = false;
                    SetCrosshairColor(standardColor);
                }
            }
        }
    }


    void SetCrosshairColor(Color color)
    {
        crosshair.color = color;
        hitmarker.color = color;

        //crosshair.DOColor(color, tweenDuration);
        //hitmarker.DOColor(color, tweenDuration);
    }

}
