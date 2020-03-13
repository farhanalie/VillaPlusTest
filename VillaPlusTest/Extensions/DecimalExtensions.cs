namespace VillaPlus.API.Extensions
{
    public static class DecimalExtensions
    {
        public static decimal? GetPercent(this decimal? value, int percent)
        {
            if (value == null)
                return null;
            var r = value * (1 - ((decimal)percent / 100));
            return r;
        }
    }
}
