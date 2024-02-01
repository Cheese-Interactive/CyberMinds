using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterPowerup : Powerup {

    protected override void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.CompareTag("Defender")) {

            defenderController.SetHasFilter(true);
            Invoke("ResetEffect", powerupLength);
            DisableCollisions();

        }
    }

    protected override void ResetEffect() {

        defenderController.SetHasFilter(false);

    }
}
