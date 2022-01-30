using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameronsWorld.Utility
{
    public class SoundManager : Singleton<SoundManager>
    {
        IList<AudioSource> sources = new List<AudioSource>();

        protected override void Start()
        {
            base.Start();
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