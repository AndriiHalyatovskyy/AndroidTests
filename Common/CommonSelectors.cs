namespace ClassLibrary1.Common
{
    public static class CommonSelectors
    {
        /// <summary>
        /// Returns selector of element to scroll
        /// </summary>
        /// <param name="id">Parent id</param>
        /// <param name="name">Child name</param>
        /// <returns></returns>
        public static string ScrollToElementSelector(string id, string name)
        {
            return $"new UiScrollable(new UiSelector().resourceId(\"{id}\")).scrollIntoView(new UiSelector().text(\"{name}\"));";
        }
    }
}
