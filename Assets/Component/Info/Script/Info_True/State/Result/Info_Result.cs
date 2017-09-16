using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class Info_Result : MonoBehaviour {
    private int _pick_up;
    private int _damage;
    private int _teleport;
    private int _event;

    [SerializeField] private SpriteRenderer MainScreen;
    [SerializeField] private SpriteRenderer SubScreen;
    [SerializeField] private float LineSpace;


    public int PickUp { set { _pick_up = value; } }
    public int Damage { set { _damage = value; } }
    public int Teleport { set { _teleport = value; } }

    public TextMesh Event1_text;
    public TextMesh Eval_Text;
    public TextMesh Comm_Text;

    // Use this for initialization
    void Start () {
        AdjustResult();

        //Debug
        /*List<string> comment = new List<string>();
        comment.Add("アイウエオアイウエオアオイエオアイウエオアイウエオ");
        comment.Add("アイウエオアイウエオアオイエオアイウエオアイウエオアｓダフェアｄファｄｓファ");
        comment.Add("アイウエオアイウエオアオイエオアイウエオアイウエオ12313454321354648678796879641315351");

        foreach (var String in comment)
        {
            string text = CheckTextCharaNum(String, 20);
            text += "\n";
            int line = CheckTextLinesNum(text);
            SetScreenHeight(line, SubScreen);
            Comm_Text.text += text + "\n";
        }*/


        ResultManager result = GameObject.FindObjectOfType<ResultManager>();
        if (result)
        {
            _pick_up = result.SelfGemCount;
            _damage = result.SelfDamageCount;
            _teleport = result.SelfTeleportCount;



            result.Spots.ToList().ForEach(s =>
            {
                Event1_text.text += s.SpotName + "\n";
            });

            if (result.Score == ResultManager.ScoreType.SCORE_D)
            {
                Eval_Text.text = ("D");
                Eval_Text.color = new Color(81f / 255f, 2f / 255f, 2f / 255f);
            }
            if (result.Score == ResultManager.ScoreType.SCORE_C)
            {
                Eval_Text.text = ("C");
                Eval_Text.color = new Color(1f / 255f, 31f / 255f, 101f / 255f);
            }
            if (result.Score == ResultManager.ScoreType.SCORE_B)
            {
                Eval_Text.text = ("B");
                Eval_Text.color = new Color(96f / 255f, 67f / 255f, 32f / 255f);
            }
            if (result.Score == ResultManager.ScoreType.SCORE_A)
            {
                Eval_Text.text = ("A");
                Eval_Text.color = new Color(78f / 255f, 69f / 255f, 74f / 255f);
            }
            if (result.Score == ResultManager.ScoreType.SCORE_S)
            {
                Eval_Text.text = ("S");
                Eval_Text.color = new Color(162f / 255f, 126f / 255f, 57f / 255f);
            }
            if (result.Score == ResultManager.ScoreType.SCORE_SS)
            {
                Eval_Text.text = ("SS");
                Eval_Text.color = new Color(220f / 255f, 220f / 255f, 220f / 255f);
            }

            result.Comment.ToList().ForEach(c =>
            {
                string text = CheckTextCharaNum(c, 18);
                text += "\n";
                int line = CheckTextLinesNum(text);
                SetScreenHeight(line, SubScreen);
                Comm_Text.text += text + "\n";
            });
        }
    }

    void SetScreenHeight( int linesNum, SpriteRenderer screen )
    {
        float win_size = 0.05f;
        float win_y = -0.05f;
        screen.transform.localScale += new Vector3(0, win_size * linesNum, 0);
        screen.transform.position += new Vector3(0, win_y * linesNum, 0);
    }

    string CheckTextCharaNum(string text, int oneLineMaxNum) {
        string String = "";
        String = SubstringAtCount(text, oneLineMaxNum);
        return String;
    }

    int CheckTextLinesNum(string text) {
        int count = CountChar(text, '\n');//text.ToList().Where(c => c.Equals("\n")) + 1;
        return count;
    }

	// Update is called once per frame
	void Update () {
        if ( Input.GetKeyDown( KeyCode.F6 ) )
        {
            AdjustResult();
        }
        // 宝石の取得数を設定
        Transform num_mesh = transform.GetChild(1).transform.GetChild(1).transform.GetChild(2).transform;
        num_mesh.GetChild(0).GetComponent<TextMesh>().text = _pick_up.ToString();
        num_mesh.GetChild(1).GetComponent<TextMesh>().text = _damage.ToString();
        num_mesh.GetChild(2).GetComponent<TextMesh>().text = _teleport.ToString();

        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            SceneManager.LoadScene(1);
        }
    }

    private void AdjustResult()
    {
        float info_pos_y = 1.0f;     //ウィンドウの高さ
        GameObject camera = GameObject.Find("Camera (eye)");  //プレイヤーのカメラの取得
        Vector3 camera_pos = camera.transform.position;         //プレイヤーのカメラ座標
        Vector3 front = camera.transform.forward * 3f;        //カメラの正面*距離
        Quaternion camera_rot = camera.transform.rotation;      //プレイヤーのカメラの角度

        // position
        Vector3 pos = camera_pos + front + new Vector3(0, info_pos_y, 0); //どの位置に出すか
                                                                          // 回転
        Vector3 rot = camera_rot.eulerAngles;                               //どの角度で出すか
        rot = new Vector3(0, rot.y, 0);
        Quaternion qua = Quaternion.Euler(rot);

        this.transform.position = pos;        //ウィンドウのポジション
        this.transform.rotation = qua;        //ウィンドウの角度
    }

    public string SubstringAtCount( string self, int count)
    {
        string result = "";
        var length = (int)Mathf.Ceil((float)self.Length / count);

        for (int i = 0; i < length; i++)
        {
            int start = count * i;
            if (self.Length <= start)
            {
                break;
            }
            if (self.Length < start + count)
            {
                result += self.Substring(start);
                result += "\n";
            }
            else
            {
                result += self.Substring(start, count);
                result += "\n";
            }
        }

        return result;
    }
    public int CountChar(string s, char c) {
        return s.Length - s.Replace(c.ToString(), "").Length;
    }

}
