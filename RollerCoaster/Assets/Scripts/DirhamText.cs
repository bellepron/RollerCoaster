using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class DirhamText : MonoBehaviour, IEndOfRideObserver
{
    [SerializeField] TextMeshProUGUI addDirhamText;
    Vector3 addDirhamTextPos;

    void Start()
    {
        GameManager.Instance.AddEndOfRideObserver(this);
        addDirhamTextPos = addDirhamText.transform.position;
        addDirhamText.enabled = false;
    }
    public void GoNextRide_IfExists()
    {
        addDirhamText.text = Calculator.Instance.dailyRideEarnings[Globals.dailyWorkCount].ToString();
        addDirhamText.transform.position = addDirhamTextPos;
        addDirhamText.enabled = true;

        Sequence seq = DOTween.Sequence();

        seq.Append(addDirhamText.transform.DOMove(addDirhamText.transform.position + new Vector3(0, 180, 0), 1.2f)).OnComplete(() => addDirhamText.enabled = false);
        seq.Append(transform.DOScale(Vector3.one * 1.5f, 0.2f));
        seq.Append(transform.DOScale(Vector3.one, 0.1f));

        // transform.DOScale(Vector3.one * 1.5f, 0.2f).OnComplete(() => transform.DOScale(Vector3.one, 0.1f));
    }
}
