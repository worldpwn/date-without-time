using System;
using System.Collections.Generic;
using System.Linq;
using ValueObjects;
using Xunit;

namespace ValueObjects.Tests
{
    public class DateWithoutTime_InMonth
	{
        [Fact]
        public void Method_Should_Return_Same_Collection_Count_As_DateTime()
        {
	        int month = 2;
	        int year = 1999;
	        IEnumerable<DateWithoutTime> collection = DateWithoutTime.InMonth(month, year);
	        int dateTimeCount = DateTime.DaysInMonth(month: month, year: year);

            Assert.Equal(dateTimeCount, collection.Count());
        }
    }
}
