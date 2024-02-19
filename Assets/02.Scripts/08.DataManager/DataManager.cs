//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System.IO;
//#if UNITY_EDITOR
//using UnityEditor;
//#endif

//public class DataManager : MonoBehaviour
//{
//    static GameObject container;
//    static DataManager instance;

//    public static DataManager Instance
//    {
//        get
//        {
//            if (!instance)
//            {
//                container = new GameObject();
//                container.name = "DataManager";
//                instance = container.AddComponent(typeof(DataManager)) as DataManager;
//                DontDestroyOnLoad(container);
//            }
//            return instance;
//        }
//    }

//    string GameDataFileName = "GameData.json";

//    [System.Serializable]
//    public class GameObjectState
//    {
//        public GameObject gameObject;
//        public string gameObjectName;
//        public Vector3 position;
//        public Quaternion rotation;
       
//    }

//    public List<GameObjectState> gameObjectStates = new List<GameObjectState>();

//    public void LoadGameData()
//    {
//        string filePath = Application.persistentDataPath + "/" + GameDataFileName;

//        if (File.Exists(filePath))
//        {
//            string FromJsonData = File.ReadAllText(filePath);
//            gameObjectStates = JsonUtility.FromJson<List<GameObjectState>>(FromJsonData);
//            print("불러오기 완료");
//        }
//    }

//    public void SaveGameData()
//    {
//        string ToJsonData = JsonUtility.ToJson(gameObjectStates, true);
//        string filePath = Application.persistentDataPath + "/" + GameDataFileName;
//        File.WriteAllText(filePath, ToJsonData);
//    }

//    private void Start()
//    {
//        DataManager.Instance.LoadGameData();
//    }

//    private void OnApplicationQuit()
//    {
//        DataManager.Instance.SaveGameData();
//    }

//    public class DataManagerEditor : Editor
//    {
//        public override void OnInspectorGUI()
//        {
//            base.OnInspectorGUI();

//            DataManager dataManager = (DataManager)target;

           
//            if (GUILayout.Button("Add GameObject"))
//            {
//                GameObject selectedObject = Selection.activeGameObject;
//                if (selectedObject != null)
//                {
//                    dataManager.gameObjectStates.Add(new DataManager.GameObjectState
//                    {
//                        gameObject = selectedObject,
//                        gameObjectName = selectedObject.name,
//                        position = selectedObject.transform.position,
//                        rotation = selectedObject.transform.rotation
//                    });
//                }
//            }

           
//            EditorGUILayout.Space();
//            EditorGUILayout.LabelField("Game Object States", EditorStyles.boldLabel);
//            foreach (var state in dataManager.gameObjectStates)
//            {
//                EditorGUILayout.LabelField(state.gameObjectName);
//            }
//        }
//    }

//}
