using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BriefingLevel : MonoBehaviour
{
    ElapsedTime timeHandler;
    public int briefingOffsetseconds;
    [SerializeField]
    TMP_Text next;

    [SerializeField]
    Transform camChangePos;
    [SerializeField]
    AudioSource boostOnfx;
    [SerializeField]
    AudioSource gateOpenSfx;
    [SerializeField]
    CameraFollow follow;
    [SerializeField]
    GameObject dockingBay;
    private void Start()
    {
        timeHandler = GetComponent<ElapsedTime>();
        SetupLevel();
    }

    void SetupLevel()
    {
        timeHandler.QueueEvent(Helpers.ToSeconds(0, briefingOffsetseconds, 0), () =>
        {
            BriefingCaptions.Instance.Say("Commander", "After a series of failed operations, all of our main forces are currently occupied with homeworld defense.", Dialogues.Instance.DialogueDict["FC_Briefer_01aE"], true);
        });
        briefingOffsetseconds += (int)Dialogues.Instance.DialogueDict["FC_Briefer_01aE"].length + 1;
        timeHandler.QueueEvent(Helpers.ToSeconds(0, briefingOffsetseconds, 0), () =>
        {
            BriefingCaptions.Instance.Say("Commander", "The current chain of command is in shambles and the only battlegroup left in this sector -- is us.", Dialogues.Instance.DialogueDict["FC_Briefer_01bE"], true);
        });
        briefingOffsetseconds += (int)Dialogues.Instance.DialogueDict["FC_Briefer_01bE"].length + 1;
        timeHandler.QueueEvent(Helpers.ToSeconds(0, briefingOffsetseconds, 0), () =>
        {
            BriefingCaptions.Instance.Say("Commander", "Frankly, we are fighting a losing battle regardless of whether or not this operation succeeds.", Dialogues.Instance.DialogueDict["FC_Briefer_02aE"], true);
        });
        briefingOffsetseconds += (int)Dialogues.Instance.DialogueDict["FC_Briefer_02aE"].length + 1;
        timeHandler.QueueEvent(Helpers.ToSeconds(0, briefingOffsetseconds, 0), () =>
        {
            BriefingCaptions.Instance.Say("Commander", "The best thing that could happen for us, is to buy more time.", Dialogues.Instance.DialogueDict["FC_Briefer_02bE"], true);
        });
        briefingOffsetseconds += (int)Dialogues.Instance.DialogueDict["FC_Briefer_02bE"].length + 1; ;
        timeHandler.QueueEvent(Helpers.ToSeconds(0, briefingOffsetseconds, 0), () =>
        {
            BriefingCaptions.Instance.Say("Commander", "...", null, true);
        });
        briefingOffsetseconds += 1;
        timeHandler.QueueEvent(Helpers.ToSeconds(0, briefingOffsetseconds, 0), () =>
        {
            BriefingCaptions.Instance.Say("Commander", "(Ahem)", Dialogues.Instance.DialogueDict["FC_Briefer_AhemE"], true);
        });
        briefingOffsetseconds += (int)Dialogues.Instance.DialogueDict["FC_Briefer_AhemE"].length + 1;
        timeHandler.QueueEvent(Helpers.ToSeconds(0, briefingOffsetseconds, 0), () =>
        {
            BriefingCaptions.Instance.Say("Commander", " Our objective is to go through layers of debris fields to breach an otherwise impenetrable Umbral defense perimeter.", Dialogues.Instance.DialogueDict["FC_Briefer_03E"], true);
        });
        briefingOffsetseconds += (int)Dialogues.Instance.DialogueDict["FC_Briefer_03E"].length + 1;
        timeHandler.QueueEvent(Helpers.ToSeconds(0, briefingOffsetseconds, 0), () =>
        {
            BriefingCaptions.Instance.Say("Commander", " The area is believed to be a key staging point, so once there, we are to locate and destroy any suspicious facilities found.", Dialogues.Instance.DialogueDict["FC_Briefer_03bE"], true);
        });
        briefingOffsetseconds += (int)Dialogues.Instance.DialogueDict["FC_Briefer_03bE"].length + 1;
        timeHandler.QueueEvent(Helpers.ToSeconds(0, briefingOffsetseconds, 0), () =>
        {
            BriefingCaptions.Instance.Say("Commander", "Our main attack capabilities are dependent on our fighters,", Dialogues.Instance.DialogueDict["FC_Briefer_04aE"], true);
        });
        briefingOffsetseconds += (int)Dialogues.Instance.DialogueDict["FC_Briefer_04aE"].length + 1;
        timeHandler.QueueEvent(Helpers.ToSeconds(0, briefingOffsetseconds, 0), () =>
        {
            BriefingCaptions.Instance.Say("Commander", "you still need to keep this and possibly other vessels operational for immediate support.", Dialogues.Instance.DialogueDict["FC_Briefer_04bE"], true);
        });
        briefingOffsetseconds += (int)Dialogues.Instance.DialogueDict["FC_Briefer_04bE"].length + 1;
        timeHandler.QueueEvent(Helpers.ToSeconds(0, briefingOffsetseconds, 0), () =>
        {
            BriefingCaptions.Instance.Say("Commander", "in addition, as our on-board resources are scarce, we will need to salvage whatever we could find back to the ship", Dialogues.Instance.DialogueDict["FC_Briefer_04cE"], true);
        });
        briefingOffsetseconds += (int)Dialogues.Instance.DialogueDict["FC_Briefer_04cE"].length + 1;
        timeHandler.QueueEvent(Helpers.ToSeconds(0, briefingOffsetseconds, 0), () =>
        {
            BriefingCaptions.Instance.Say("Commander", "Captain, as you will have the overview of the battle in this operation,", Dialogues.Instance.DialogueDict["FC_Briefer_05aE"], true);
        });
        briefingOffsetseconds += (int)Dialogues.Instance.DialogueDict["FC_Briefer_05aE"].length + 1;
        timeHandler.QueueEvent(Helpers.ToSeconds(0, briefingOffsetseconds, 0), () =>
        {
            BriefingCaptions.Instance.Say("Commander", "I will leave you responsible for any resource allocation decisions.", Dialogues.Instance.DialogueDict["FC_Briefer_05bE"], true);
        });
        briefingOffsetseconds += (int)Dialogues.Instance.DialogueDict["FC_Briefer_05bE"].length + 1;
        timeHandler.QueueEvent(Helpers.ToSeconds(0, briefingOffsetseconds, 0), () =>
        {
            BriefingCaptions.Instance.Say("Commander", "Remember to distribute upgrades to our weaponry as you see fit.", Dialogues.Instance.DialogueDict["FC_Briefer_05cE"], true);
        });
        briefingOffsetseconds += (int)Dialogues.Instance.DialogueDict["FC_Briefer_05cE"].length + 1;
        timeHandler.QueueEvent(Helpers.ToSeconds(0, briefingOffsetseconds, 0), () =>
        {
            BriefingCaptions.Instance.Say("Commander", "As this is basically a suicide mission, we are desperately praying for an oversight in their part.", Dialogues.Instance.DialogueDict["FC_Briefer_06aE"], true);
        });
        briefingOffsetseconds += (int)Dialogues.Instance.DialogueDict["FC_Briefer_06aE"].length + 1;
        timeHandler.QueueEvent(Helpers.ToSeconds(0, briefingOffsetseconds, 0), () =>
        {
            BriefingCaptions.Instance.Say("Commander", "Regardless, this might be the only shot we have.", Dialogues.Instance.DialogueDict["FC_Briefer_06bE"], true);
            dockingBay.SetActive(true);
            follow.lookAt = false;
            follow.enabled = false;
            Camera.main.transform.SetParent(camChangePos);
            Camera.main.transform.localPosition = Vector3.zero;
            Camera.main.transform.rotation = camChangePos.rotation;
            follow.StopAllCoroutines();
        });
        briefingOffsetseconds += (int)Dialogues.Instance.DialogueDict["FC_Briefer_06bE"].length + 1;
        timeHandler.QueueEvent(Helpers.ToSeconds(0, briefingOffsetseconds, 0), () =>
        {
            BriefingCaptions.Instance.Say("Commander", "Good Luck", Dialogues.Instance.DialogueDict["FC_Briefer_06cE"], true);
            follow.enabled = true;
            follow.StartCoroutine(follow.ChangeCamPosition());
            follow.lookAt = true;
            Camera.main.transform.SetParent(null);
            dockingBay.SetActive(false);
        });
        briefingOffsetseconds += (int)Dialogues.Instance.DialogueDict["FC_Briefer_06cE"].length + 1;
        timeHandler.QueueEvent(Helpers.ToSeconds(0, briefingOffsetseconds, 0), () =>
        {
            next.gameObject.SetActive(true);
        });


    }
}
