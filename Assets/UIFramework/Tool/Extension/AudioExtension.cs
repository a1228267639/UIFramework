/*文件说明
 * CreateTime:  2019/05/23/17:23:06
 * Projectname:  FST_Project
 * FileName:  AudioExtension.cs
 * Developers:  五峰杰
 * Creator:  杨智杰
 * Description:
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Tools
{
    public static class AudioExtension
    {
        /// <summary>
        /// 音乐播放完毕回调事件
        /// </summary>
        /// <param name="audio">播放的音乐</param>
        /// <param name="action">回调事件</param>
        /// <returns></returns>
        public static IEnumerator WaitAudioPlay(this AudioSource selfAudio, Action action = null)
        {
            if (selfAudio.clip != null)
            {
                yield return new WaitForSeconds(selfAudio.clip.length);
                if (action != null)
                {
                    action();
                }
            }
        }
        /// <summary>
        /// 播放自身音乐 关闭其他音乐
        /// </summary>
        /// <param name="selfAudio"></param>
        /// <param name="audioSources"></param>
        public static void PlaySelfStopOtherAudio(this AudioSource selfAudio, params AudioSource[] audioSources)
        {
            for (int i = 0; i < audioSources.Length; i++)
            {
                audioSources[i].Stop();
            }
            selfAudio.Play();
        }
        /// <summary>
        /// 播放音乐 添加 Clip
        /// </summary>
        /// <param name="selfAudio"></param>
        /// <param name="audioClip"></param>
        public static void Play(this AudioSource selfAudio, AudioClip audioClip)
        {
            selfAudio.clip = audioClip;
            selfAudio.Play();
        }
    }
}