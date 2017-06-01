using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Mickey.ThinkingInDoNet
{

    public class Obj
    {
        public string code { get; set; }

        public string msg { get; set; }

        public Dictionary<string, List<string>> data { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("WXPAY" + string.Format("{0:yyyyMMddHHmmssffff}", DateTime.Now));

            //Console.WriteLine(Regex.IsMatch("1", @"\d{10}"));

            //var path = @"F:\git\mickey.thinkingInDoNet\implementation\src\Mickey.ThinkingInDoNet\Mickey.ThinkingInDoNet\test.txt";
            //var test = File.ReadAllText(path);

            //for (int i = 0; i < 5; i++)
            //{
            //    File.AppendAllText(path, "\n222222");
            //}
            //Console.Write(File.ReadAllText(path));
            //DropdownFilter<E_SourceType>("", "", "");
            //DropdownFilter<E_SourceType>("", "", "", E_SourceType.AuditAgain, E_SourceType.AuditFirst);

            var str = "{\"code\":\"200\",\"msg\":\"success!\",\"count\":5,\"data\":{\"a BgwGDn9Q = \":[],\"a5x7OTmkQXA--cMHQ1FMyS15CsA0GCD6\":[\"085e9858ed402fd6de8bd0ee7 @wx.tenpay.com\",\"5396363\"],\"a8LVEZ_LFDU--1jiDS3QORGui_otmBOp\":[\"3123731963\"],\"a5x7OTmkQXA--cMHQ1FMyQQpLkkZojhb\":[\"085e9858e872262d2d3261acf @wx.tenpay.com\",\"397685993\"],\"a8LVEZ_LFDU--2vYRKyFZR - FzvEpq_uf\":[\"085e9858eb310ce624ec92341 @wx.tenpay.com\",\"1762028883\"]},\"page\":null,\"pageNum\":null,\"pagetotal\":null}";
            var obj = JsonConvert.DeserializeObject<Obj>(str);
            Console.ReadLine();
        }

        public static string DropdownFilter<TEnum>(string mappingField, string mappingTab, string label, params TEnum[] enums)
        {
            var sb = new StringBuilder();
            sb.Append("<div class=\"dropdown dropdown-filter\">");
            sb.AppendFormat("<button type=\"button\"><span data-original=\"{0}\">{0}</span><i class=\"fa fa-caret-down\"></i></button>", label);
            sb.AppendFormat("<ul class=\"dropdown-menu filter\" mapping-field=\"{0}\" mapping-tab=\"{1}\">", mappingField, mappingTab);
            var values = from TEnum e in enums
                         select $"<li><a href=\"javascript:void(0)\" data=\"{e}\">{GetDescription(e)}</a></li>";
            foreach (var v in values)
            {
                sb.Append(v);
            }
            sb.Append("</ul></div>");
            return sb.ToString();
        }

        public static string DropdownFilter<TEnum>(string mappingField, string mappingTab, string label)
            where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            var enums = (from TEnum e in Enum.GetValues(typeof(TEnum)) select e).ToArray();
            return DropdownFilter(mappingField, mappingTab, label, enums);
        }

        private static string GetDescription(object enumName)
        {

            FieldInfo fieldInfo = enumName.GetType().GetField(enumName.ToString());
            DescriptionAttribute attribute = Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute)) as DescriptionAttribute;
            if (attribute != null)
            {
                return attribute.Description;
            }
            else
            {
                return enumName.ToString();
            }
        }

        /// <summary>
        /// 来源
        /// </summary>
        public enum E_SourceType
        {
            /// <summary>
            /// 初评
            /// </summary>
            [Description("初评")]
            AuditFirst = 1,

            /// <summary>
            /// 再评
            /// </summary>
            [Description("再评")]
            AuditAgain = 2,

            /// <summary>
            /// 主动调级
            /// </summary>
            [Description("主动调级")]
            ResetLevel = 3,

            /// <summary>
            /// 可疑交易
            /// </summary>
            [Description("可疑交易")]
            Suspicious = 4,

            /// <summary>
            /// 名单监控
            /// </summary>
            [Description("名单监控")]
            BlackMonitor = 5
        }
    }
}