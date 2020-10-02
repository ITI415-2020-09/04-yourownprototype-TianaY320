using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameMode
{
    idle,
    playing,
    levelEnd
}

public class MissionDemolition : MonoBehaviour
{
    static private MissionDemolition S;

    [Header("Set in Inspector")]
    public Text uitLevel;
    public Text uitShots;
    public Text uitButton;
    public Vector3 appleTreePos;
    public GameObject[] appleTrees;
   // public Text scoreGT;

    [Header("Set Dynamically")]
    public int level;
    public int levelMax;
    public int shotsTaken;
    public GameObject appleTree;
    public GameMode mode = GameMode.idle;
    public string showing = "Show Bow";
    public Text scoreGT;


    // Start is called before the first frame update
    void Start()
    {
        S = this;

        level = 0;
        levelMax = appleTrees.Length;
        StartLevel();
    }

    void StartLevel()
    {
        if (appleTree != null)
        {
            Destroy(appleTree);
        }

        GameObject[] gos = GameObject.FindGameObjectsWithTag("Arrow");
        foreach (GameObject aTemp in gos)
        {
            Destroy(aTemp);
        }

        appleTree = Instantiate<GameObject>(appleTrees[level]);
        appleTree.transform.position = appleTreePos;
        shotsTaken = 0;

        SwitchView("Show Both");
        ArrowLine.S.Clear();

        GameObject scoreGO = GameObject.Find("ScoreCounter");
        scoreGT = scoreGO.GetComponent<Text>();
        scoreGT.text = "0";

        UpdateGUI();

        mode = GameMode.playing;

    }

    void UpdateGUI()
    {
        uitLevel.text = "Level: " + (level + 1) + " of " + levelMax;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGUI();

        if((mode==GameMode.playing)&& (scoreGT.text == "1000"|| scoreGT.text == "1100"|| scoreGT.text == "1200"|| scoreGT.text == "1300"|| scoreGT.text == "1400"|| scoreGT.text == "1500"))
        {
            mode = GameMode.levelEnd;
            SwitchView("Show Both");

            Invoke("NextLevel", 2f);
        }
    }

    void NextLevel()
    {
        level++;
        if (level == levelMax)
        {
            level = 0;
        }
        StartLevel();
    }

    public void SwitchView(string eView = "")
    {
        if (eView == "")
        {
            eView = uitButton.text;
        }
        showing = eView;
        switch (showing)
        {
            case "Show Bow":
                FollowCam.POI = null;
                uitButton.text = "Show AppleTree";
                break;

            case "Show AppleTree":
                FollowCam.POI = S.appleTree;
                uitButton.text = "Show Both";
                break;

            case "Show Both":
                FollowCam.POI = GameObject.Find("ViewBoth");
                uitButton.text = "Show Bow";
                break;
        }
    }

}
