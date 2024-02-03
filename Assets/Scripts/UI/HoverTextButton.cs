using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverTextButton : CustomButton {

    [Header("References")]
    [SerializeField] private TMP_Text text;

    [Header("Animations")]
    [SerializeField] private Color hoverTextColor;
    [SerializeField] private float hoverFadeDuration;
    private Color startTextColor;

    private void Start() {

        startTextColor = text.color;

    }

    protected override void OnPointerEnter(PointerEventData eventData) {

        text.DOColor(hoverTextColor, hoverFadeDuration).SetUpdate(true); // set update to true to ignore time scale

    }

    protected override void OnPointerExit(PointerEventData eventData) {

        text.DOColor(startTextColor, hoverFadeDuration).SetUpdate(true); // set update to true to ignore time scale

    }
}
