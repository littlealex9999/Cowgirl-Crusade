using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CrosshairColors : MonoBehaviour
{
    [SerializeField] Image[] crosshairElements;
    [SerializeField] Image reticle;
    [SerializeField] Color standardColor = Color.green;
    [SerializeField] Color targetingEnemyColor = Color.red;
    [SerializeField] Color overheatedColor = Color.grey;

    [SerializeField] float tweenDuration = 0.5f;
    [SerializeField] float colorDelay = 0.02f;
    [SerializeField] float scaleDelay = 1f;

    float colorTimer = 0f;
    float scaleTimer = 0f;

    Player player;

    bool enemyDetected = false;
    bool shrinkReticle = false;
    bool canShoot = true;
    Tween scaleReticle;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();

        SetCrosshairColor(standardColor);
    }

    void Update()
    {
        if (colorTimer > 0) {
            colorTimer -= Time.deltaTime;
        }

        if (shrinkReticle) {
            if (scaleTimer > 0) {
                scaleTimer -= Time.deltaTime;
            } else {
                shrinkReticle = false;
                ScaleReticle(1f, tweenDuration);
            }
        }
        

        if (canShoot) {
            if (player != null) {
                if (!enemyDetected) {
                    if (player.CursorOverEnemy) {
                        enemyDetected = true;
                        colorTimer = colorDelay;
                        scaleTimer = scaleDelay;
                        shrinkReticle = false;
                        SetCrosshairColor(targetingEnemyColor);
                        ScaleReticle(2f, tweenDuration);
                    }
                } else if (enemyDetected && colorTimer <= 0) {
                    if (!player.CursorOverEnemy) {
                        enemyDetected = false;
                        shrinkReticle = true;
                        SetCrosshairColor(standardColor);
                    }
                }
            }
        }
        
    }


    void SetCrosshairColor(Color color, bool tween = false)
    {
        if (tween) {
            foreach (Image i in crosshairElements) {
                i.DOColor(color, tweenDuration);
            }
        } else {
            foreach (Image i in crosshairElements) {
                i.color = color;
            }
        }

    }

    public void AbleToShoot(bool canShoot)
    {
        if (canShoot) {
            SetCrosshairColor(standardColor, true);
            FadeCrosshair(1f);
        } else {
            SetCrosshairColor(overheatedColor, true);
            FadeCrosshair(0.5f);
        }

        this.canShoot = canShoot;

    }

    void FadeCrosshair(float alpha)
    {
        foreach (Image i in crosshairElements) {
            i.DOFade(alpha, tweenDuration);
        }
    }

    void ScaleReticle(float scale, float duration)
    {
        scaleReticle = reticle.transform.DOScale(scale, duration);
    }
}
