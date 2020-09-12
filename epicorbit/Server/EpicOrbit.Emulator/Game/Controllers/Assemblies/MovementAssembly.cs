using EpicOrbit.Emulator.Game.Controllers.Abstracts;
using EpicOrbit.Emulator.Game.Controllers.Assemblies.Abstracts;
using EpicOrbit.Emulator.Netty;
using EpicOrbit.Emulator.Netty.Interfaces;
using EpicOrbit.Server.Data.Models.Modules;
using System;
using System.Diagnostics;

namespace EpicOrbit.Emulator.Game.Controllers.Assemblies {
    public class MovementAssembly : AssemblyBase {

        #region {[ PROPERTIES ]}
        public Position Destination => _destination;
        public bool IsMoving => _isMoving;
        #endregion

        #region {[ FIELDS ]}
        private Position _destination, _source, _direction;
        private Stopwatch _timeElapsed;
        private double _time, _remainingError;

        private bool _isMoving, _isStuck; // isStuck = if speed is 0 / slow mine / ice rocket
        #endregion

        #region {[ CONSTRUCTOR ]}
        public MovementAssembly(EntityControllerBase controller) : base(controller) {
            _destination = Controller.HangarAssembly.Position;
            _timeElapsed = new Stopwatch();
        }
        #endregion


        /// <summary>
        /// This method is used as soon as the speed changes and the current movement needs refreshing
        /// </summary>
        /// <returns></returns>
        public void RefreshMovement() {
            Move(ActualPosition(), _destination);
        }

        public void Move(Position source, Position destination) {
            if (Controller.HangarAssembly.Speed <= 0) {
                _source = ActualPosition();
                _destination = destination;

                if (!_isStuck) {
                    ICommand moveFixedCommand = PacketBuilder.MoveCommand(Controller, _source, 1);
                    Controller.EntitesInRange(x => x.Send(moveFixedCommand));
                }

                _isStuck = true;
                return;
            }

            _isStuck = false;
            int time = CalculateMovement(source, destination);

            ICommand moveCommand = PacketBuilder.MoveCommand(Controller, destination, time);
            Controller.EntitesInRange(x => x.Send(moveCommand));
        }

        protected virtual int CalculateMovement(Position source, Position destination) {
            _source = source;
            _destination = destination;
            _direction = new Position(_destination.X - _source.X, _destination.Y - _source.Y);

            _timeElapsed.Restart();
            _isMoving = true;

            double time = _source.DistanceTo(_destination) / Controller.HangarAssembly.Speed * 1000.0 + _remainingError;
            _time = Math.Round(time);
            _remainingError = time - _time;

            return (int)_time;
        }

        public Position ActualPosition() {
            if (_isStuck) {
                return _source;
            }

            Position actualPosition;
            if (_isMoving) {
                var timeElapsed = _timeElapsed.Elapsed.TotalMilliseconds;
                if (timeElapsed < _time) {
                    actualPosition = new Position(
                        (int)Math.Round(_source.X + (_direction.X * (timeElapsed / _time))),
                        (int)Math.Round(_source.Y + (_direction.Y * (timeElapsed / _time)))
                    );
                } else {
                    _isMoving = false;
                    actualPosition = _destination;
                }

                Controller.HangarAssembly.Position = actualPosition;
            } else {
                actualPosition = Controller.HangarAssembly.Position;
            }
            return actualPosition;
        }

        public ICommand MovementCommand() {
            if (!IsMoving) {
                return null;
            }

            return PacketBuilder.MoveCommand(Controller, Destination, (int)Math.Max(0, _time - _timeElapsed.Elapsed.TotalMilliseconds));
        }

        public override void Refresh() { }

    }
}
