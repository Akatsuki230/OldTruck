using System.Linq;
using System.Text;
using UnityEngine;

namespace OldTruck.Stuff {
    public class Gifu : MonoBehaviour {
        public GameObject GifuRadioPivot;
        public GameObject GifuLod;
        public GameObject GifuCabin;
        public GameObject GifuMesh;
        public GameObject GifuAudioEngine;

        void Start() {
            GifuRadioPivot = gameObject.transform.Find("RadioPivot").gameObject;
            GifuLod = gameObject.transform.Find("LOD").gameObject;
            GifuCabin = gameObject.transform.Find("Cabin").gameObject;
            GifuMesh = gameObject.transform.Find("MESH").gameObject;

            switch(OldTruck.RadioPosNumber.Value) {
                case 1:
                    GifuRadioPivot.transform.localPosition = new Vector3(-0.178f,1.729f,3.388f);
                    GifuRadioPivot.transform.localRotation = Quaternion.Euler(new Vector3(-85.645f,-4.8f,184.795f));
                    break;
                case 2:
                    GifuRadioPivot.SetActive(false);
                    break;
            }

            if((bool)OldTruck.RemovePedalsToggle.Value) {
                GifuLod.transform.Find("Dashboard/Pedals").gameObject.SetActive(false);
            }

            if ((bool) OldTruck.RemoveGearStickToggle.Value) {
                GifuLod.transform.Find("Dashboard/GearLever").gameObject.SetActive(false);
            }

            if ((bool) OldTruck.ReplaceShitTankWithLogsToggle.Value) {
                GifuMesh.transform.Find("shit_tank 1").gameObject.SetActive(false);
                GifuLod.transform.Find("ShitTank").gameObject.SetActive(false);
                GifuLod.transform.Find("work_lights").gameObject.SetActive(false);
                transform.Find("ShitTank").gameObject.SetActive(false);
                if ((bool) OldTruck.ReplaceShitTankWithLogsToggle.Value) {
                    GameObject logsHolder = Instantiate(OldTruck.TruckLogBed1);
                    logsHolder.transform.parent = GifuMesh.transform;
                    logsHolder.transform.position = Vector3.zero;
                    logsHolder.transform.rotation = Quaternion.identity;
                    logsHolder.transform.localPosition = new Vector3(-0.08f,1.52f,0.8f);
                    logsHolder.transform.localRotation = new Quaternion(-5.960465E-08f,2.107342E-08f,-2.107343E-08f,1f);
                    logsHolder.transform.localScale = new Vector3(0.95f,0.98f,0.74f);
                    GameObject logs = Instantiate(OldTruck.TruckLogBed2);
                    logs.transform.parent = GifuMesh.transform;
                    logs.transform.position = Vector3.zero;
                    logs.transform.rotation = Quaternion.identity;
                    logs.transform.localPosition = new Vector3(-0.08f,1.52f,0.8f);
                    logs.transform.localRotation = new Quaternion(-5.960465E-08f,2.107342E-08f,-2.107343E-08f,1f);
                    logs.transform.localScale = new Vector3(0.95f,0.98f,0.74f);
                }
            }

            if ((bool) OldTruck.OldStartingSoundToggle.Value) {
                GifuAudioEngine = UnityEngine.Resources.FindObjectsOfTypeAll<GameObject>().First(x => x.name == "AudioEngineGifu");
                foreach (PlayMakerFSM playMakerFsm in GifuAudioEngine.GetComponents<PlayMakerFSM>()) {
                    Destroy(playMakerFsm);
                }
            }
        }
    }
}
