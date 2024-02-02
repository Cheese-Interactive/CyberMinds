using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firewall : MonoBehaviour {

    [Header("References")]
    private DefenderController defenderController;

    private void Start() {

        defenderController = FindObjectOfType<DefenderController>();

    }

    private void LateUpdate() {

        transform.position = defenderController.transform.position;

    }
}
