using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullscreenMap : MonoBehaviour
{
    public static FullscreenMap Instance { get; private set; }

    public MinimapSystem.MinimapSystemSetting MinimapSystemSettings;
    public RectTransform Arrow;

    void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }
        public void UpdateForPlayerTransform(Transform playerTransform)
    {
        //MinimapSystem.Render(m_RT, playerTransform.position, playerTransform.forward, MinimapSystemSettings);

        //if (MinimapSystemSettings.isFixed)
        //{
        //    Arrow.rotation = Quaternion.Euler(0, 0, Vector3.SignedAngle(playerTransform.forward, Vector3.forward, Vector3.up));
        //}
        //else
        //{
        //   Arrow.rotation = Quaternion.identity;
        //}
    }
}
