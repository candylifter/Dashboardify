namespace Dashboardify.Handlers.Helpers
{
    public static class ValueLenghtHelper
    {
        public static bool CheckIfValueNotTooLong(string value)
        {
            return value.Length < 255;
        }
    }
}
