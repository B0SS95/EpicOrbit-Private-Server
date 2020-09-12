using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class PetStatusCommand : ICommand {

        public short ID { get; set; } = 23546;
        public int petShieldEnergyMax = 0;
        public double petExperiencePoints = 0;
        public int petSpeed = 0;
        public int petMaxFuel = 0;
        public int petCurrentFuel = 0;
        public int petHitPoints = 0;
        public string petName = "";
        public double petExperiencePointsUntilNextLevel = 0;
        public int petHitPointsMax = 0;
        public int petLevel = 0;
        public int petId = 0;
        public int petShieldEnergyNow = 0;

        public PetStatusCommand(int param1 = 0, int param2 = 0, double param3 = 0, double param4 = 0, int param5 = 0, int param6 = 0, int param7 = 0, int param8 = 0, int param9 = 0, int param10 = 0, int param11 = 0, string param12 = "") {
            this.petId = param1;
            this.petLevel = param2;
            this.petExperiencePoints = param3;
            this.petExperiencePointsUntilNextLevel = param4;
            this.petHitPoints = param5;
            this.petHitPointsMax = param6;
            this.petShieldEnergyNow = param7;
            this.petShieldEnergyMax = param8;
            this.petCurrentFuel = param9;
            this.petMaxFuel = param10;
            this.petSpeed = param11;
            this.petName = param12;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.petShieldEnergyMax = param1.ReadInt();
            this.petShieldEnergyMax = param1.Shift(this.petShieldEnergyMax, 5);
            this.petExperiencePoints = param1.ReadDouble();
            this.petSpeed = param1.ReadInt();
            this.petSpeed = param1.Shift(this.petSpeed, 30);
            this.petMaxFuel = param1.ReadInt();
            this.petMaxFuel = param1.Shift(this.petMaxFuel, 12);
            this.petCurrentFuel = param1.ReadInt();
            this.petCurrentFuel = param1.Shift(this.petCurrentFuel, 16);
            this.petHitPoints = param1.ReadInt();
            this.petHitPoints = param1.Shift(this.petHitPoints, 8);
            this.petName = param1.ReadUTF();
            this.petExperiencePointsUntilNextLevel = param1.ReadDouble();
            this.petHitPointsMax = param1.ReadInt();
            this.petHitPointsMax = param1.Shift(this.petHitPointsMax, 11);
            param1.ReadShort();
            this.petLevel = param1.ReadInt();
            this.petLevel = param1.Shift(this.petLevel, 19);
            param1.ReadShort();
            this.petId = param1.ReadInt();
            this.petId = param1.Shift(this.petId, 9);
            this.petShieldEnergyNow = param1.ReadInt();
            this.petShieldEnergyNow = param1.Shift(this.petShieldEnergyNow, 11);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.petShieldEnergyMax, 27));
            param1.WriteDouble(this.petExperiencePoints);
            param1.WriteInt(param1.Shift(this.petSpeed, 2));
            param1.WriteInt(param1.Shift(this.petMaxFuel, 20));
            param1.WriteInt(param1.Shift(this.petCurrentFuel, 16));
            param1.WriteInt(param1.Shift(this.petHitPoints, 24));
            param1.WriteUTF(this.petName);
            param1.WriteDouble(this.petExperiencePointsUntilNextLevel);
            param1.WriteInt(param1.Shift(this.petHitPointsMax, 21));
            param1.WriteShort(-15276);
            param1.WriteInt(param1.Shift(this.petLevel, 13));
            param1.WriteShort(-19952);
            param1.WriteInt(param1.Shift(this.petId, 23));
            param1.WriteInt(param1.Shift(this.petShieldEnergyNow, 21));
        }
    }
}
