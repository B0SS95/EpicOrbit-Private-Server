using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Commands;
using EpicOrbit.Emulator.Netty.Interfaces;

namespace EpicOrbit.Emulator.Netty.Handlers {

    [AutoDiscover("10.0.6435")]
    public class SetSlotbarItemRequestHandler : ICommandHandler<SetSlotbarItemRequest> {

        public void Execute(IClient initiator, SetSlotbarItemRequest command) {

            if (command.fromIndex != 0) {
                switch (command.fromSlotbarId) {
                    case "standardSlotBar":
                        initiator.Controller.ClientConfiguration.Remove(
                            initiator.Controller.ClientConfiguration.StandardSlotBar, command.fromIndex);
                        break;

                    case "premiumSlotBar":
                        initiator.Controller.ClientConfiguration.Remove(
                            initiator.Controller.ClientConfiguration.PremiumSlotBar, command.fromIndex);
                        break;

                    case "proActionBar":
                        initiator.Controller.ClientConfiguration.Remove(
                            initiator.Controller.ClientConfiguration.ProActionBar, command.fromIndex);
                        break;
                }
            }


            if (command.toIndex != 0) {

                switch (command.toSlotbarId) {
                    case "standardSlotBar":

                        if (command.fromIndex != 0 && initiator.Controller.ClientConfiguration.Get(initiator.Controller.ClientConfiguration.StandardSlotBar, command.toIndex, out string itemId1)) {
                            if (command.fromSlotbarId == "standardSlotBar") {
                                initiator.Controller.ClientConfiguration.Set(
                                    initiator.Controller.ClientConfiguration.StandardSlotBar, command.fromIndex, itemId1);
                            } else if (command.fromSlotbarId == "premiumSlotBar") {
                                initiator.Controller.ClientConfiguration.Set(
                                    initiator.Controller.ClientConfiguration.PremiumSlotBar, command.fromIndex, itemId1);
                            } else if (command.fromSlotbarId == "proActionBar") {
                                initiator.Controller.ClientConfiguration.Set(
                                    initiator.Controller.ClientConfiguration.ProActionBar, command.fromIndex, itemId1);
                            }
                        }

                        initiator.Controller.ClientConfiguration.Set(
                        initiator.Controller.ClientConfiguration.StandardSlotBar, command.toIndex, command.itemId);

                        break;

                    case "premiumSlotBar":

                        if (command.fromIndex != 0 && initiator.Controller.ClientConfiguration.Get(initiator.Controller.ClientConfiguration.PremiumSlotBar, command.toIndex, out string itemId2)) {
                            if (command.fromSlotbarId == "standardSlotBar") {
                                initiator.Controller.ClientConfiguration.Set(
                                    initiator.Controller.ClientConfiguration.StandardSlotBar, command.fromIndex, itemId2);
                            } else if (command.fromSlotbarId == "premiumSlotBar") {
                                initiator.Controller.ClientConfiguration.Set(
                                    initiator.Controller.ClientConfiguration.PremiumSlotBar, command.fromIndex, itemId2);
                            } else if (command.fromSlotbarId == "proActionBar") {
                                initiator.Controller.ClientConfiguration.Set(
                                    initiator.Controller.ClientConfiguration.ProActionBar, command.fromIndex, itemId2);
                            }
                        }

                        initiator.Controller.ClientConfiguration.Set(
                            initiator.Controller.ClientConfiguration.PremiumSlotBar, command.toIndex, command.itemId);
                        break;

                    case "proActionBar":

                        if (command.fromIndex != 0 && initiator.Controller.ClientConfiguration.Get(initiator.Controller.ClientConfiguration.ProActionBar, command.toIndex, out string itemId3)) {
                            if (command.fromSlotbarId == "standardSlotBar") {
                                initiator.Controller.ClientConfiguration.Set(
                                    initiator.Controller.ClientConfiguration.StandardSlotBar, command.fromIndex, itemId3);
                            } else if (command.fromSlotbarId == "premiumSlotBar") {
                                initiator.Controller.ClientConfiguration.Set(
                                    initiator.Controller.ClientConfiguration.PremiumSlotBar, command.fromIndex, itemId3);
                            } else if (command.fromSlotbarId == "proActionBar") {
                                initiator.Controller.ClientConfiguration.Set(
                                    initiator.Controller.ClientConfiguration.ProActionBar, command.fromIndex, itemId3);
                            }
                        }

                        initiator.Controller.ClientConfiguration.Set(
                        initiator.Controller.ClientConfiguration.ProActionBar, command.toIndex, command.itemId);

                        break;

                }
            }

            initiator.Send(PacketBuilder.Slotbar.SlotBarsCommand(initiator.Controller, false));

        }

    }
}
