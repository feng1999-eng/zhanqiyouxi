using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPoint : MonoBehaviour
{
    public int LevelId; //…Ë÷√πÿø®Id

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameAPP.MessageCenter.PostEvent(Defines.ShowLevelDesEvent, LevelId);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameAPP.MessageCenter.PostEvent(Defines.HideLevelDesEvent);
    }
}
