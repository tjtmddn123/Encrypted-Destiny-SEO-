using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    // Singleton 인스턴스
    private static DataManager instance;

    // Singleton 인스턴스에 접근하기 위한 속성
    public static DataManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DataManager>();
                if (instance == null)
                {
                    GameObject container = new GameObject("DataManager");
                    instance = container.AddComponent<DataManager>();
                }
                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }

    string GameDataFileName = "GameData.json";

    
    [System.Serializable]
    public class GameObjectState
    {
        public string gameObjectName;
        public Vector3 position;
        public Quaternion rotation;
        

        public GameObjectState(string name, Vector3 pos, Quaternion rot)
        {
            gameObjectName = name;
            position = pos;
            rotation = rot;
        }
    }

    public List<GameObjectState> gameObjectStates = new List<GameObjectState>();

    [SerializeField]
    private GameObject gameObjectToAdd; // 추가할 게임 오브젝트를 Inspector에서 설정하기 위한 변수

    public void AddGameObjectState()
    {
        if (gameObjectToAdd != null)
        {
            GameObjectState state = new GameObjectState(gameObjectToAdd.name, gameObjectToAdd.transform.position, gameObjectToAdd.transform.rotation);
            gameObjectStates.Add(state);
        }
        else
        {
            Debug.LogWarning("GameObject to add is not assigned.");
        }
    }

    public void SaveGameData()
    {
        string ToJsonData = JsonUtility.ToJson(gameObjectStates, true);
        string filePath = Application.persistentDataPath + "/" + GameDataFileName;
        Debug.Log("저장");
        File.WriteAllText(filePath, ToJsonData);
    }

    public void LoadGameData()
    {
        string filePath = Application.persistentDataPath + "/" + GameDataFileName;

        if (File.Exists(filePath))
        {
            string FromJsonData = File.ReadAllText(filePath);
            gameObjectStates = JsonUtility.FromJson<List<GameObjectState>>(FromJsonData);
            Debug.Log("불러오기 완료");
        }
    }

    // 게임 종료 시 호출되는 함수
    private void OnDestroy()
    {
        SaveGameData();
    }
}
;