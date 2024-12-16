using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public TMP_Text Text;
    Sequence _tween;

    public void InitSmall(float amount, Color color, bool isCrit = false)
    {
        transform.localScale = Vector3.one;
        transform.localPosition = new Vector3(transform.localPosition.x + Random.Range(-1f, 1f), transform.localPosition.y, transform.localPosition.z);

        Text.alpha = 1;
        Text.text = amount.ToString();
        Text.color = color;

        if (_tween.IsActive()) _tween.Kill();

        _tween = DOTween.Sequence();

        _tween
            .Join(transform.DOPunchScale(new Vector3(1.2f, 1.2f, 1.2f), .1f))
            .Join(transform.DOLocalMoveY(transform.localPosition.y + 2f, .5f)).SetEase(Ease.InQuad)
            .Append(Text.DOFade(0, .2f))
            .OnComplete(() =>
            {
            });
    }
}
