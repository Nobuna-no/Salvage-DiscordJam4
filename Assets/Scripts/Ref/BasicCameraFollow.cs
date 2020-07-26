using UnityEngine;
using System.Collections;
using Cinemachine;

public class BasicCameraFollow : MonoBehaviour 
{
    [SerializeField]
    CinemachineVirtualCamera cam;

    float AmplitudeGain = 0.5f;
    float FrequencyGain = 0.5f;

    private void Start()
    {
        EndShake();
    }

    public void StartShake()
    {
        cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = AmplitudeGain;
        cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = FrequencyGain;
    }

    public void EndShake()
    {
        cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0.0f;
        cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0.0f;
    }

}

