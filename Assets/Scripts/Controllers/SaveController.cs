using UnityEngine;

namespace Controllers
{
    public class SaveController
    {
        public void Save(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
        }
        
        public void Save(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
        }
        
        public void Save(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
        }

        public int LoadInt(string key)
        {
            return PlayerPrefs.GetInt(key);
        }
        
        public float LoadFloat(string key)
        {
            return PlayerPrefs.GetFloat(key);
        }
        
        public string LoadString(string key)
        {
            return PlayerPrefs.GetString(key);
        }
    }
}