using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ResultManager : MonoBehaviour {

    [SerializeField] List<ScoreSetting> scoreSetting = new List<ScoreSetting>();
    [SerializeField] List<CommentSetting> commentSetting = new List<CommentSetting>();

    private ScoreType score = ScoreType.SCORE_D;
    public ScoreType Score { get { return score; } }

    private List<string> comment = new List<string>();
    public string[] Comment { get { return comment.ToArray(); } }

    public int GemCount 
    {
        get 
        {
            PlayerManager player = GameObject.FindObjectOfType<PlayerManager>();
            if (player)
            {
                return player.GEM_NUM;
            }

            return 0;
        }
    }
    public int DamageCount 
    {
        get 
        {
            CharaManager chara = GameObject.FindObjectOfType<CharaManager>();
            if (chara)
            {
                return 0;
            }

            return 0;
        }
    }
    public int TeleportCount 
    {
        get 
        {
            LineRendererController line = GameObject.FindObjectOfType<LineRendererController>();
            if (line)
            {
                return line.TeleportCount;
            }

            return 0;
        }
    }

    private List<SpotControl> spots = new List<SpotControl>();
    public SpotControl[] Spots { get { return spots.ToArray(); } }

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

    private void Start()
    {
        ComputeScore();
        ComputeComment();
    }

    public void ComputeScore()
    {
        Sort();

        scoreSetting.ForEach(s =>
        {
            bool pass = true;

            s.conditions.ToList().ForEach(c =>
            {
                pass &= CheckCondition(c);
            });

            if (pass)
            {
                score = s.Score;
            }
        });
    }

    public void ComputeComment()
    {
        commentSetting.ForEach(s =>
        {
            bool pass = true;

            s.conditions.ToList().ForEach(c =>
            {
                pass &= CheckCondition(c);
            });

            if (pass)
            {
                AddComment(s.Comment);
            }
        });
    }

    [ContextMenu("Sort")]
    public void Sort()
    {
        scoreSetting.Sort((ScoreSetting x, ScoreSetting y) =>
        {
            return x.Score.CompareTo(y.Score);
        });
    }

    public void AddComment(string str)
    {
        comment.Add(str);
    }

    public void AddSpot(SpotControl spot)
    {
        if (spot && !spots.Contains(spot))
        {
            spots.Add(spot);
        }
    }

    private bool CheckCondition(ScoreCondition sc)
    {
        switch (sc.Type)
        {
            case ConditionType.CONDI_DAMEGE:
                {
                    return CompareCondition(sc.OP, DamageCount, sc.Param);
                }
            case ConditionType.CONDI_GEM:
                {
                    return CompareCondition(sc.OP, GemCount, sc.Param);
                }
            case ConditionType.CONDI_SPOT:
                {
                    bool result = false;

                    spots.ForEach(s => 
                    {
                        result |= CompareCondition(OPType.OP_EQUAL, s.SpotID, sc.Param);
                    });

                    return result;
                }
            case ConditionType.CONDI_TELEPORT:
                {
                    return CompareCondition(sc.OP, TeleportCount, sc.Param);
                }
        }

        return false;
    }

    private bool CompareCondition(OPType op, int source, int param)
    {
        switch (op)
        {
            case OPType.OP_EQUAL:
                {
                    return source == param;
                }
            case OPType.OP_GEQUAL:
                {
                    return source >= param;
                }
            case OPType.OP_GREATER:
                {
                    return source > param;
                }
            case OPType.OP_LEQUAL:
                {
                    return source <= param;
                }
            case OPType.OP_LESS:
                {
                    return source < param;
                }
            case OPType.OP_NOT_EQUAL:
                {
                    return source != param;
                }
        }

        return false;
    }
}
