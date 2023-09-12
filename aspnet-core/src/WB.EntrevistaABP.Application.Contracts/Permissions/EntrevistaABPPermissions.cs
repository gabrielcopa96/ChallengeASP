namespace WB.EntrevistaABP.Permissions;

public static class EntrevistaABPPermissions
{
    public const string GroupName = "EntrevistaABP";

    //Add your own permission names. Example:
    //public const string MyPermission1 = GroupName + ".MyPermission1";

    public static class Pasajeros
    {
        public const string Default = GroupName + ".Pasajeros";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";

    }

    public static class Viajes
    {
        public const string Default = GroupName + ".Viajes";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";

    }
}
