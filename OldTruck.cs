using MSCLoader;
using OldTruck.Stuff;
using System;
using UnityEngine;

namespace OldTruck {
    public class OldTruck : Mod {
        public override string ID => "OldTruck";
        public override string Name => "OldTruck";
        public override string Author => "MLDKYT";
        public override string Version => "1.0";

        public override bool UseAssetsFolder => false;


        #region Variables

        public GameObject Gifu;

        public static AudioClip EngineStart1;
        public static AudioClip EngineStart2;
        public static AudioClip EngineIdle;
        public static AudioClip EngineAccel;
        public static AudioClip NewEngineIdle;
        public static AudioClip NewEngineAccel;
        public static GameObject TruckLogBed1;
        public static GameObject TruckLogBed2;
        public static Material Paint4;
        public static Material TruckCabin;

        #endregion
        #region Settings

        public static Settings RadioPosNumber;
        public static Settings EngineNumber;
        public static Settings RemovePedalsToggle;
        public static Settings RemoveDoorsToggle;
        public static Settings ReplaceShitTankWithLogsToggle;
        public static Settings OldStartingSoundToggle;
        public static Settings PaintInteriorBlackToggle;
        public static Settings PaintExteriorRedToggle;
        public static Settings HideSPZToggle;
        public static Settings HideMudFlapsToggle;

        #endregion

        public override void OnLoad() {
            byte[] resBytes = Resources.oldtruck;
            AssetBundle bundle = AssetBundle.CreateFromMemoryImmediate(resBytes);

            if((bool)OldStartingSoundToggle.Value) {
                EngineStart1 = bundle.LoadAsset<AudioClip>("valmetstart");
                EngineStart2 = bundle.LoadAsset<AudioClip>("valmetstarting");
            }

            if((bool)ReplaceShitTankWithLogsToggle.Value) {
                TruckLogBed1 = bundle.LoadAsset<GameObject>("trucklogbed.prefab");
                TruckLogBed2 = bundle.LoadAsset<GameObject>("trucklogs.prefab");
            }

            if(EngineNumber.Value.ToString() != "0") {
                switch(EngineNumber.Value.ToString()) {
                    case "1":
                        NewEngineIdle = bundle.LoadAsset<AudioClip>("newcumminsidle");
                        NewEngineAccel = bundle.LoadAsset<AudioClip>("newcumminsaccel");
                        break;
                    case "2":
                        EngineIdle = bundle.LoadAsset<AudioClip>("cumminsidle");
                        EngineAccel = bundle.LoadAsset<AudioClip>("cumminsaccel");
                        break;
                }
            }

            if((bool)PaintInteriorBlackToggle.Value) {
                Paint4 = bundle.LoadAsset<Material>("paint4");
            }

            if((bool)PaintExteriorRedToggle.Value) {
                TruckCabin = bundle.LoadAsset<Material>("truckcabin");
            }
            bundle.Unload(false);

            Gifu = GameObject.Find("GIFU(750/450psi)");
            Gifu.AddComponent<Gifu>();

            // Start sounds
            if((bool)OldStartingSoundToggle.Value) {
                GameObject truckSoundsGameObject = GameObject.Find("MasterAudio/Truck");
                truckSoundsGameObject.transform.Find("start1").GetComponent<AudioSource>().clip = EngineStart1;
                truckSoundsGameObject.transform.Find("start2").GetComponent<AudioSource>().clip = EngineStart2;
                truckSoundsGameObject.transform.Find("start3").GetComponent<AudioSource>().clip = EngineStart2;
                truckSoundsGameObject.transform.Find("start4").GetComponent<AudioSource>().clip = EngineStart2;
            }
        }

        public override void ModSettings() {
            RadioPosNumber = new Settings("radioPos","Radio position",0);
            EngineNumber = new Settings("engineSel","Engine type",0);
            RemovePedalsToggle = new Settings("removePedals","Remove pedals",false);
            RemoveDoorsToggle = new Settings("removeDoors","Remove doors",false);
            ReplaceShitTankWithLogsToggle = new Settings("logs","Replace shit tank with logs",false);
            OldStartingSoundToggle = new Settings("oldStarting","Old starting sound",false);
            PaintInteriorBlackToggle = new Settings("blackInt","Black interior",false);
            PaintExteriorRedToggle = new Settings("redExt","Red exterior",false);
            HideSPZToggle = new Settings("spzHide","Hide license plate",false);
            HideMudFlapsToggle = new Settings("hideFlaps","Hide mud flaps",false);

            Settings.AddHeader(this,"Inside");
            Settings.AddSlider(this,RadioPosNumber,0,2,new[] {
                "Normal",
                "Put it next to wipers knob",
                "Remove radio"
            });
            Settings.AddCheckBox(this,RemovePedalsToggle);
            Settings.AddHeader(this,"Cabin");
            Settings.AddCheckBox(this,RemoveDoorsToggle);
            Settings.AddCheckBox(this,PaintInteriorBlackToggle);
            Settings.AddHeader(this,"Outside");
            Settings.AddCheckBox(this,ReplaceShitTankWithLogsToggle);
            Settings.AddCheckBox(this,PaintExteriorRedToggle);
            Settings.AddCheckBox(this,HideSPZToggle);
            Settings.AddCheckBox(this,HideMudFlapsToggle);
            Settings.AddHeader(this,"Sounds");
            Settings.AddSlider(this,EngineNumber,0,2,new[] {
                "Keep these sounds",
                "Use pre-2018 sounds",
                "Use build 172 sounds"
            });
            Settings.AddCheckBox(this,OldStartingSoundToggle);
        }

        public static void PrintE(string s,Exception e) {
            ModConsole.Warning(string.Format("[OldTruck:Error] {0}: {1}",s,e.Message));
        }
    }


}
