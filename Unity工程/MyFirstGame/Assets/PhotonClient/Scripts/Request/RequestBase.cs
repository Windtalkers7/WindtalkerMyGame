using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using ExitGames.Client.Photon;

public abstract class RequestBase : MonoBehaviour
{
    [HideInInspector]
    public OperationCode operationCode;

    public virtual void Awake()
    {
        Init();
    }

    protected abstract void Init();

    public abstract void DefaultRequest();

    public abstract void OnOperationRespons(OperationResponse operationResponse);


    public virtual void Start()
    {
        PhotonEngine.Instance.AddRequest(this);         
    }

    public  virtual void OnDestory()
    {
        PhotonEngine.Instance.RemoveRequest(this);
    }

}
