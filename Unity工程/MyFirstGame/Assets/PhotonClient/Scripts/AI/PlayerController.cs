using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Common;
using Common.Tools;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private static PlayerController instance;
    public static PlayerController Instance
    {
        get { return instance;}
        
    }

    [SerializeField] private GameObject playerPrefab;
    private GameObject player;
    private NewPlayerEvent newPlayerEvent;
    private SyncPositionEvent syncPositionEvent;
    private SyncPlayerRequest syncPlayerRequest;
    private SyncPositionRequest syncPositionRequest;
    private Vector3 lastPosition;
    private Dictionary<string, GameObject> playerDic;

    private void Awake()
    {
        Init();
    }
    private void Init()
    {
        instance = this;
        syncPlayerRequest = gameObject.AddComponent<SyncPlayerRequest>();
        syncPositionRequest = gameObject.AddComponent<SyncPositionRequest>();
        syncPositionEvent = gameObject.AddComponent<SyncPositionEvent>();
        newPlayerEvent = gameObject.AddComponent<NewPlayerEvent>();
        lastPosition = Vector3.zero;
        playerDic = new Dictionary<string, GameObject>();
    }

    private void Start()
    {
        player = CreatPlayer();
        player.transform.GetComponent<MeshRenderer>().material.color = Color.red;
        syncPlayerRequest.DefaultRequest();
        StartCoroutine(RequestPlayerSyncPosition());
    }

    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        player.transform.Translate(new Vector3(h, 0, v) * Time.deltaTime * 4);
    }

    int count = 0;
    IEnumerator RequestPlayerSyncPosition()
    {
        yield return new WaitForSeconds(1);
        while (true)
        {
            if (Vector3.Distance(player.transform.position, lastPosition) > 0.01f)
            {
                lastPosition = player.transform.position;
                syncPositionRequest.pos = player.transform.position;
                syncPositionRequest.DefaultRequest();                
            }
            count++;
            yield return new WaitForSeconds(0.02f);
        }
    }

    public void OnNewPlayerEvent(string username)
    {
        GameObject go = GameObject.Instantiate(playerPrefab);
        playerDic.Add(username, go);
    }


    public void OnSyncPlayerResponse(List<string> usernameList)
    {
        foreach (var username in usernameList)
        {
            OnNewPlayerEvent(username);
        }
    }

    public void OnSyncPositionEvent(List<PlayerData> playerDataList)
    {
        foreach (PlayerData playerData in playerDataList)
        {
            GameObject go = DicTool.GetValue<string, GameObject>(playerDic, playerData.Username);
            if (go != null)
                go.transform.position = new Vector3() { x = playerData.Pos.X, y = playerData.Pos.Y, z = playerData.Pos.Z };
        }
    }

    private GameObject CreatPlayer()
    {
        GameObject go = GameObject.Instantiate(playerPrefab);     
        return go;
    }
}
