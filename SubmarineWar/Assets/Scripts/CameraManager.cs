using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 

public class CameraManager : MonoBehaviour
{
  public GameObject submarineCamera;
  public GameObject scopeCamera;

    void Start()
  {
    // 潜水艦内部と潜望鏡のカメラオブジェクトを取得
    submarineCamera = GameObject.Find("SubmarineCamera");
    scopeCamera = GameObject.Find("ScopeCamera");

    // 潜望鏡のカメラは、デフォルトで無効にしとく
    scopeCamera.SetActive(false);
    }

    // Update is called once per frame
    void Update()
  {
      // if(Input.GetKeyDown(KeyCode.Space))
      if(Keyboard.current.spaceKey.wasPressedThisFrame)
    {
      submarineCamera.SetActive(!submarineCamera.activeSelf);
      scopeCamera.SetActive(!scopeCamera.activeSelf);
    }   
    }
}
