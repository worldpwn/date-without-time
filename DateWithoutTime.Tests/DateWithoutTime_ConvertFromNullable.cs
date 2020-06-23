using ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ValueObjects.Tests
{
    public class DateWithoutTime_ConvertFromNullable
    {
        [Theory]
        [InlineData(null, null, null)]
        [InlineData(0, null, null)]
        [InlineData(null, 0, null)]
        [InlineData(null, null, 0)]
        [InlineData(null, 11, 1)]
        [InlineData(1, null, 2)]
        [InlineData(2, 3, null)]

        public void SetToNullableVariants_Should_ReturnNull(int? day, int? month, int? year)
        {
            DateWithoutTime date = DateWithoutTime.ConvertFromNullable(day, month, year);
            Assert.Null(date);
        }

        [Fact]

        public void SetToNullString_Should_ReturnNull()
        {
            string dateString = null;
            DateWithoutTime date = DateWithoutTime.ConvertFromNullable(dateString);
            Assert.Null(date);
        }

        [Fact]

        public void SetToCorrectString_Should_ReturnNull()
        {
            string dateString = "01/01/2019/DateWithoutTime";
            DateWithoutTime? date = DateWithoutTime.ConvertFromNullable(dateString);
            Assert.NotNull(date);
            if (date != null)
            {
                Assert.Equal(1, date.Day);
                Assert.Equal(1, date.Month);
                Assert.Equal(2019, date.Year);
            }
        }

    }
}
