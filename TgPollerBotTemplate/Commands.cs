namespace TgPollerBotTemplate
{
    public static class Commands
    {
        public static readonly string Start = "/start";

        public static string Parse(string input)
        {
            return input.Split(' ')[0];
        }
    }
}
