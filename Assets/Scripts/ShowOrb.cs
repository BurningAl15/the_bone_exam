using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowOrb : MonoBehaviour
{
    [SerializeField] AnimationCurve animCurve;

    [SerializeField] Image orbImg;
    [SerializeField] RectTransform orbTransform;

    [SerializeField] TextMeshProUGUI orbName;
    [SerializeField] TextMeshProUGUI orbQuantity;
    [SerializeField] GameObject closeBtn;

    [Space]
    [SerializeField] GameObject orbGameObject;
    [SerializeField] GameObject orbInfoContainerGameObject;


    [Space]
    [SerializeField] ParticleSystem greenParticle;
    [SerializeField] ParticleSystem blueParticle, whiteParticle;
    Coroutine currentCoroutine;

    public void UpdateOrbDetails(Sprite _orbImg, string _orbName, string _quantity)
    {
        orbGameObject.SetActive(true);
        orbInfoContainerGameObject.SetActive(false);
        closeBtn.SetActive(false);

        orbImg.sprite = _orbImg;
        orbName.text = _orbName + " RUNE";
        orbQuantity.text = "Quantity: " + _quantity;
        orbTransform.localScale = new Vector3(0, 0, 0);

        greenParticle.gameObject.SetActive(true);
        whiteParticle.gameObject.SetActive(true);
        blueParticle.gameObject.SetActive(true);

        if (currentCoroutine == null)
            currentCoroutine = StartCoroutine(Animation());
    }

    IEnumerator Animation()
    {
        greenParticle.Play();
        whiteParticle.Play();
        blueParticle.Play();
        SoundManager._instance.PlayMoveSound(SoundTypes.WIN);

        yield return new WaitUntil(() => !greenParticle.IsAlive());
        yield return new WaitForSeconds(1f);
        greenParticle.gameObject.SetActive(false);
        whiteParticle.gameObject.SetActive(false);
        blueParticle.gameObject.SetActive(false);

        for (float i = 0; i < 1; i += Time.deltaTime)
        {
            orbTransform.localScale = Vector3.one * animCurve.Evaluate(i);
            yield return null;
        }
        orbTransform.localScale = Vector3.one;
        SoundManager._instance.PlayMoveSound(SoundTypes.WIN);

        orbInfoContainerGameObject.SetActive(true);
        closeBtn.SetActive(true);

        currentCoroutine = null;
    }
}