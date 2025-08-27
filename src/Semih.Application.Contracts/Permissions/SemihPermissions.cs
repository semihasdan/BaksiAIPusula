namespace Semih.Permissions;

public static class SemihPermissions
{
    public const string GroupName = "Semih";

    public static class Doctors
    {
        public const string Default = GroupName + ".Doctors";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }
}
