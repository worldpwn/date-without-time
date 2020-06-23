using ValueObjects;
using Xunit;

namespace ValueObjects.Tests
{
    public class DateWithoutTime_ToString
    {
        [Theory]
        [InlineData(1, 3, 2000, "01/03/2000/DateWithoutTime")]
        [InlineData(1, 3, 2004, "01/03/2004/DateWithoutTime")]
        [InlineData(1, 2, 123, "01/02/0123/DateWithoutTime")]
        public void DateToString_Should_ReturnStringDate(int day, int month, int year, string expected)
        {
            var date = new DateWithoutTime(day, month, year);

            string actual = date.ToString();

            Assert.Equal(expected, actual);
        }
    }
}
