using System.Collections.Generic;
using SpecFlow.Models;

namespace SpecFlow.Helpers
{
    public class PageLinkModelHelper
    {

        public PageLinkModelHelper()
        {

        }

        public bool ModelIsVisited(List<PageLinkModel> Model)
        {
            bool result = false;

            foreach (PageLinkModel PageLink in Model)
            {
                if (PageLink.PageLinkVisited == true)
                {
                    result = true;
                    continue;
                }

                if (PageLink.PageLinkVisited == false)
                {
                    return false;
                }

            }

            return result;
        }

    }
}
