using System;
using UnityEngine;

namespace AssemblyCSharp
{
    public class PlayerPrefHelper
    {
        public PlayerPrefHelper()
        {

        }

        public static void SaveSoundSetting(bool value)
        {
            int iValue = value ? 1 : 0;
            PlayerPrefs.SetInt(ConfigConstant.PLAYER_SOUND_SETTING, iValue);
        }

        public static bool GetSoundSetting()
        {
            return PlayerPrefs.GetInt(ConfigConstant.PLAYER_SOUND_SETTING, 1) == 1;
        }

        public static int GetCurrentStage()
        {
            return PlayerPrefs.GetInt(ConfigConstant.PLAYER_CURRENT_STATGE, 1);
        }

        public static void SaveCurrentStage(int currentStage)
        {

			PlayerPrefs.SetInt(ConfigConstant.PLAYER_CURRENT_STATGE, currentStage);
        }

        public static int GetPassingStage()
        {
            return PlayerPrefs.GetInt(ConfigConstant.PLAYER_PASSING_STAGE, 1);
        }

        public static void SavePassingStage(int passingStage)
        {
            PlayerPrefs.SetInt(ConfigConstant.PLAYER_PASSING_STAGE, passingStage);
        }
    }
}

