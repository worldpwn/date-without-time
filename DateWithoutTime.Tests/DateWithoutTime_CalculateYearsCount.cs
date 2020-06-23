using DateWithoutTime;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ValueObjects.Tests
{
    public class DateWithoutTime_CalculateYearsCount
    {
        [Theory]
        [InlineData(01, 01, 2019, 01, 01, 2019, 0)]
        [InlineData(01, 01, 2019, 31, 12, 2019, 0)]
        [InlineData(01, 01, 2019, 01, 01, 2020, 1)]

        [InlineData(01, 01, 2019, 02, 01, 2020, 1)]
        [InlineData(01, 01, 2019, 01, 02, 2020, 1)]
        [InlineData(01, 01, 2000, 01, 01, 2100, 100)]

        [InlineData(02, 01, 2019, 01, 01, 2020, 0)]
        [InlineData(02, 01, 2019, 02, 01, 2020, 1)]

        [InlineData(01, 02, 2019, 01, 01, 2020, 0)]
        [InlineData(01, 02, 2019, 31, 01, 2020, 0)]
        [InlineData(01, 02, 2019, 01, 02, 2020, 1)]

        [InlineData(28, 02, 2019, 29, 02, 2020, 1)]
        [InlineData(29, 02, 2020, 28, 02, 2021, 0)] // Not sure about this case
        public void CalculatedYearsCount_Should_BeCorrect(int beginDay, int beginMonth, int beginYear, int endDay, int endMonth, int endYear, int expectedYearsCount)
        {
            DateWithoutTime beginDate = new DateWithoutTime(beginDay, beginMonth, beginYear);
            DateWithoutTime endDate = new DateWithoutTime(endDay, endMonth, endYear);

            int actualYearsCount = DateWithoutTime.CalculateYearsCount(
                beginDate: beginDate,
                endDate: endDate);

            Assert.Equal(expectedYearsCount, actualYearsCount);
        }

        [Fact]
        public void BeginDate_GreaterThan_EndDate_Should_Throw()
        {
            DateWithoutTime beginDate = new DateWithoutTime(02, 01, 2019);
            DateWithoutTime endDate = new DateWithoutTime(01, 01, 2019);

            Assert.Throws<ArgumentException>(() =>
            {
                DateWithoutTime.CalculateYearsCount(
                    beginDate: beginDate,
                    endDate: endDate);
            });
        }
    }
}
