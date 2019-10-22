using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VIDE_Data;

public class UIManager : MonoBehaviour
{
    public GameObject container_NPC;
    public GameObject container_PLAYER;
    public Text text_NPC;
    public Text text_last_NPC;
    public Text[] text_Choices;
    public GameObject intro;
    public Text expl_text;
    bool firstmessage = true;
    public GameObject NPC_line;
    private GameObject NPC_line_duplicate;

    // Start is called before the first frame update
    void Start()
    {
        container_NPC.SetActive(false);
        container_PLAYER.SetActive(false);
        intro.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!VD.isActive)
                Begin();
            else
                VD.Next();
        }
    }

    void Begin()
    {
        VD.OnNodeChange += UpdateUI;
        VD.OnEnd += End;
        VD.BeginDialogue(GetComponent<VIDE_Assign>());
    }

    void UpdateUI(VD.NodeData data)
    {
        intro.SetActive(false);
        container_NPC.SetActive(false);
        container_PLAYER.SetActive(false);

        if (data.isPlayer)
        {
            container_PLAYER.SetActive(true);

            //if first message - don't show the girl message, if not the first message - show girl message
            if (!firstmessage)
                container_PLAYER.transform.GetChild(0).gameObject.SetActive(true);

            int index = 0;
            foreach (Text choice in text_Choices)
            {
                if (index < data.comments.Length)
                {
                    choice.transform.parent.gameObject.SetActive(true);
                    choice.text = data.comments[index];
                }
                else
                    choice.transform.parent.gameObject.SetActive(false);
                index++;
            }
            // It is now no longer the first message
            firstmessage = false;
        }
        else
        {
            container_NPC.SetActive(true);
            text_NPC.text = data.comments[data.commentIndex];
            text_last_NPC.text = data.comments[data.commentIndex];
            if (data.extraVars["Explanation"] != null)
                expl_text.text = (string)data.extraVars["Explanation"];
        }
    }

    void End(VD.NodeData data)
    {
        VD.EndDialogue();
        GameManager.WonLevel();
        // container_NPC.SetActive(false);
        // container_PLAYER.SetActive(false);
        // VD.OnNodeChange -= UpdateUI;
        // VD.OnEnd -= End;
    }

    private void OnDisable()
    {
        if (container_NPC != null)
            End(null);


    }

    public void SetPlayerChoice(int choice)
    {
        VD.nodeData.commentIndex = choice;
        if (Input.GetMouseButtonUp(0))
            VD.Next();
    }
}
