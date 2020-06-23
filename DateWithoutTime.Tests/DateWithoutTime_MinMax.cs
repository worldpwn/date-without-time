using ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ValueObjects.Tests
{
    public class DateWithoutTime_MinMax
    {
        [Fact]
        public void Min_Should_Return_EarliestDate()
        {
            DateWithoutTime earliestDate = new DateWithoutTime(31, 12, 2019);
            DateWithoutTime latestDate = new DateWithoutTime(01, 01, 2020);

            DateWithoutTime actualEarliestDate1 = DateWithoutTime.Min(earliestDate, latestDate);
            DateWithoutTime actualEarliestDate2 = DateWithoutTime.Min(latestDate, earliestDate);

            Assert.Equal(earliestDate, actualEarliestDate1);
            Assert.Equal(earliestDate, actualEarliestDate2);
        }

        [Fact]
        public void Max_Should_Return_LatestDate()
        {
            DateWithoutTime earliestDate = new DateWithoutTime(31, 12, 2019);
            DateWithoutTime latestDate = new DateWithoutTime(01, 01, 2020);

            DateWithoutTime actualLatestDate1 = DateWithoutTime.Max(earliestDate, latestDate);
            DateWithoutTime actualLatestDate2 = DateWithoutTime.Max(latestDate, earliestDate);

            Assert.Equal(latestDate, actualLatestDate1);
            Assert.Equal(latestDate, actualLatestDate2);
        }
    }
}
