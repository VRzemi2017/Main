using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDesignManager : MonoBehaviour {
    [SerializeField] List<LevelSetting> setting = new List<LevelSetting>();
    [SerializeField] List<RandomTable> randomTable = new List<RandomTable>();

    [System.Serializable]
    public struct LevelSetting
    {
        [TextArea(3, 10)]
        public string Memo;
        public GameObject[] StartPositionSet;
        public GaneratorController GeneratorSetting;
    }

    [System.Serializable]
    public struct RandomTable
    {
        public int[] Sequence;
    }

    private void Awake()
    {

    }

    private void Start()
    {
        
    }

    [ContextMenu("GenerateRandomTable")]
    public void GenerateRandomTable()
    {
        List<int> table = new List<int>();

        for (int i = 0; i < setting.Count; ++i)
        {
            int tmp = 0;
            do
            {
                tmp = UnityEngine.Random.Range(0, setting.Count);
            } while (table.Contains(tmp));

            table.Add(tmp);
        }

        randomTable.Add(new RandomTable() { Sequence = table.ToArray()});
    }

    public System.Nullable<LevelSetting> GetLevelSetting(int index)
    {
        if (index >= 0 && index < setting.Count)
        {
            return setting[index];
        }

        return null;
    }

    public RandomTable RandomPickTable()
    {
        if (randomTable.Count == 0)
        {
            GenerateRandomTable();
        }

        return randomTable[UnityEngine.Random.Range(0, randomTable.Count)];
    }
}
