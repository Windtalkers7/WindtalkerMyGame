using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using ExitGames.Client.Photon;

public abstract class EventBase : MonoBehaviour
{
    public EventCode eventCode;

    protected virtual void Awake()
    {
        Init();
    }

    protected abstract void Init();

    public abstract void OnEvent(EventData eventData);

    protected virtual void Start()
    {
        PhotonEngine.Instance.AddEvent(this);
    }

    protected virtual void OnDestroy()
    {
        PhotonEngine.Instance.RemoveEvent(this);
    }
}
