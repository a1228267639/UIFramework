using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AnimatorExtension
{

    private static WaitForFixedUpdate Update = new WaitForFixedUpdate();

    public static IEnumerator WaitAnimPlay(this Animator selfanim, string animName, Action action)
    {
        bool Loop = true;
        while (Loop)
        {
            yield return Update;
            AnimatorStateInfo animatorInfo;
            animatorInfo = selfanim.GetCurrentAnimatorStateInfo(0);
            //normalizedTime的值为0~1，0为开始，1为结束。
            if ((animatorInfo.normalizedTime >= 1.0f) && animatorInfo.IsName(animName))
            {
                if (action != null)
                {
                    action();
                    Loop = false;
                }
            }
        }
    }

}
