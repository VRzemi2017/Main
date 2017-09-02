using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ResultManager : MonoBehaviour {

    [SerializeField] List<ScoreSetting> score = new List<ScoreSetting>();
    [SerializeField] List<CommentSetting> comment = new List<CommentSetting>();

    public enum ScoreType
    {
        SCORE_D,
        SCORE_C,
        SCORE_B,
        SCORE_A,
        SCORE_S,
        SCORE_SS,
    }

    public enum ConditionType
    {
        CONDI_GEM,
        CONDI_DAMEGE,
        CONDI_TELEPORT,
        CONDI_SPOT,
    }

    public enum OPType
    {
        OP_EQUAL,
        OP_NOT_EQUAL,
        OP_GREATER,
        OP_GEQUAL,
        OP_LESS,
        OP_LEQUAL,
    }

    [System.Serializable]
    public struct ScoreSetting
    {
        public ScoreType Score;
        public ScoreCondition[] conditions;
    }

    [System.Serializable]
    public struct ScoreCondition
    {
        public ConditionType Type;
        public OPType OP;
        public int Param;
    }

    [System.Serializable]
    public struct CommentSetting
    {
        [TextArea(3, 10)]
        public string Comment;
        public ScoreCondition[] conditions;
    }

    public ScoreType GetScore()
    {
        Sort();

        ScoreType result = ScoreType.SCORE_D;

        score.ForEach(s =>
        {
            bool pass = true;

            s.conditions.ToList().ForEach(c =>
            {

            });
        });

        return result;
    }

    private void Start()
    {
        GetScore();
    }

    [ContextMenu("Sort")]
    public void Sort()
    {
        score.Sort((ScoreSetting x, ScoreSetting y) =>
        {
            return x.Score.CompareTo(y.Score);
        });
    }

    private bool Compare<T>(T a, T b, OPType op)
    {

        //switch (op)
        //{
        //    case OPType.OP_EQUAL:
        //}

        return false;
    }
}
