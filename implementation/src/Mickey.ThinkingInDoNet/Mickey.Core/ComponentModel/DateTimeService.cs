using System;

namespace Mickey.Core.ComponentModel
{
    public class DateTimeService
    {
        static DateTime? _FakeDateTime;

        public static DateTime Now
        {
            get
            {
                return _FakeDateTime.HasValue ? _FakeDateTime.Value : DateTime.Now;
            }
        }

        public static void Fake(DateTime? dateTime)
        {
            _FakeDateTime = dateTime;
        }
    }
}
