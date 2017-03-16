using System;

namespace Mickey.Core.ComponentModel
{
    public static class SequentialGuid
    {
        //详见： http://stackoverflow.com/questions/211498/is-there-a-net-equalent-to-sql-servers-newsequentialid
        //      https://github.com/nhibernate/nhibernate-core/blob/5e71e83ac45439239b9028e6e87d1a8466aba551/src/NHibernate/Id/GuidCombGenerator.cs
        public static Guid NewGuid()
        {
            if (_FakeGuid.HasValue)
                return _FakeGuid.Value;

            byte[] guidArray = Guid.NewGuid().ToByteArray();

            DateTime baseDate = new DateTime(1900, 1, 1);
            DateTime now = DateTime.Now;

            // Get the days and milliseconds which will be used to build the byte string 
            TimeSpan days = new TimeSpan(now.Ticks - baseDate.Ticks);
            TimeSpan msecs = now.TimeOfDay;

            // Convert to a byte array 
            // Note that SQL Server is accurate to 1/300th of a millisecond so we divide by 3.333333 
            byte[] daysArray = BitConverter.GetBytes(days.Days);
            byte[] msecsArray = BitConverter.GetBytes((long)(msecs.TotalMilliseconds / 3.333333));

            // Reverse the bytes to match SQL Servers ordering 
            Array.Reverse(daysArray);
            Array.Reverse(msecsArray);

            // Copy the bytes into the guid 
            Array.Copy(daysArray, daysArray.Length - 2, guidArray, guidArray.Length - 6, 2);
            Array.Copy(msecsArray, msecsArray.Length - 4, guidArray, guidArray.Length - 4, 4);

            return new Guid(guidArray);
        }

        public static string NewGuidString()
        {
            if (_FakeGuid.HasValue)
                return _FakeGuid.Value.ToString();

            byte[] guidArray = Guid.NewGuid().ToByteArray();
            DateTime baseDate = new DateTime(1900, 1, 1);
            DateTime now = DateTime.Now;

            // Get the days and milliseconds which will be used to build the byte string 
            TimeSpan days = new TimeSpan(now.Ticks - baseDate.Ticks);
            TimeSpan msecs = now.TimeOfDay;

            // Convert to a byte array 
            byte[] daysArray = BitConverter.GetBytes(days.Days);
            byte[] msecsArray = BitConverter.GetBytes((long)(msecs.TotalMilliseconds));

            // Copy the bytes into the guid 
            Array.Copy(daysArray, 0, guidArray, 2, 2); //Guid第3、4个字节ToString以后排在首位。
            Array.Copy(msecsArray, 2, guidArray, 0, 2);//Guid第1、2个字节ToString以后紧跟着前面显示。
            Array.Copy(msecsArray, 0, guidArray, 4, 2);//Guid第5、6个字节ToString以后紧跟着前面显示。

            return new Guid(guidArray).ToString();
        }

        static Guid? _FakeGuid;
        public static void Fake(Guid guid)
        {
            _FakeGuid = guid;
        }
    }
}
