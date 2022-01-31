using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace CameronsWorld.Utility
{
    public class SoundManager : Singleton<SoundManager>
    {
        IList<AudioSource> sources = new List<AudioSource>();

        public void PlayBGM(object clip)
        {
            throw new NotImplementedException();
        }

        AudioSource bgm;

        protected override void Start()
        {
            base.Start();
            bgm = gameObject.AddComponent<AudioSource>();
            bgm.loop = true;
        }

        public void StopAll()
        {
            foreach (var item in sources)
            {
                item.Stop();
                Destroy(item);
            }
            sources.Clear();
        }



        public void PlayBGM(AudioClip newClip)
        {

            if (bgm.clip != null)
            {
                StartCoroutine(FadeOutThenIn(newClip));
            }
            else
            {
                if (newClip == bgm.clip) return;
                StartCoroutine(FadeIn(newClip));
                print("GG");
            }
        }
        public IEnumerator FadeIn(AudioClip newClip)
        {
            bgm.volume = 0;
            bgm.clip = newClip;
            bgm.Play();
            yield return bgm.DOFade(1, 0.5f).WaitForCompletion();
        }
        public IEnumerator FadeOut()
        {
            yield return bgm.DOFade(0, 0.5f).WaitForCompletion();
        }

        public IEnumerator FadeOutThenIn(AudioClip newClip)
        {
            yield return bgm.DOFade(0, 0.5f).WaitForCompletion();
            bgm.clip = newClip;
            bgm.Play();
            yield return bgm.DOFade(1, 0.5f).WaitForCompletion();
        }

        public AudioSource Play(AudioClip clip, bool loop = true)
        {
            var source = gameObject.AddComponent<AudioSource>();
            source.clip = clip;
            source.Play();
            sources.Add(source);
            source.loop = loop;
            return source;
        }


    }
}