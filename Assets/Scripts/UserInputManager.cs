using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 用于控制管理器 键盘操作 鼠标操作等
/// </summary>
public class UserInputManager
{
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                //如果点击到UI
                
            }
            else
            {
                Tools.ScreenPointToRay2D(Camera.main, Input.mousePosition, delegate(Collider2D col)
                {
                    if (col != null)
                    {
                        //检测到有碰撞物体
                        GameAPP.MessageCenter.PostEvent(col.gameObject, Defines.OnSelectEvent);
                    }
                    else
                    {
                        //执行为选中
                        GameAPP.MessageCenter.PostEvent(Defines.OnSelectEvent);
                    }
                });
            }
        }
    }
}
