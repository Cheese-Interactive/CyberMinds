using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizePowerup : Powerup {

    [Header("Size")]
    [SerializeField] private Vector2 targetSize;
    [SerializeField] private float sizeDuration;
    private Vector2 startSize;
    private Tweener sizeTween;

    private void Start() {

        startSize = defenderController.transform.localScale;

    }

    protected override void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.CompareTag("Defender")) {

            DOTween.Kill(sizeTween);
            sizeTween = defenderController.transform.DOScale(targetSize, sizeDuration).SetEase(Ease.OutExpo).OnComplete(() => Invoke("ResetEffect", powerupLength));
            DisableCollisions();

        }
    }

    protected override void ResetEffect() {

        defenderController.transform.DOScale(startSize, sizeDuration).OnComplete(() => Destroy(gameObject));

    }
}
