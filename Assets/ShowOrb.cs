using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowOrb : MonoBehaviour
{
    [SerializeField] Image orbImg;
    [SerializeField] RectTransform orbTransform;

    [SerializeField] TextMeshProUGUI orbName;
    [SerializeField] TextMeshProUGUI orbQuantity;

    [SerializeField] GameObject orbGameObject;
    [SerializeField] GameObject orbInfoContainerGameObject;


    [SerializeField] AnimationCurve animCurve;
    Coroutine currentCoroutine;

    public void UpdateOrbDetails(Sprite _orbImg, string _orbName, string _quantity)
    {
        orbGameObject.SetActive(true);
        orbInfoContainerGameObject.SetActive(false);

        orbImg.sprite = _orbImg;
        orbName.text = _orbName;
        orbQuantity.text = _quantity;
        orbTransform.localScale = new Vector3(0, 0, 0);
        if (currentCoroutine == null)
            currentCoroutine = StartCoroutine(Animation());
    }

    IEnumerator Animation()
    {
        for (float i = 0; i < 1; i += Time.deltaTime)
        {
            orbTransform.localScale = Vector3.one * animCurve.Evaluate(i);
            yield return null;
        }
        orbTransform.localScale = Vector3.one;
        orbInfoContainerGameObject.SetActive(true);
        currentCoroutine = null;
    }
}
