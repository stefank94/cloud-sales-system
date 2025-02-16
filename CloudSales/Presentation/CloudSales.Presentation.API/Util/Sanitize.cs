using CloudSales.Presentation.API.Exceptions;

namespace CloudSales.Presentation.API.Util
{
    public static class Sanitize
    {
        public static int Integer(int? value, int? min, int? max, string name)
        {
            if (value is null)
            {
                throw new InvalidParameterException($"{name} must be provided");
            }

            if (min is not null && value < min)
            {
                throw new InvalidParameterException($"{name} cannot be less than {min}");
            }

            if (max is not null && value > max)
            {
                throw new InvalidParameterException($"{name} cannot be greater than {max}");
            }

            return value.Value;
        }
    }
}
