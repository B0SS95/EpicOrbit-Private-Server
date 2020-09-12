namespace EpicOrbit.Emulator.Chat {
    public static class ChatPacketBuilder {

        public static dynamic CreateRoomCommand(string roomName, string roomType, string roomClass, bool isCreator = false) {
            return BasicCommand("/~endpoints/EpicOrbit/changePredefinedRoom/", new {
                newRoomName = roomName,
                roles = "listenChatter,voiceChatter" + (isCreator ? ",creator" : ""),
                roomType,
                roomClass
            }, 8, new { });
        }

        public static dynamic CreateRoomResultCommand(int requestId, bool result) {
            return new {
                path = "/roomSvc/usr/createRoom/",
                data = new {
                    success = result
                },
                t = 2,
                requestId,
                endpointObject = EnpointObject(new { }),
                answerTarget = "EpicOrbit",
                space = "achat"
            };
        }

        public static dynamic ChatInitializationCommand() {
            return BasicCommand(new { }, 3, new { });
        }

        public static dynamic BasicCommand(string path, dynamic data, int t, dynamic endpointObjectData) {
            return new {
                path,
                data,
                t,
                endpointObject = EnpointObject(endpointObjectData)
            };
        }

        public static dynamic BasicCommand(dynamic data, int t, dynamic endpointObjectData) {
            return new {
                data,
                t,
                endpointObject = EnpointObject(endpointObjectData)
            };
        }

        public static dynamic BasicCommand(dynamic data, int t, int requestId, dynamic endpointObjectData) {
            return new {
                data,
                t,
                requestId,
                endpointObject = EnpointObject(endpointObjectData)
            };
        }

        public static dynamic EnpointObject(dynamic data) {
            return new {
                path = "/~endpoints/EpicOrbit/",
                endpoint = "EpicOrbit",
                data
            };
        }

    }
}
