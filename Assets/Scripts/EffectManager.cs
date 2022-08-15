using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    private CinemachineVirtualCamera m_camera;
    private Transform m_target;
    void Awake()
    {
        m_camera = GetComponent<CinemachineVirtualCamera>();
        m_target = m_camera.Follow;
    }
    
    public void FreezeTime(float _duration)
    {
        StartCoroutine(FreezeTimeCoroutine(_duration));
    }
    IEnumerator FreezeTimeCoroutine(float duration)
    {
        Time.timeScale = .0000001f;
        yield return new WaitForSeconds(duration * Time.timeScale);
        Time.timeScale = 1.0f;
    }
    public void Shake(float _duration, float _force)
    {
        StartCoroutine(ShakeCoroutine(_duration, _force));
    }
    IEnumerator ShakeCoroutine(float _duration, float _force)
    {
        var noise = m_camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        noise.m_AmplitudeGain = _force;
        yield return new WaitForSeconds(_duration);
        noise.m_AmplitudeGain = 0f;
    }

    public void SpawnFx(GameObject _fx, Vector3 _pos, Quaternion _rot, float _life)
    {
        GameObject instance = Instantiate(_fx, _pos, _rot);
        StartCoroutine(DestroyFx(instance, _life));
    }

    IEnumerator DestroyFx(GameObject _fxInstance, float _life)
    {
        yield return new WaitForSeconds(_life);
        Destroy(_fxInstance);
    }
}