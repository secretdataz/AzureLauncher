using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace AzureLauncher.Logic
{
    internal static class PageManager
    {
        internal static ContentControl PageContent;
        internal static List<Page> Pages = new List<Page>();

        internal static void SwitchPage(Type t)
        {
            try
            {
                PageContent.Content = Pages.First(x => x.GetType() == t).Content;
            }
            catch
            { }
        }

        internal static void SwitchPage(Page page)
        {
            if (Pages.First(x => x.GetType() == page.GetType()) != null)
            {
                Pages.RemoveAll(x => x.GetType() == page.GetType());
            }
            Pages.Add(page);
            PageContent.Content = page.Content;
        }

        internal static Page GetPage(Type t)
        {
            try
            {
                return Pages.First(x => x.GetType() == t);
            }
            catch
            {
                return null;
            }
        }
    }
}
