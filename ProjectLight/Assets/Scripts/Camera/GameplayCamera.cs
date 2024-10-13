using Cinemachine;
using UnityEngine;

public class GameplayCamera : MonoBehaviour
{
    private CinemachineConfiner2D m_confiner_2d;

    private void Awake()
    {
        m_confiner_2d = GetComponent<CinemachineConfiner2D>();
    }

    private void Start()
    {
        GetSceneCinemachineConfiner();
    }

    private void GetSceneCinemachineConfiner()
    {
        var obj = GameObject.FindGameObjectWithTag("CinemachineConfiner");
        if (obj == null)
        {
            return;
        }

        m_confiner_2d.InvalidateCache();
        m_confiner_2d.m_BoundingShape2D = obj.GetComponent<Collider2D>();
    }
}
