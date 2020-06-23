using System;
using ValueObjects;
using Xunit;

namespace ValueObjects.Tests
{
    public class DateWithoutTime_Ctor
    {
        [Fact]
        public void DateWithDayLessThanOne_Should_ThrowException()
        {
            Assert.ThrowsAny<ArgumentOutOfRangeException>(() =>
            {
                new DateWithoutTime(-1, 1, 2000);
            });

            Assert.ThrowsAny<ArgumentOutOfRangeException>(() =>
            {
                new DateWithoutTime(0, 1, 2000);
            });
        }

        [Fact]
        public void DateWithMonthLessThanOne_Should_ThrowException()
        {
            Assert.ThrowsAny<ArgumentOutOfRangeException>(() =>
            {
                new DateWithoutTime(1, -1, 2000);
            });

            Assert.ThrowsAny<ArgumentOutOfRangeException>(() =>
            {
                new DateWithoutTime(1, 0, 2000);
            });
        }

        [Fact]
        public void DateWithYearLessThanOne_Should_ThrowException()
        {
            Assert.ThrowsAny<ArgumentOutOfRangeException>(() =>
            {
                new DateWithoutTime(1, 1, -1);
            });

            Assert.ThrowsAny<ArgumentOutOfRangeException>(() =>
            {
                new DateWithoutTime(1, 1, 0);
            });
        }

        [Fact]
        public void DateWithDayMoreThanInMonth_Should_ThrowException()
        {
            var nonLeapYear = 2007;
            var leapYear = 2008;

            foreach (var year in new [] { nonLeapYear, leapYear})
            {
                for (int month = 1; month <= 12; month++)
                {
                    int dayInMonth = DateTime.DaysInMonth(year, month);

                    new DateWithoutTime(dayInMonth, month, year);
                    Assert.ThrowsAny<ArgumentOutOfRangeException>(() =>
                    {
                        new DateWithoutTime(dayInMonth + 1, month, year);
                    });
                }
            }
        }

        [Fact]
        public void DateWithMonthMoreThan12_Should_ThrowException()
        {
            Assert.ThrowsAny<ArgumentOutOfRangeException>(() =>
            {
                new DateWithoutTime(1, 13, 2000);
            });
        }

        [Fact]
        public void DateWithoutTimeFromDateTime_Should_Create()
        {
            DateTime expectedDate = new DateTime(2019, 8, 14);

            DateWithoutTime actualDate = new DateWithoutTime(expectedDate);

            Assert.Equal(expectedDate.Day, actualDate.Day);
            Assert.Equal(expectedDate.Month, actualDate.Month);
            Assert.Equal(expectedDate.Year, actualDate.Year);
        }

        [Fact]
        public void CopyConstructor_Should_CreateCopy()
        {
            DateWithoutTime expectedDate = new DateWithoutTime(14, 8, 2019);

            DateWithoutTime actualDate = new DateWithoutTime(expectedDate);

            Assert.Equal(expectedDate, actualDate);
            Assert.False(ValueObject.ReferenceEquals(expectedDate, actualDate));
        }
    }
}
