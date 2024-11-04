using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//�����������
public class SetView : BaseView
{
    protected override void OnAwake()
    {
        base.OnAwake();
        Button closeBtn = Find<Button>("bg/closeBtn");
        closeBtn.onClick.AddListener(onCloseBtn);
        Toggle isOpnSound = Find<Toggle>("bg/IsOpnSound");
        isOpnSound.onValueChanged.AddListener(onIsStopBtn);
        isOpnSound.isOn = GameAPP.SoundManager.IsStop;
        Slider soundCount = Find<Slider>("bg/soundCount");
        soundCount.onValueChanged.AddListener(onSliderBgmBtn);
        soundCount.value = GameAPP.SoundManager.BgmVolume;
        Slider effectCount = Find<Slider>("bg/effectCount");
        effectCount.onValueChanged.AddListener(onSliderSoundEffectBtn);
        effectCount.value = GameAPP.SoundManager.EffectVolume;
    }
    //�Ƿ���
    private void onIsStopBtn(bool isStop)
    {
        GameAPP.SoundManager.IsStop = isStop;
    }
    //����bgm����
    private void onSliderBgmBtn(float val)
    {
        GameAPP.SoundManager.BgmVolume = val;
    }
    //
    private void onSliderSoundEffectBtn(float val)
    {
        GameAPP.SoundManager.EffectVolume = val;
    }
    private void onCloseBtn()
    {
        GameAPP.ViewManager.Close(ViewId);
    }
}
