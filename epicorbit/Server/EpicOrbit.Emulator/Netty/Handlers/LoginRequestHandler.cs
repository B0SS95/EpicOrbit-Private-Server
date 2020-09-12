using EpicOrbit.Emulator.Game;
using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Commands;
using EpicOrbit.Emulator.Netty.Interfaces;
using EpicOrbit.Emulator.Network.Handlers;
using EpicOrbit.Emulator.Services;
using EpicOrbit.Server.Data.Models.Enumerables;
using EpicOrbit.Server.Data.Models.ViewModels.Account;
using EpicOrbit.Shared.ViewModel;
using EpicOrbit.Shared.ViewModels.Account;
using System;
using System.Threading.Tasks;

namespace EpicOrbit.Emulator.Netty.Handlers {

    [AutoDiscover("10.0.6435")]
    public class LoginRequestHandler : ICommandHandler<LoginRequest> {
        public void Execute(IClient initiator, LoginRequest command) {

            // dummes spiel amk
            command.sessionID = command.sessionID.Replace("\0", "").Trim();

            Task.Run(async () => {
                try {
                    AccountSessionView session = new AccountSessionView(command.userID, command.sessionID);
                    ValidatedView<GlobalRole> result = await AccountService.Authenticate(session);
                    if (result.IsValid) {

                        if (GameManager.Get(command.userID, out _)) {
                            GameManager.Attach(command.userID, initiator as GameConnectionHandler,
                                null, command.version.Replace("\0", "").Trim().Length != 0);
                            return;
                        }

                        ValidatedView<AccountView> validatedAccountView = await AccountService.RetrieveAccount(session.AccountID);
                        if (validatedAccountView.IsValid) {
                            validatedAccountView.Object.CurrentHangar.Check(initiator.Logger,
                                validatedAccountView.Object.ID, validatedAccountView.Object.Vault);
                            validatedAccountView.Object.CurrentHangar.Calculate();

                            GameManager.Attach(validatedAccountView.Object.ID, initiator as GameConnectionHandler,
                                validatedAccountView.Object, command.version.Replace("\0", "").Trim().Length != 0);
                        } else {
                            initiator.Logger.LogError(new Exception($"Player: '{session.AccountID}': {validatedAccountView.Message}"));
                            initiator.Dispose();
                        }
                    } else {
                        initiator.Send(PacketBuilder.InvalidSession());
                        initiator.Dispose();
                    }
                } catch (Exception e) {
                    initiator.Logger.LogError(e);
                    initiator.Dispose();
                }
            });

        }
    }
}
