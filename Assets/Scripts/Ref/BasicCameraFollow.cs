using UnityEngine;
using System.Collections;
using Cinemachine;
using XInputDotNetPure;


public class BasicCameraFollow : MonoBehaviour 
{
    [SerializeField]
    CinemachineVirtualCamera cam;

    bool playerIndexSet = false;
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;
    bool shake = false;

    private void Start()
    {
        EndShake();
    }

    void Update()
    {
        if (!playerIndexSet || !prevState.IsConnected)
        {
            for (int i = 0; i < 4; ++i)
            {
                PlayerIndex testPlayerIndex = (PlayerIndex) i;
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                if (testState.IsConnected)
                {
                    playerIndex = testPlayerIndex;
                    playerIndexSet = true;
                }
            }
        }

        prevState = state;
        state = GamePad.GetState(playerIndex);
    }

    public void StartShake(CameraShakeSO Intensity)
    {
        StopAllCoroutines();
        EndShake();

        cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = Intensity.AmplitudeGain;
        cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = Intensity.FrequencyGain;
        GamePad.SetVibration(playerIndex, Intensity.ControllerIntensity.x, Intensity.ControllerIntensity.y);
        if (Intensity.Duration > 0.0f)
        {
            StartCoroutine(WaitEndShake(Intensity.Duration));
        }
    }

    public void EndShake()
    {
        cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0.0f;
        cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0.0f;
        GamePad.SetVibration(playerIndex, 0.0f, 0.0f);
    }

    IEnumerator WaitEndShake(float duration)
    {
        yield return new WaitForSeconds(duration);
        EndShake();
    }

}

