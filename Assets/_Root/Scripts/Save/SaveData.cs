namespace JustMobyTest.Save
{
    using UnityEngine;

    //TMP
    public class SaveData
    {
        public int DamageStatLevel
        {
            get => PlayerPrefs.GetInt("DamageStatLevel", 0);
            set => PlayerPrefs.SetInt("DamageStatLevel", value);
        }
        
        public int HealthStatLevel
        {
            get => PlayerPrefs.GetInt("HealthStatLevel", 0);
            set => PlayerPrefs.SetInt("HealthStatLevel", value);
        }

        public int SpeedStatLevel
        {
            get => PlayerPrefs.GetInt("SpeedStatLevel", 0);
            set => PlayerPrefs.SetInt("SpeedStatLevel", value);
        }

        public int UpgradePoints
        {
            get => PlayerPrefs.GetInt("UpgradePoints", 0);
            set => PlayerPrefs.SetInt("UpgradePoints", value);
        }
    }
}