using UnityEngine;
public class TargetFrame : MonoBehaviour
{
    void Awake()
    {
        Application.targetFrameRate = 90;
    }
}