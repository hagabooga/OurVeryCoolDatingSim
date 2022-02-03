using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CameronsWorld.Utility;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.IO;
using System.Linq;
using System.Reflection;

namespace CameronsWorld
{
    [CreateAssetMenu(fileName = "Character", menuName = "Cameron's World/Character", order = 1)]
    public class CharacterInfo : ScriptableObject
    {
        [Serializable]
        public class Ui
        {
            [SerializeField] Sprite arrow, dialogueBoxLeft, dialogueBoxRight;

            public Sprite Arrow { get => arrow; }
            public Sprite DialogueBoxLeft { get => dialogueBoxLeft; }
            public Sprite DialogueBoxRight { get => dialogueBoxRight; }

            public Ui(Sprite arrow, Sprite dialogueBoxLeft, Sprite dialogueBoxRight)
            {
                this.arrow = arrow;
                this.dialogueBoxLeft = dialogueBoxLeft;
                this.dialogueBoxRight = dialogueBoxRight;
            }
        }

        [Serializable]
        class CharacterPoses : SerializableDictionary<int, SerializableDictionary<string, Sprite>> { }

        private const string DataFolder = "Assets/CamsWorld/Character/Data/";
        [SerializeField] CharacterPoses poses;
        [SerializeField] Ui ui;


        public Sprite GetPose(int poseNumber, string expression) => poses[poseNumber][expression];





        // #if UNITY_EDITOR
        void OnValidate()
        {
            LoadPoses();

            LoadUi();
        }

        private void LoadUi()
        {
            GetFiles(out string uiPath, out IEnumerable<FileInfo> files, "UI", ".png");
            foreach (var file in files)
            {
                var nameWithoutExtension = Path.GetFileNameWithoutExtension(file.Name);
                var field = ui.GetType().GetField(nameWithoutExtension, BindingFlags.NonPublic | BindingFlags.Instance);
                var sprite = AssetDatabase.LoadAssetAtPath<Sprite>(Path.Combine(uiPath, file.Name));
                field.SetValue(ui, sprite);
            }
        }

        private void LoadPoses()
        {
            poses = new CharacterPoses();
            GetFiles(out string posesPath, out IEnumerable<FileInfo> files, "Poses", ".png");
            foreach (var file in files)
            {
                var nameWithoutExtension = Path.GetFileNameWithoutExtension(file.Name);
                string[] nameSplit = nameWithoutExtension.Split("_");
                var sprite = AssetDatabase.LoadAssetAtPath<Sprite>(Path.Combine(posesPath, file.Name));
                int poseNumber = int.Parse(nameSplit[0]);
                if (!poses.ContainsKey(poseNumber))
                {
                    poses.Add(poseNumber, new SerializableDictionary<string, Sprite>());
                }
                poses[poseNumber][nameSplit[1]] = sprite;
            }
        }

        private void GetFiles(out string posesPath,
                              out IEnumerable<FileInfo> files,
                              string folderToLookIn,
                              string endsWith)
        {
            string path = Path.Combine(DataFolder, name);
            posesPath = Path.Combine(path, folderToLookIn);
            files = new DirectoryInfo(posesPath).GetFiles().Where(x => x.Name.EndsWith(endsWith));
        }
        // #endif
    }
}