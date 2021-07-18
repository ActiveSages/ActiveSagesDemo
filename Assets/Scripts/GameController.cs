using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if(UNITY_2018_3_OR_NEWER && UNITY_ANDROID)
using UnityEngine.Android;
#endif


public class GameController : MonoBehaviour
{
    // Use this for initialization
#if (UNITY_2018_3_OR_NEWER && UNITY_ANDROID)
    private ArrayList permissionList = new ArrayList();
#endif

    [SerializeField]
    private string AppID = "aab8b8f5a8cd4469a63042fcfafe7063";

    [SerializeField]
    private GameObject videoSphere = null;

    [SerializeField]
    private string channelName = "Test1";

    static VideoCall videoCallSystem = null;

    void Awake()
    {
#if (UNITY_2018_3_OR_NEWER && UNITY_ANDROID)
		permissionList.Add(Permission.Microphone);         
		permissionList.Add(Permission.Camera);               
#endif
    }

    // Start is called before the first frame update
    void Start()
    {
        videoCallSystem = new VideoCall();
        if (videoCallSystem != null)
        {

            videoCallSystem.SetSphereGameObject(videoSphere);
            videoCallSystem.InitEngine(AppID);
            videoCallSystem.JoinChannel(channelName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckPermissions();
    }

    void OnApplicationQuit()
    {
        if (videoCallSystem != null)
        {
            videoCallSystem.DestroyEngine();
        }
    }

    private void CheckPermissions()
    {
#if (UNITY_2018_3_OR_NEWER && UNITY_ANDROID)
        foreach(string permission in permissionList)
        {
            if (!Permission.HasUserAuthorizedPermission(permission))
            {                 
				Permission.RequestUserPermission(permission);
			}
        }
#endif
    }

}
