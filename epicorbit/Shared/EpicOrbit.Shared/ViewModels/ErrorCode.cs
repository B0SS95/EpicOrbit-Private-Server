namespace EpicOrbit.Shared.ViewModel {
    public static class ErrorCode {

        public const string ACCOUNT_BANNED = "Your account is banned until {0}! Reason: {1}";
        public const string PASSWORD_USERNAME_NOT_FOUND = "Combination of username and password not found!";
        public const string USERNAME_ALREADY_IN_USE = "Username already in use!";
        public const string PROBLEM_WHILE_CREATING_ACCOUNT = "A problem occured while creating your account! Please try again in few minutes!";
        public const string PROBLEM_WHILE_LOGIN = "A problem occured while trying to login into your account! Please try again in few minutes!";
        public const string EMAIL_NOT_VERIFIED = "You have to verify your e-Mail address first!";
        public const string ACCOUNT_NOT_FOUND = "Updating your account failed, because its identifier (username) was not found!";
        public const string OPERATION_FAILED = "Error occured while performing this operation!";
        public const string INVALID_SESSION = "The session being used is invalid!";
        public const string NO_HANGAR_FOUND = "No active hangar found!";
        public const string EQUIPMENT_NOT_POSSIBLE = "Equipment currently not possible! You need to be in your companys base to push your changes!";
        public const string CLAN_NOT_FOUND = "Clan not found!";
        public const string ACCOUNT_VAULT_NOT_FOUND = "Vault not found!";
        public const string ERROR_WHILE_READING_RESULT = "Error while reading result!";
        public const string CONNECTION_FAILED = "Connection failed!";
        public const string EMAIL_ALREADY_IN_USE = "Email already in use! Please sign into your existing account or reset your password!";
        public const string HANGAR_ALREADY_ACTIVE = "Hangar already active!";
        public const string CLAN_NAME_ALREADY_IN_USE = "Name already in use!";
        public const string CLAN_TAG_ALREADY_IN_USE = "Tag already in use!";
        public const string CLAN_ALREADY_MEMBER = "You are already member of a clan!";
        public const string CLAN_FAILED_TO_REVOKE_REQUEST = "Failed to revoke your join request! Please try again!";
        public const string CLAN_REQUEST_ALREADY_EXISTS = "You have already requested to join this clan!";
        public const string CLAN_ACCEPT_MEMBER_INSUFFICIENT_RIGHTS = "You are not allowed to inspect pending requests!";
        public const string CLAN_PENDING_DID_NOT_REQUEST = "This player revoked his request to join your clan!";
        public const string CLAN_NOT_MEMBER = "You are not member of any clan!";
        public const string CLAN_CANNOT_LEAVE_WHILE_LEADER = "Your clan still has members, to leave you need to give up your position as leader first!";
        public const string CLAN_CANNOT_LEAVE_WHILE_NOT_AT_BASE = "Cannot leave clan! You need to be in your companys base to leave your clan!";
        public const string CLAN_MANAGE_MEMBER_INSUFFICIENT_RIGHTS = "You are not allowed to manage members!";
        public const string CLAN_MANAGE_INSUFFICIENT_RIGHTS = "You are not allowed to manage the clan!";
        public const string CLAN_MANAGE_DIPLOMACIES_INSUFFICIENT_RIGHTS = "You are not allowed to manage the clan diplomacies!";
        public const string CLAN_TARGET_NOT_MEMBER = "Your target is not in your clan!";
        public const string CLAN_FULL = "Clan has already reached the maximum amount of players possible";
        public const string CLAN_RELATION_ALREADY_EXISTS = "There is already a relation with this clan, end it first, before you establish a new one";
        public const string CLAN_PENDING_RELATION_NOT_FOUND = "There is no pending diplomacy for the selected target!";

    }
}
