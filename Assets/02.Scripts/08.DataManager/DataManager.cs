using UnityEngine;
using System.Collections.Generic;
using System.IO;


public class DataManager : MonoBehaviour
{
    public List<GameObject> targetObjects; // 저장할 게임 오브젝트 리스트

    private List<ObjectState> objectStates = new List<ObjectState>(); // 게임플레이중 발생한 이벤트 리스트

    [System.Serializable]
    public class ObjectState
    {
        public string objectName;
        public bool isActive;

        public ObjectState(string objectName, bool isActive)
        {
            this.objectName = objectName;
            this.isActive = isActive;
        }
    }

    private void Start()
    {
        LoadGameData();
    }

    private void Update()
    {
        // 게임 플레이 중 발생한 이벤트를 기록
        RecordEvent();
    }

    private void RecordEvent()
    {
        objectStates.Clear();

        foreach (GameObject obj in targetObjects)
        {
            bool isActive = obj.activeSelf;
            objectStates.Add(new ObjectState(obj.name, isActive));
        }
        Debug.Log("이벤트 로그 저장완료.");
    }

    private void OnApplicationQuit()
    {
        SaveGameData();
    }

    private void SaveGameData() // 데이터 저장
    {
        List<TransformData> transformDataList = new List<TransformData>();

        
        foreach (GameObject obj in targetObjects)
        {
            Vector3 position = obj.transform.position;
            Quaternion rotation = obj.transform.rotation;
            bool isActive = obj.activeSelf;
            transformDataList.Add(new TransformData(obj.name, position, rotation.eulerAngles, isActive));
        }

        string eventJson = JsonUtility.ToJson(objectStates);
        string eventLogFilePath = Application.persistentDataPath + "/EventLog.json";
        File.WriteAllText(eventLogFilePath, eventJson);
        Debug.Log("이벤트 로그 저장.");

        string json = JsonUtility.ToJson(new TransformDataList(transformDataList));

        
        string filePath = Application.persistentDataPath + "/GameData.json";
        File.WriteAllText(filePath, json);

        Debug.Log(" 저장됨 " + filePath);
    }

    private void LoadGameData() // 데이터 로드
    {
        string filePath = Application.persistentDataPath + "/GameData.json";

        
        if (File.Exists(filePath))
        {
            
            string json = File.ReadAllText(filePath);


            TransformDataList data = JsonUtility.FromJson<TransformDataList>(json);

            
            foreach (TransformData transformData in data.transformDataList)
            {
                GameObject obj = targetObjects.Find(x => x.name == transformData.objectName);
                if (obj != null)
                {
                    obj.transform.position = transformData.position;
                    obj.transform.rotation = Quaternion.Euler(transformData.rotation);
                    obj.SetActive(transformData.isActive);
                }
            }

            Debug.Log(" 불러옴 " + filePath);
        }
        else
        {
            Debug.Log(" 저장된 데이터 없음");
        }

        string eventLogFilePath = Application.persistentDataPath + "/EventLog.json";
        if (File.Exists(eventLogFilePath))
        {
            string eventJson = File.ReadAllText(eventLogFilePath);
            objectStates = JsonUtility.FromJson<List<ObjectState>>(eventJson);

            
            ReplayEvents();
        }

    }
    private void ReplayEvents()
    {
        foreach (ObjectState state in objectStates)
        {
            GameObject obj = targetObjects.Find(x => x.name == state.objectName);
            if (obj != null)
            {
                obj.SetActive(state.isActive);
            }
        }
    }
   
}

[System.Serializable]
public class TransformData // 저장할 게임오브젝트 정보 Position, Rotation
{
    public string objectName;
    public Vector3 position;
    public Vector3 rotation;
    public bool isActive;

    public TransformData(string objectName, Vector3 position, Vector3 rotation, bool isActive)
    {
        this.objectName = objectName;
        this.position = position;
        this.rotation = rotation;
        this.isActive = isActive;
    }
}

[System.Serializable]
public class TransformDataList // 리스트로 만들어서 저장
{
    public List<TransformData> transformDataList;

    public TransformDataList(List<TransformData> transformDataList)
    {
        this.transformDataList = transformDataList;
    }
}