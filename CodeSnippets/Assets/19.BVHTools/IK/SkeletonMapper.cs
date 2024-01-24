﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BVHTools
{
    public class HumanoidBone
    {
        public string Name;
        public HumanBodyBones ID;
        public int IndexBone;
        public Transform BoneTransform;
        public Vector3 offsetWithParent = Vector3.zero;

        public List<HumanoidBone> Childs = new List<HumanoidBone>();
        public string Path;
    }

    public class SkeletonMapper : MonoBehaviour
    {
        public Animator animator { get; set; }
       
        /// <summary>
        /// Minimun amount of bones and important ones
        /// </summary>
        private List<HumanBodyBones> fullBodyBones;
        public List<HumanBodyBones> FullBodyBones
        {
            get { return fullBodyBones; }
        }

        private List<HumanBodyBones> upperBodyBones;
        public List<HumanBodyBones> UpperBodyBones
        {
            get { return upperBodyBones; }
        }



        private HumanoidBone[] boneMap;

        public int BoneNumber
        {
            get
            {
                if (boneMap == null) return 0;

                return boneMap.Length;
            }
        }

        private HumanoidBone rootBone;

        public HumanoidBone RootBone
        {
            get { return rootBone; }
        }

        private bool initialized = false;

        public bool Initialized
        {
            get { return initialized; }
        }

        private void Awake()
        {
            initialized = false;
        }


        /// <summary>
        /// Initialize list of full body bones, (only the main bones, finger bones are discarded)
        /// </summary>
        private void InitializeFullBodyBones()
        {
            fullBodyBones = new List<HumanBodyBones>();
            fullBodyBones.Add(HumanBodyBones.Hips);
            fullBodyBones.Add(HumanBodyBones.LeftUpperLeg);
            fullBodyBones.Add(HumanBodyBones.RightUpperLeg);
            fullBodyBones.Add(HumanBodyBones.LeftLowerLeg);
            fullBodyBones.Add(HumanBodyBones.RightLowerLeg);
            fullBodyBones.Add(HumanBodyBones.LeftFoot);
            fullBodyBones.Add(HumanBodyBones.RightFoot);
            fullBodyBones.Add(HumanBodyBones.Spine);
            fullBodyBones.Add(HumanBodyBones.Chest);
            fullBodyBones.Add(HumanBodyBones.Neck);
            fullBodyBones.Add(HumanBodyBones.Head);
            fullBodyBones.Add(HumanBodyBones.LeftShoulder);
            fullBodyBones.Add(HumanBodyBones.RightShoulder);
            fullBodyBones.Add(HumanBodyBones.LeftUpperArm);
            fullBodyBones.Add(HumanBodyBones.RightUpperArm);
            fullBodyBones.Add(HumanBodyBones.LeftLowerArm);
            fullBodyBones.Add(HumanBodyBones.RightLowerArm);
            fullBodyBones.Add(HumanBodyBones.LeftHand);
            fullBodyBones.Add(HumanBodyBones.RightHand);
            fullBodyBones.Add(HumanBodyBones.LeftToes);
            fullBodyBones.Add(HumanBodyBones.RightToes);
            fullBodyBones.Add(HumanBodyBones.UpperChest);
        }

        /// <summary>
        /// Initialize list of upper body bones, legs are discarded
        /// </summary>
        private void InitializeUpperBodyBones()
        {
            // Add upper body bones
            upperBodyBones = new List<HumanBodyBones>();
            upperBodyBones.Add(HumanBodyBones.Hips);
            upperBodyBones.Add(HumanBodyBones.Spine);
            upperBodyBones.Add(HumanBodyBones.Chest);
            upperBodyBones.Add(HumanBodyBones.Neck);
            upperBodyBones.Add(HumanBodyBones.Head);
            upperBodyBones.Add(HumanBodyBones.LeftShoulder);
            upperBodyBones.Add(HumanBodyBones.RightShoulder);
            upperBodyBones.Add(HumanBodyBones.LeftUpperArm);
            upperBodyBones.Add(HumanBodyBones.RightUpperArm);
            upperBodyBones.Add(HumanBodyBones.LeftLowerArm);
            upperBodyBones.Add(HumanBodyBones.RightLowerArm);
            upperBodyBones.Add(HumanBodyBones.LeftHand);
            upperBodyBones.Add(HumanBodyBones.RightHand);
            upperBodyBones.Add(HumanBodyBones.UpperChest);
        }

        public void GenerateBoneMap(Animator inAnimator)
        {
            initialized = false;

            if (inAnimator == null)
            {
                Debug.Log("<color=red>" + "[SkeletorMapper.GenerateSkeleton] Animator can't be null: " + "</color>");
                return;
            }

            animator = inAnimator;

            InitializeFullBodyBones();

            InitializeUpperBodyBones();

            /// Initialize all bones, no discarded bones, this is helpful for the recorder and loader
            int nBones = (int)HumanBodyBones.LastBone;

            boneMap = new HumanoidBone[(int)HumanBodyBones.LastBone];

            for (int i = 0; i < nBones; i++)
            {
                HumanBodyBones boneIndex = (HumanBodyBones)i;
                if (boneIndex == HumanBodyBones.LastBone)
                {
                    continue;
                }

                boneMap[i] = new HumanoidBone();
                boneMap[i].ID = boneIndex;
                boneMap[i].IndexBone = i;
                boneMap[i].Name = boneIndex.ToString();

                if (boneIndex == HumanBodyBones.Hips)
                {
                    rootBone = boneMap[i];
                }

                Transform boneTransform = animator.GetBoneTransform(boneIndex);

                if (boneTransform != null)
                {
                    boneMap[i].BoneTransform = boneTransform;

                    if (boneIndex == HumanBodyBones.Hips)
                    {
                        rootBone.Path = boneTransform.name;
                    }
                }
            }

            // Generate from the root the child list
            if ((rootBone != null) && (rootBone.BoneTransform != null))
            {
                string path = rootBone.Path;
                Vector3 basePosition = animator.transform.position;

                rootBone.offsetWithParent = rootBone.BoneTransform.position - basePosition;
                boneMap[rootBone.IndexBone].offsetWithParent = rootBone.offsetWithParent;

                BuildHierarchy(rootBone, rootBone.BoneTransform, path);
            }
        }

        private void BuildHierarchy(HumanoidBone currentParentBone, Transform currentBone, string path)
        {
            HumanoidBone nextParentBone;
            string currentPath = path;

            if (currentBone != rootBone.BoneTransform)
            {
                int indexInMap = -1;
                if (IsBoneInMap(currentBone, out indexInMap))
                {
                    currentParentBone.Childs.Add(boneMap[indexInMap]);
                    boneMap[indexInMap].Path = path + "/" + currentBone.name;

                    nextParentBone = boneMap[indexInMap];
                    currentPath = boneMap[indexInMap].Path;
                }
                else
                {
                    nextParentBone = currentParentBone;
                }
            }
            else
            {
                nextParentBone = currentParentBone;
            }

            // Get childs
            if (currentBone.childCount > 0)
            {
                for (int i = 0; i < currentBone.childCount; i++)
                {
                    BuildHierarchy(nextParentBone, currentBone.GetChild(i), currentPath);
                }
            }

            initialized = true;
        }


        private bool IsBoneInMap(Transform bone, out int index)
        {
            index = -1;
            if (boneMap == null) return false;

            for (int i = 0; i < boneMap.Length; i++)
            {
                if (boneMap[i].BoneTransform == null)
                {
                    continue;
                }

                if (boneMap[i].BoneTransform == bone)
                {
                    index = i;
                    return true;
                }
            }

            return false;
        }

        public HumanoidBone GetBoneByIndex(HumanBodyBones index)
        {
            if (boneMap == null) return null;

            if (index == HumanBodyBones.LastBone) return null;

            return boneMap[(int)index];
        }

        public HumanoidBone GetBoneByIndex(int index)
        {
            if (boneMap == null) return null;

            if ((index < 0) || (index >= boneMap.Length)) return null;

            return boneMap[index];
        }

        public int GetBoneIndexByName(string name)
        {
            if (boneMap == null) return -1;

            for (int i = 0; i < boneMap.Length; i++)
            {
                if (boneMap[i].Name.Trim().ToLower() == name.Trim().ToLower())
                {
                    return (int)boneMap[i].ID;
                }
            }
            return -1;
        }
    }
}
