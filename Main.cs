using MelonLoader;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Protobot;
using HarmonyLib;

namespace PartsList
{
    public static class BuildInfo
    {
        public const string Name = "PartsList"; // Name of the Mod.  (MUST BE SET)
        public const string Description = "Lets you view a list of parts"; // Description for the Mod.  (Set as null if none)
        public const string Author = "InvertedOwl"; // Author of the Mod.  (MUST BE SET)
        public const string Company = null; // Company that made the Mod.  (Set as null if none)
        public const string Version = "0.0.1"; // Version of the Mod.  (MUST BE SET)
        public const string DownloadLink = null; // Download Link for the Mod.  (Set as null if none)
    }

    public class PartsList : MelonMod
    {

        public static GameObject bg;
        public static GameObject buildCanvas;
        public static GameObject title;
        public static Dictionary<string, int> partCount = new Dictionary<string, int>();

        public override void OnSceneWasInitialized(int buildindex, string sceneName) // Runs when a Scene has Initialized and is passed the Scene's Build Index and Name.
        {
            buildCanvas = GameObject.Find("Build Canvas");

            GameObject bgTest = new GameObject();
            bgTest.transform.SetParent(buildCanvas.transform);
            bg = bgTest;
            RectTransform transform = bgTest.AddComponent<RectTransform>();
            bgTest.name = "Parts List BG";
            Image image = bgTest.AddComponent<Image>();
            image.sprite = GameObject.Find("Part Selection").GetComponent<Image>().sprite;
            image.color = GameObject.Find("Part Selection").GetComponent<Image>().color;
            transform.localPosition = new Vector2(-buildCanvas.GetComponent<RectTransform>().rect.width / 2 + (40), 0);
            transform.transform.localScale = Vector3.one;

            title = GameObject.Instantiate(GameObject.Find("Text"), transform.transform);
            title.GetComponent<Text>().enabled = true;
            title.GetComponent<Text>().text = "Part                      Count";
            title.GetComponent<Text>().fontSize = 17;
            title.transform.localPosition = new Vector2(0, bg.GetComponent<RectTransform>().sizeDelta.y/2 - 10);
            ReevaluateLads();
        }

        public static void ReevaluateLads()
        {
            bg.GetComponent<RectTransform>().localPosition = new Vector2(-buildCanvas.GetComponent<RectTransform>().rect.width / 2 + (250 / 2) + 10, -100);

            partCount = new Dictionary<string, int>();

            foreach (GameObject part in PartsManager.FindLoadedObjects())
            {
                if (partCount.ContainsKey(part.GetComponent<SavedObject>().id))
                {
                    partCount[part.GetComponent<SavedObject>().id] = partCount[part.GetComponent<SavedObject>().id] + 1;
                }
                else
                {
                    partCount[part.GetComponent<SavedObject>().id] = 1;
                }
            }

            for (int i = bg.transform.childCount - 1; i > 0; i--)
            {
                GameObject.Destroy(bg.transform.GetChild(i).gameObject);
            }

            
            bg.GetComponent<RectTransform>().sizeDelta = new Vector2(250, 40 + (partCount.Count * 20));
            title.transform.localPosition = new Vector2(0, bg.GetComponent<RectTransform>().sizeDelta.y / 2 - 10);
            int inity = 40;
            foreach (string key in partCount.Keys)
            {
                GameObject part = GameObject.Instantiate(GameObject.Find("Text"), bg.transform);
                part.GetComponent<Text>().enabled = true;
                part.GetComponent<Text>().text = "  " + key.Split(new string[] { "(Clone)" }, System.StringSplitOptions.None)[0];
                part.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
                part.GetComponent<Text>().fontSize = 14;
                part.transform.localPosition = new Vector2(0, bg.GetComponent<RectTransform>().sizeDelta.y / 2 - inity);

                GameObject part2 = GameObject.Instantiate(GameObject.Find("Text"), bg.transform);
                part2.GetComponent<Text>().enabled = true;
                part2.GetComponent<Text>().text = partCount[key] + "  ";
                part2.GetComponent<Text>().alignment = TextAnchor.MiddleRight;
                part2.GetComponent<Text>().fontSize = 14;
                part2.transform.localPosition = new Vector2(0, bg.GetComponent<RectTransform>().sizeDelta.y / 2 - inity);
                inity += 20;
            }

        }

        private int tick = 0;
        public override void OnUpdate()
        {
            base.OnUpdate();
            tick += 1;
            if (tick % 30 == 0)
            {
                ReevaluateLads();
            }
        }

        [HarmonyPatch(typeof(ObjectDeletor), "DeleteObject")]
        class DestroyPatch
        {
            [HarmonyPrefix]
            internal static bool Prefix()
            {
                ReevaluateLads();
                return true;
            }

        }

        [HarmonyPatch(typeof(PartGenerator), "SetId")]
        class Generate
        {
            [HarmonyPrefix]
            internal static bool Prefix(GameObject obj)
            {
                ReevaluateLads();
                return true;
            }
        }
    }
}