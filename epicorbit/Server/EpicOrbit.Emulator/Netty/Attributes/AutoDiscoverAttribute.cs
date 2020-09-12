using System;

namespace EpicOrbit.Emulator.Netty.Attributes {

    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class AutoDiscoverAttribute : Attribute {

        public string Identifier { get; set; }

        public AutoDiscoverAttribute(string identifier) {
            Identifier = identifier;
        }

    }
}
