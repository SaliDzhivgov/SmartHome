namespace SmartHome.Web.Common
{
    public static class ValidationConstants
    {
        public const int RoomNameMinLength = 2;
        public const int RoomNameMaxLength = 50;
        public const int RoomDescriptionMaxLength = 300;
        public const int RoomFloorMin = -2;
        public const int RoomFloorMax = 50;

        public const int CategoryNameMinLength = 2;
        public const int CategoryNameMaxLength = 40;
        public const int CategoryIconMaxLength = 40;
        public const int CategoryDescriptionMaxLength = 300;

        public const int ManufacturerNameMinLength = 2;
        public const int ManufacturerNameMaxLength = 60;
        public const int ManufacturerCountryMinLength = 2;
        public const int ManufacturerCountryMaxLength = 50;
        public const int ManufacturerWebsiteMaxLength = 200;

        public const int DeviceNameMinLength = 2;
        public const int DeviceNameMaxLength = 60;
        public const int DeviceSerialMinLength = 5;
        public const int DeviceSerialMaxLength = 30;
        public const string DeviceSerialPattern = @"^[A-Z0-9\-]+$";
        public const double DevicePowerMin = 0.1;
        public const double DevicePowerMax = 5000;
        public const int DeviceNotesMaxLength = 500;
    }
}