using EpicOrbit.Emulator.Game.Objects.Abstracts;
using EpicOrbit.Emulator.Netty;
using EpicOrbit.Emulator.Netty.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;

namespace EpicOrbit.Emulator.Game.Controllers {
    public class GroupController : AddressableObjectBase {

        public struct Invitation {

            public DateTime CreationDate { get; set; }
            public PlayerController Initiator { get; set; }
            public PlayerController Target { get; set; }

            public Invitation(PlayerController initiator, PlayerController target) {
                Initiator = initiator;
                Target = target;
                CreationDate = DateTime.Now;
            }

        }

        #region {[ STATIC ]}

        #region {[ FIELDS ]}
        private static Dictionary<int, GroupController> _groups;
        private static Dictionary<long, Invitation> _invitations;
        private static object _invitationsLock;
        private static Timer _timer;
        #endregion

        #region {[ CONSTRUCTOR ]}
        static GroupController() {
            _groups = new Dictionary<int, GroupController>();
            _invitations = new Dictionary<long, Invitation>();
            _invitationsLock = new object();

            _timer = new Timer(x => {
                CheckTimeout();
            }, null, TimeSpan.FromMilliseconds(800), TimeSpan.FromMilliseconds(800));
        }
        #endregion

        #region {[ TIMING ]}
        private static void CheckTimeout() {
            lock (_invitationsLock) {
                Dictionary<long, Invitation> newList = new Dictionary<long, Invitation>();
                foreach (Invitation invitation in _invitations.Values) {
                    if (DateTime.Now - invitation.CreationDate > TimeSpan.FromMinutes(1)) {
                        ICommand invTimeoutCommand = PacketBuilder.Group.InvitationTimeout(invitation);
                        invitation.Initiator.Send(invTimeoutCommand);
                        invitation.Target.Send(invTimeoutCommand);
                    } else {
                        newList.Add(Generate(invitation.Initiator.ID, invitation.Target.ID), invitation);
                    }
                }
                _invitations = newList;
            }
        }
        #endregion

        #region {[ FUNCTIONS ]}
        public static long Generate(int initiatorId, int targetId) {
            return (Convert.ToInt64(initiatorId) << 32) + targetId;
        }

        public static GroupController For(int id) {
            lock (_groups) {
                if (!_groups.TryGetValue(id, out GroupController groupController)) {
                    return null;
                }
                return groupController;
            }
        }

        public static GroupController Create(PlayerController initiator, PlayerController member) {
            GroupController group = new GroupController(initiator, member);
            lock (_groups) {
                _groups.Add(group.ID, group);
            }
            group.Initialize();
            return group;
        }

        public static void EnqueueInvitation(PlayerController initiator, PlayerController target) {
            lock (_invitationsLock) {
                if (!_invitations.ContainsKey(Generate(initiator.ID, target.ID))
                    && !_invitations.ContainsKey(Generate(target.ID, initiator.ID))) {
                    Invitation inv = new Invitation(initiator, target);
                    _invitations.Add(Generate(initiator.ID, target.ID), inv);
                    ICommand invCommand = PacketBuilder.Group.InvitiationCommand(inv);
                    initiator.Send(invCommand);
                    target.Send(invCommand);
                }
            }
        }

        public static IEnumerable<Invitation> ForUser(PlayerController initiator) {
            lock (_invitationsLock) {
                foreach (Invitation invitation in _invitations.Values) {
                    if (invitation.Initiator.ID == initiator.ID || invitation.Target.ID == initiator.ID) {
                        yield return invitation;
                    }
                }
            }
        }

        public static bool GetForPeer(int initiator, int target, out Invitation inv) {
            lock (_invitationsLock) {
                if (_invitations.TryGetValue(Generate(initiator, target), out inv)) {
                    return true;
                }
                return false;
            }
        }

        public static bool RemoveForPeer(int initiator, int target) {
            lock (_invitationsLock) {
                return _invitations.Remove(Generate(initiator, target));
            }
        }

        public static void RemoveAllForUser(PlayerController initiator) {
            lock (_invitationsLock) {
                Dictionary<long, Invitation> newList = new Dictionary<long, Invitation>();
                foreach (Invitation invitation in _invitations.Values) {
                    if (invitation.Initiator.ID == initiator.ID || invitation.Target.ID == initiator.ID) {
                        ICommand command = PacketBuilder.Group.InvitationRemoveNoReason(invitation);
                        invitation.Initiator.Send(command);
                        invitation.Target.Send(command);
                    } else {
                        newList.Add(Generate(invitation.Initiator.ID, invitation.Target.ID), invitation);
                    }
                }
                _invitations = newList;
            }
        }
        #endregion

        #endregion

        #region {[ FIELDS ]}
        private PlayerController _owner;
        private List<PlayerController> _members;
        #endregion

        private GroupController(PlayerController initiator, PlayerController member) {
            _owner = initiator;
            _members = new List<PlayerController> {
                initiator,
                member
            };
        }

        public void Initialize() {

        }

    }
}
