namespace SmartHome.Web.Common
{
    // {0} = име на полето, {1} = максимум, {2} = минимум
    public static class ErrorMessages
    {
        public const string Required = "Полето „{0}“ е задължително.";
        public const string LengthBetween = "Полето „{0}“ трябва да е между {2} и {1} символа.";
        public const string MaxLength = "Полето „{0}“ може да съдържа най-много {1} символа.";
        public const string RangeBetween = "Стойността на „{0}“ трябва да е между {1} и {2}.";
        public const string SerialFormat = "Серийният номер може да съдържа само главни латински букви, цифри и тире.";
        public const string InvalidUrl = "Въведете валиден адрес, например https://example.com.";
        public const string DateInFuture = "Полето „{0}“ не може да бъде дата в бъдещето.";

        public const string DuplicateRoomName = "Вече съществува стая с това име.";
        public const string DuplicateCategoryName = "Вече съществува категория с това име.";
        public const string DuplicateManufacturerName = "Вече съществува производител с това име.";
        public const string DuplicateSerialNumber = "Вече съществува устройство с този сериен номер.";
        public const string InvalidIcon = "Изберете икона от списъка.";
        public const string InvalidRoom = "Избраната стая не съществува.";
        public const string InvalidCategory = "Избраната категория не съществува.";
        public const string InvalidManufacturer = "Избраният производител не съществува.";
    }
}