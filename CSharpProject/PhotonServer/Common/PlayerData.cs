using System;

namespace Common
{
    [Serializable]
    public class PlayerData
    {
        public string Username { get; set; }
        public Vector3Data Pos { get; set; }
    }
}
