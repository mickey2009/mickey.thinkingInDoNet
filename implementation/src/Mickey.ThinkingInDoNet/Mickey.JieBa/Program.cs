using JiebaNet.Segmenter;
using System;

namespace Mickey.JieBa
{
    class Program
    {
        static void Main(string[] args)
        {
            var segmenter = new JiebaSegmenter();
            segmenter.LoadUserDict(@"..\..\Resources\userdict.txt");
            var tags = segmenter.Cut("我是产品经理，    会画产品原型", false, false);
            foreach (var tag in tags)
            {
                Console.WriteLine(tag);
            }
            Console.ReadLine();
        }
    }
}
