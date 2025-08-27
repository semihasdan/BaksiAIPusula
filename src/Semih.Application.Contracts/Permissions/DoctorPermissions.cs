namespace Semih.Permissions;

public static class DoctorPermissions
{
    public const string GroupName = "Doctors";

    public static class Doctor
    {
        public const string Default = GroupName + ".Doctor";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }
}
