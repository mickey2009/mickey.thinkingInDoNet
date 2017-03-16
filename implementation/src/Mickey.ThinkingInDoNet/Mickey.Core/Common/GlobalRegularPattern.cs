namespace Mickey.Core.Common
{
    public static class GlobalRegularPattern
    {
        public const string SecurityPassword = @"^(?![0-9a-z]+$)(?![0-9A-Z]+$)(?![0-9\W]+$)(?![a-z\W]+$)(?![a-zA-Z]+$)(?![A-Z\W]+$)[a-zA-Z0-9\W_]{7,20}$";
        public const string SimplyEmail = @"^[_a-zA-Z0-9-]+(\.[_a-zA-Z0-9-]+)*@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*(\.[a-zA-Z]{2,4})$";
        public const string SimplyMobile = @"^1[34578]\d{9}$";
        public const string SimplyPassword = @"((?=.*\d)(?=.*[a-zA-Z]).{7,20})";
        public const string SimplyTelephone = @"^\d[-0-9]{5,20}\d$";
        public const string StrictEmail = @"^\s*[a-zA-Z0-9_%+#&'*/=^`{|}~-](?:\.?[a-zA-Z0-9_%+#&'*/=^`{|}~-])*@(?:[a-zA-Z0-9_](?:(?:\.?|-*)[a-zA-Z0-9_])*\.[a-zA-Z]{2,9}|\[(?:2[0-4]\d|25[0-5]|[01]?\d\d?)\.(?:2[0-4]\d|25[0-5]|[01]?\d\d?)\.(?:2[0-4]\d|25[0-5]|[01]?\d\d?)\.(?:2[0-4]\d|25[0-5]|[01]?\d\d?)])\s*$";
    }
}
