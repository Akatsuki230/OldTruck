using System;
using MSCLoader;
using OldTruck.Stuff;
using UnityEngine;

namespace OldTruck {
    public class OldTruck : Mod {
        public override string ID => "OldTruck";
        public override string Name => "OldTruck";
        public override string Author => "MLDKYT";
        public override string Version => "b0.1";

        public override bool UseAssetsFolder => false;


        #region Variables

        public GameObject Gifu;

        public static AudioClip EngineStart1;
        public static AudioClip EngineStart2;
        public static GameObject TruckLogBed1;
        public static GameObject TruckLogBed2;

        #endregion
        #region Settings

        public static Settings RadioPosNumber;
        public static Settings RemovePedalsToggle;
        public static Settings RemoveGearStickToggle;
        public static Settings RemoveDoorsToggle;
        public static Settings ReplaceShitTankWithLogsToggle;
        public static Settings OldStartingSoundToggle;

        #endregion

        public override void OnLoad() {
            byte[] resBytes = Resources.oldtruck;
            AssetBundle bundle = AssetBundle.CreateFromMemoryImmediate(resBytes);
            if ((bool) OldStartingSoundToggle.Value) {
                EngineStart1 = bundle.LoadAsset<AudioClip>("valmet_start");
                EngineStart2 = bundle.LoadAsset<AudioClip>("valmet_starting");
            }
            if ((bool) ReplaceShitTankWithLogsToggle.Value) {
                TruckLogBed1 = bundle.LoadAsset<GameObject>("truck_logbed");
                TruckLogBed2 = bundle.LoadAsset<GameObject>("truck_logs");
            }
            bundle.Unload(false);

            Gifu = GameObject.Find("GIFU(750/450psi)");
            Gifu.AddComponent<Gifu>();
        }

        public override void ModSettings() {
            RadioPosNumber = new Settings("radioPos", "Radio position", 0);
            RemovePedalsToggle = new Settings("removePedals", "Remove pedals", false);
            RemoveGearStickToggle = new Settings("removeGearStick", "Remove gear stick", false);
            RemoveDoorsToggle = new Settings("removeDoors", "Remove doors", false);
            ReplaceShitTankWithLogsToggle = new Settings("logs", "Replace shit tank with logs", false);
            OldStartingSoundToggle = new Settings("oldStarting", "Old starting sound", false);

            Settings.AddHeader(this,"Inside");
            Settings.AddSlider(this, RadioPosNumber, 0, 2, new[] {
                "Normal",
                "Put it next to wipers knob",
                "Remove radio"
            });
            Settings.AddCheckBox(this, RemovePedalsToggle);
            Settings.AddCheckBox(this, RemoveGearStickToggle);
            Settings.AddHeader(this, "Cabin");
            Settings.AddCheckBox(this,RemoveDoorsToggle);
            Settings.AddHeader(this, "Outside");
            Settings.AddCheckBox(this, ReplaceShitTankWithLogsToggle);
            Settings.AddHeader(this, "Sounds");
            Settings.AddCheckBox(this, OldStartingSoundToggle);
        }
    }
}
