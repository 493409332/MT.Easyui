using System;
using System.Collections.Generic;

namespace Complex.Common.Utility
{
    /// <summary>
    /// 页码对象
    /// </summary>
    public struct PageNumber
    {
        public PageNumber(string text, int number)
            : this()
        {
            this.Text = text;
            this.Number = number;
        }

        /// <summary>
        /// 显示文本
        /// </summary>
        public string Text
        {
            get;
            set;
        }

        /// <summary>
        /// 页码数
        /// </summary>
        public int Number
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 分页页码呈现方案
    /// </summary>
    public interface IPageNumberRenderPlan
    {
        /// <summary>
        /// 根据分页情况枚举出每一个要呈现的页码 
        /// </summary>
        /// <param name="pageCount">分页总数</param>
        /// <param name="everyDisplayPageCount">一次显示的页码数 特定常量 （-1:要求一次性显示显示所有页码 0:要求不显示任何页码）</param>
        /// <param name="currentPage">当前页</param>
        /// <param name="beginPageNumber">返回本方案经过计算后得出的 开始页码</param>
        /// <param name="endPageNumber">返回本方案经过计算后得出的 结束页码</param>
        /// <returns></returns>
        IEnumerable<PageNumber> EnumeratePageNumber(int pageCount, int everyDisplayPageCount, int currentPage, out int beginPageNumber, out int endPageNumber);
    }

    /// <summary>
    /// 默认的页面呈现方案
    /// </summary>
    public class DefaultPageNumberRenderPlan : IPageNumberRenderPlan
    {
        public static readonly DefaultPageNumberRenderPlan Instance = new DefaultPageNumberRenderPlan();
        private DefaultPageNumberRenderPlan()
        {
        }

        public IEnumerable<PageNumber> EnumeratePageNumber(int pageCount, int everyDisplayPageCount, int currentPageNumber, out int beginPageNumber, out int endPageNumber)
        {
            if (everyDisplayPageCount == -1) //显示所有页码
            {
                beginPageNumber = 1;
                endPageNumber = pageCount;
            }
            else if (everyDisplayPageCount == 0) //不显示任何页码
            {
                beginPageNumber = 0;
                endPageNumber = 0;
            }
            else
            {
                beginPageNumber = Math.Max(1, currentPageNumber - (currentPageNumber - 1) % everyDisplayPageCount); //本次开始的页数
                endPageNumber = Math.Min(pageCount, beginPageNumber + everyDisplayPageCount - 1);   //本次结束的页数
            }
            return enumerate(beginPageNumber, endPageNumber);
        }

        private IEnumerable<PageNumber> enumerate(int begin, int end)
        {
            if (end == 0)
                yield break;
            for (int i = begin; i <= end; i++)
                yield return new PageNumber(i.ToString(), i);
        }
    }
}
