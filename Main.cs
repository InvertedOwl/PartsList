﻿using MelonLoader;
using UnityEngine;
using UnityEngine.UI;

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
        public override void OnApplicationStart() // Runs after Game Initialization.
        {
        }

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
            transform.localPosition = new Vector2(-buildCanvas.GetComponent<RectTransform>().rect.width/2 + (80), 0);

        }

        public override void OnUpdate()
        {
            bg.GetComponent<RectTransform>().localPosition = new Vector2(-buildCanvas.GetComponent<RectTransform>().rect.width / 2 + (80), 0);
        }
    }
}