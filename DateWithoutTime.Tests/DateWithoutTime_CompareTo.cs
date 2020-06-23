using ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ValueObjects.Tests
{
    public class DateWithoutTime_CompareTo
    {
        [Theory]
        [InlineData(01, 01, 2019, 02, 01, 2019, -1)]
        [InlineData(01, 01, 2019, 30, 01, 2019, -1)]
        [InlineData(01, 01, 2019, 01, 02, 2019, -1)]
        [InlineData(01, 01, 2018, 01, 01, 2019, -1)]

        [InlineData(01, 01, 2019, 01, 01, 2019, 0)]
        [InlineData(01, 02, 2019, 01, 02, 2019, 0)]
        [InlineData(01, 01, 2020, 01, 01, 2020, 0)]

        [InlineData(02, 01, 2019, 01, 01, 2019, 1)]
        [InlineData(30, 01, 2019, 01, 01, 2019, 1)]
        [InlineData(01, 02, 2019, 01, 01, 2019, 1)]
        [InlineData(01, 01, 2020, 01, 01, 2019, 1)]
        public void CompareTo_Should_BeEqual(int firstDay, int firstMonth, int firstYear, int secondDay, int secondMonth, int secondYear, int expectedResult)
        {
            DateWithoutTime firstDate = new DateWithoutTime(firstDay, firstMonth, firstYear);
            DateWithoutTime secondDate = new DateWithoutTime(secondDay, secondMonth, secondYear);

            int actualResult = firstDate.CompareTo(secondDate);

            Assert.Equal(expectedResult, actualResult);
        }
    }
}
