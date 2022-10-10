using System.Collections.Generic;
using System.Linq;
using Hazel;
using Reactor;

namespace BetterTownOfUs.CustomOption
{
    public static class Rpc
    {
        public static void SendRpc(CustomOption optionn = null)
        {
            List<CustomOption> options;
            if (optionn != null)
                options = new List<CustomOption> {optionn};
            else
                options = CustomOption.AllOptions;

            var writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId,
                (byte) CustomRPC.SyncCustomSettings, SendOption.Reliable);
            foreach (var option in options)
            {
                // Split up option packets when they go over 1000 bytes
                // The maximum size of an internet-routable packet is around 1472 bytes, but we stay way below this number for safety
                if (writer.Position > 1000) {
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId,
                        (byte) CustomRPC.SyncCustomSettings, SendOption.Reliable);
                }
                writer.Write(option.ID);
                if (option.Type == CustomOptionType.Toggle) writer.Write((bool) option.Value);
                else if (option.Type == CustomOptionType.Number) writer.Write((float) option.Value);
                else if (option.Type == CustomOptionType.String) writer.Write((int) option.Value);
            }

            AmongUsClient.Instance.FinishRpcImmediately(writer);
        }

        public static void ReceiveRpc(MessageReader reader)
        {
            while (reader.BytesRemaining > 0)
            {
                var id = reader.ReadInt32();
                var customOption =
                    CustomOption.AllOptions.FirstOrDefault(option =>
                        option.ID == id); // Works but may need to change to gameObject.name check
                var type = customOption?.Type;
                object value = null;
                if (type == CustomOptionType.Toggle) value = reader.ReadBoolean();
                else if (type == CustomOptionType.Number) value = reader.ReadSingle();
                else if (type == CustomOptionType.String) value = reader.ReadInt32();

                customOption?.Set(value);
            }
        }
    }
}