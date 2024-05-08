using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake Instance { get; private set; }

    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private CinemachineBasicMultiChannelPerlin channelPerlin;

    private float shakeTimer = 0;
    private float shakeTimerTotal;
    private float startingIntensity;

    [SerializeField]
    private Image damageBackground;

    private void Awake()
    {
        Instance = this;

        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        channelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void ShakeCamera(float intensity, float time)
    {
        channelPerlin.m_AmplitudeGain = intensity;

        startingIntensity = intensity;
        shakeTimerTotal = time;
        shakeTimer = time;
    }

    public void ShowDamageBackground()
    {
        DOTween.Sequence().AppendCallback(() => damageBackground.gameObject.SetActive(true))
                          .AppendInterval(.1f)
                          .Append(damageBackground.DOFade(0f, .2f))
                          .AppendCallback(() => { damageBackground.gameObject.SetActive(false);
                              damageBackground.DOFade(.1f, 0f);
                          });
    }

    void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            channelPerlin.m_AmplitudeGain = Mathf.Lerp(startingIntensity, 0f, 1 - (shakeTimer / shakeTimerTotal));
        }
    }
}
