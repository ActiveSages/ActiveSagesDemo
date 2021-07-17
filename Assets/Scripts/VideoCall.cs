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



    public void InitEngine(string appID)
    {
        //  Start the RTC Engine
        if(m_RtcEngine != null)
        {
            Debug.Log("An instance of the engine has been started already. Please unload it first!");
            return;
        }

        m_RtcEngine = IRtcEngine.GetEngine(appID);

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

        return m_RtcEngine.JoinChannel(channelName, null, 0);

    }

    //  Callback functions for the event handler
    private void onJoinChannelSucces(string channelName, uint channelID, int timeElapsed)
    {
        Debug.Log("Succesfully joined: " + channelName + ". ID: " + channelID);
    }

    private void onUserJoinedChannel(uint uid, int timeElapsed)
    {
        Debug.Log("User " + uid + " joined the channel.");

        //  Maybe spawn an object to render the view 
    }


}
