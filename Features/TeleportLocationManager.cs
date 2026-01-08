using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

using ICSModMenu.Models;

namespace ICSModMenu.Features
{
    public static class TeleportLocationManager
    {
        private static readonly string filePath = Path.Combine(BepInEx.Paths.ConfigPath, "ICSMM_teleport_locations.json");
        private static readonly List<TeleportLocation> locations = new List<TeleportLocation>();

        static TeleportLocationManager()
        {
            LoadLocations();
        }

        public static IReadOnlyList<TeleportLocation> Locations => locations;

        public static void AddLocation(string name, Vector3 position)
        {
            var existing = locations.Find(l => l.Name == name);
            if (existing != null)
            {
                existing.Position = new SerializableVector3(position);
            }
            else
            {
                locations.Add(new TeleportLocation(name, position));
            }

            SaveLocations();
        }

        public static void RemoveLocation(int index)
        {
            if (index >= 0 && index < locations.Count)
            {
                locations.RemoveAt(index);
                SaveLocations();
            }
        }

        public static void ClearLocations()
        {
            locations.Clear();
            SaveLocations();
        }

        private static void SaveLocations()
        {
            try
            {
                string json = JsonConvert.SerializeObject(locations, Formatting.Indented);
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                Debug.LogError($"[TeleportLocationManager] Failed to save locations: {ex}");
            }
        }

        private static void LoadLocations()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    var loaded = JsonConvert.DeserializeObject<List<TeleportLocation>>(json);
                    if (loaded != null)
                        locations.AddRange(loaded);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"[TeleportLocationManager] Failed to load locations: {ex}");
            }
        }
    }
}
