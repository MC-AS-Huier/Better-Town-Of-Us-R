using System;
using System.Collections.Generic;
using HarmonyLib;

namespace BetterTownOfUs
{
    [HarmonyPatch(typeof(ChatController), nameof(ChatController.SendChat))]
    class SendChatPatch
    {
        public static bool Prefix(ChatController __instance)
        {
            string text = __instance.TextArea.text;
            bool handled = false;
            if (text.ToLower().StartsWith("/set"))
            {
                var args = text.ToLower().Split(" ");
                __instance.AddChat(PlayerControl.LocalPlayer, clearSettingsTxt(GameSettings.SettingsTxt.Remove(0, 27).Split("\n"), args.Length > 1 ? args[1] : ""));
                handled = true;
            }

            if (handled)
            {
                __instance.TextArea.Clear();
                __instance.quickChatMenu.ResetGlyphs();
            }
            return !handled;
        }

        private static string clearSettingsTxt(Array text, string args)
        {
            List<string> page = new List<string>();
            var prev = true;
            foreach (string line in text)
            {
                if (line.Contains("(Scroll")) continue;
                if (args == "vanilla" && line.Contains("Role Count Settings")) break;
                if (args == "rate")
                {
                    if (line.Contains("Role Count Settings")) prev = false;
                    else if (prev) continue;
                    else if (line.Contains("Custom Game Settings")) break;
                }
                if (args == "custom")
                {
                    if (line.Contains("Detective")) BetterTownOfUs.Logger.LogMessage($"(--{line}--)");
                    if (line.Contains("Custom Game Settings")) prev = false;
                    else if (prev) continue;
                    else if (line.StartsWith("<color=#4D4DFFFF>Detective")) break;
                }
                if (args == "role")
                {
                    if (line.StartsWith("<color=#4D4DFFFF>Detective")) prev = false;
                    else if (prev) continue;
                }
                string clearedLine = line;
                if (clearedLine.Contains("<color")) clearedLine = clearedLine.Remove(clearedLine.IndexOf("<"), 17);
                if (clearedLine.Contains("</color")) clearedLine = clearedLine.Remove(clearedLine.IndexOf("<"), 8);
                page.Add(clearedLine);
            }
            page.Add("\nTo See Specific Page Use\n/Settings PageName (/set vanilla):\nVanilla: Vanilla and Better Polus Settings\nRate: Role Count and Role Rate Settings\nCustom: Better Town of Us and Assassin Settings\nRole: Role Settings");
            return String.Join("\n", page.ToArray());
        }
    }
}
