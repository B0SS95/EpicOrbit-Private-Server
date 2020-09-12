using EpicOrbit.Emulator.Netty;
using EpicOrbit.Server.Data.Models.Modules;
using System;

namespace EpicOrbit.Emulator.Game.Controllers.Assemblies {
    public class PlayerMovementAssembly : MovementAssembly {

        #region {[ PROPERTIES ]}
        public PlayerController PlayerController { get; set; }
        #endregion

        #region {[ FIELDS ]}
        private int _speedBugCounter;
        private int _sequenceBugCounter;
        #endregion

        #region {[ CONSTRUCTOR ]}
        public PlayerMovementAssembly(PlayerController controller) : base(controller) {
            PlayerController = controller;
        }
        #endregion

        #region {[ FUNCTIONS ]}
        protected override int CalculateMovement(Position source, Position destination) {
            if (ActualPosition().DistanceTo(source) > 100) { // anti speedhack
                                                             // GameContext.Logger.LogCritical($"Actual: [x: {ActualPosition().X}, y: {ActualPosition().Y}], Source: [x: {source.X}, y: {source.Y}], Distance: {ActualPosition().DistanceTo(source)}");
                _sequenceBugCounter++;
                if (_sequenceBugCounter > 20) {
                    _speedBugCounter++;

                    if (_speedBugCounter > 8 || _sequenceBugCounter > 80) {
                        PlayerController.Send(PacketBuilder.Messages.SpeedhackBan());
                        PlayerController.DeclareBan("speedhack", TimeSpan.FromHours(12));
                    } else if (_speedBugCounter % 3 == 0) {
                        PlayerController.Send(PacketBuilder.Messages.SpeedhackWarning());
                    }
                }
            } else {
                if (_sequenceBugCounter > 0) {
                    _sequenceBugCounter--;
                }
            }

            return base.CalculateMovement(source, destination);
        }
        #endregion

    }
}
