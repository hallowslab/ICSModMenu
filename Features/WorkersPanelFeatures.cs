using UnityEngine;

using ICSModMenu.Utils;

namespace ICSModMenu.Features
{
    public class WorkersPanelFeatures
    {
        public static bool HasBodyguard(WorkersPanel workersPanel)
        {
            if (workersPanel.bodyguard.activeSelf == true) return true;
            return false;
        }

        public static bool HasChef(WorkersPanel workersPanel)
        {
            if (workersPanel.chef.activeSelf == true) return true;
            return false;
        }

        public static void AddBodyguard(WorkersPanel workersPanel)
        {
            if (workersPanel == null) return;

            if (HasBodyguard(workersPanel))
            {
                DebugOverlay.Log("Bodyguard has already been bought");
                return;
            }
            // set the player flag
            PlayerPrefs.SetInt("buybodyguard", 50);
            // activate
            workersPanel.bodyguard.SetActive(true);

            // Can't play sound
            // The type 'AudioClip' is defined in an assembly that is not referenced.
            // You must add a reference to assembly 'UnityEngine.AudioModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'.
            // AudioManager.Instance?.PlaySound(AudioManager.Instance?.cashRegister, 0.3f, 0f);

            // disable the button on the website
            workersPanel.buybodyguardButton.interactable = false;
        }

        public static void AddChef(WorkersPanel workersPanel)
        {
            if (workersPanel == null) return;

            if (HasChef(workersPanel))
            {
                DebugOverlay.Log("Chef has already been bought");
                return;
            }
            PlayerPrefs.SetInt("buychef", 50);
            workersPanel.chef.SetActive(true);
            // AudioManager.Instance?.PlaySound(AudioManager.Instance?.cashRegister, 0.3f, 0f);
            workersPanel.buyChefButton.interactable = false;
        }

        public static void RemoveBodyguard(WorkersPanel workersPanel)
        {
            if (workersPanel == null) return;

            if (!HasBodyguard(workersPanel))
            {
                DebugOverlay.Log("Bodyguard does no exist");
                return;
            }
            PlayerPrefs.SetInt("buybodyguard", 0);
            workersPanel.bodyguard.SetActive(false);
            // AudioManager.Instance?.PlaySound(AudioManager.Instance?.cashRegister, 0.3f, 0f);
            workersPanel.buybodyguardButton.interactable = true;
        }

        public static void RemoveChef(WorkersPanel workersPanel)
        {
            if (workersPanel == null) return;

            if (!HasChef(workersPanel))
            {
                DebugOverlay.Log("Chef does not exist");
                return;
            }
            PlayerPrefs.SetInt("buychef", 0);
            workersPanel.chef.SetActive(false);
            // AudioManager.Instance?.PlaySound(AudioManager.Instance?.cashRegister, 0.3f, 0f);
            workersPanel.buyChefButton.interactable = true;
        }
    }    
}