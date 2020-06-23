using ValueObjects;
using Xunit;

namespace ValueObjects.Tests
{
    public class DateWithoutTime_ToStringCBR
    {
        [Theory]
        [InlineData(1, 3, 2000, "01/03/2000")]
        [InlineData(1, 3, 2004, "01/03/2004")]
        [InlineData(1, 2, 123, "01/02/0123")]
        public void DateToString_Should_ReturnStringDate(int day, int month, int year, string expected)
        {
            var date = new DateWithoutTime(day, month, year);

            string actual = date.ToStringCBR();

            Assert.Equal(expected, actual);
        }
    }
}
