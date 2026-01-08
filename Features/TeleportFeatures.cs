using System.Reflection;
using UnityEngine;
using ICSModMenu.Utils;
using ICSModMenu.Models;
using System.Collections.Generic;

namespace ICSModMenu.Features
{
    public static class TeleportFeatures
    {
        /// <summary>
        /// Teleports the player to the specified position.
        /// </summary>
        public static void Teleport(Vector3 targetPosition)
        {
            var controller = GetPlayerController();
            if (controller == null) return;

            var cc = controller.GetComponent<CharacterController>();
            if (cc != null)
            {
                // Disable controller before moving to avoid collisions
                cc.enabled = false;
                controller.transform.position = targetPosition;
                cc.enabled = true;
            }

            // Reset grounded state to avoid fall-through
            var groundedField = typeof(VHS.FirstPersonController)
                .GetField("m_isGrounded", BindingFlags.NonPublic | BindingFlags.Instance);
            groundedField?.SetValue(controller, true);

            DebugOverlay.Log($"Teleported to {targetPosition}");
        }

        /// <summary>
        /// Returns the player's current position.
        /// </summary>
        public static Vector3 GetCurrentPosition()
        {
            var controller = GetPlayerController();
            if (controller == null) return Vector3.zero;
            return controller.transform.position;
        }

        /// <summary>
        /// Returns the in-game FirstPersonController instance.
        /// </summary>
        public static VHS.FirstPersonController GetPlayerController()
        {
            return Object.FindObjectOfType<VHS.FirstPersonController>();
        }
    }
}
