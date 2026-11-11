namespace SmartHome.Web.Common
{
    // Позволени икони (Bootstrap Icons) и техните описания на български
    public static class CategoryIcons
    {
        public static readonly IReadOnlyDictionary<string, string> All = new Dictionary<string, string>
        {
            ["bi-lightbulb"] = "Крушка",
            ["bi-thermometer-half"] = "Термометър",
            ["bi-shield-lock"] = "Щит",
            ["bi-plug"] = "Контакт",
            ["bi-tv"] = "Телевизор",
            ["bi-gear-wide-connected"] = "Уред",
            ["bi-camera-video"] = "Камера",
            ["bi-speaker"] = "Високоговорител",
            ["bi-wind"] = "Вентилация",
            ["bi-droplet"] = "Вода",
            ["bi-sun"] = "Слънчева енергия",
            ["bi-router"] = "Мрежа"
        };

        public static bool IsValid(string? icon) => icon != null && All.ContainsKey(icon);
    }
}