using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using agora_gaming_rtc;
using agora_utilities;

public class GameController : MonoBehaviour
{
  [SerializeField]
  private string AppID = " ";

  [SerializeField]
  private GameObject videoSphere = null;

  [SerializeField]
  private string channelName = "Test1";

  static VideoCall videoSystem = null;

  // Start is called before the first frame update
  void Start()
  {
    videoSystem = new VideoCall();
    if (videoSystem != null)
    {
      videoSystem.InitEngine(AppID);
      videoSystem.SetSphereGameObject(videoSphere);
      videoSystem.JoinChannel(channelName);
    }
  }

  // Update is called once per frame
  void Update()
  {

  }



}
