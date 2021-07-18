using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using agora_gaming_rtc;
using agora_utilities;

public class VideoCall
{
    // instance of agora engine
    private IRtcEngine m_RtcEngine;
    private Text MessageText;

    [SerializeField]
    GameObject videoSphere = null;


    public void InitEngine(string appID)
    {
        //  Start the RTC Engine
        if (m_RtcEngine != null)
        {
            Debug.Log("An instance of the engine has been started already. Please unload it first!");
            return;
        }

        m_RtcEngine = IRtcEngine.GetEngine(appID);
        Debug.Log("Succesfully loaded engine");

        //  Set up flags for debugging/logging
        m_RtcEngine.SetLogFilter(LOG_FILTER.DEBUG | LOG_FILTER.INFO | LOG_FILTER.WARNING | LOG_FILTER.ERROR | LOG_FILTER.CRITICAL);

    }

    public void DestroyEngine()
    {
        Debug.Log("calling unloadEngine");

        // delete
        if (m_RtcEngine != null)
        {
            IRtcEngine.Destroy();  // Place this call in ApplicationQuit
            m_RtcEngine = null;
        }
    }

    public string GetSdkVersion()
    {
        string ver = IRtcEngine.GetSdkVersion();
        return ver;
    }

    public int JoinChannel(string channelName)
    {
        Debug.Log("Joining channel: " + channelName);

        if (m_RtcEngine == null)
        {
            return 0;
        }

        //  Setting callbacks. This could probably be handled better using dedicated functions
        m_RtcEngine.OnJoinChannelSuccess = onJoinChannelSucces;
        m_RtcEngine.OnUserJoined = onUserJoinedChannel;
        //m_RtcEngine.OnUserOffline = Callback to handle user offline;
        //m_RtcEngine.OnWarning = Callback To handle Warnings;
        //m_RtcEngine.OnError = Callback to handle errors;

        // enable video
        m_RtcEngine.EnableVideo();
        // allow camera output callback
        m_RtcEngine.EnableVideoObserver();

        return m_RtcEngine.JoinChannel(channelName, null, 0);
    }

    public void LeaveChannel()
    {
        if (m_RtcEngine == null)
            return;

        // leave channel
        m_RtcEngine.LeaveChannel();
        // deregister video frame observers in native-c code
        m_RtcEngine.DisableVideoObserver();
    }

    //  Callback functions for the event handler
    private void onJoinChannelSucces(string channelName, uint channelID, int timeElapsed)
    {
        Debug.Log("Succesfully joined: " + channelName + ". ID: " + channelID);
    }

    private const float Offset = 100;
    private void onUserJoinedChannel(uint uid, int timeElapsed)
    {
        Debug.Log("User " + uid + " joined the channel.");

        if (videoSphere == null)
        {
            return;
        }
        videoSphere.name = uid.ToString();
        //videoSphere.AddComponent<RawImage>();
        videoSphere.transform.Rotate(0f, 0.0f, 180.0f);

        VideoSurface sphereVideoSurface = videoSphere.AddComponent<VideoSurface>();
    

        if (!ReferenceEquals(sphereVideoSurface, null))
        {
            // configure videoSurface
            sphereVideoSurface.SetForUser(uid);
            sphereVideoSurface.SetEnable(true);
            sphereVideoSurface.SetVideoSurfaceType(AgoraVideoSurfaceType.Renderer);
            sphereVideoSurface.SetGameFps(30);

        }
    }

    public VideoSurface SetVideoOnSphere(string goName)
    {
        if (videoSphere == null)
        {
            return null;
        }

        videoSphere.name = goName;
        videoSphere.AddComponent<RawImage>();
        VideoSurface sphereVideoSurface = videoSphere.AddComponent<VideoSurface>();
        return sphereVideoSurface;
    }

    public void SetSphereGameObject(GameObject _sphere)
    {
        videoSphere = _sphere;
    }
}
