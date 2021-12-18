using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using VirtualDevice.Pages;

namespace ClassLibrary1.Pages.Views
{
    public class ViewsPage : APage<ViewsPageSelectors>
    {
        public ViewsPage(Page p) : base(p, new ViewsPageSelectors())
        {

        }

        /// <summary>
        /// Returns count of clickable elements
        /// </summary>
        public int GetCountOfClickableElements()
        {
           return page.GetListElements(selectors.ClickableElements).Count;
        }

        /// <summary>
        /// Open Expandable List page
        /// </summary>
        public void OpenExpandableList()
        {
            page.SingleTapOnElement(selectors.Expandablelist);
        }

        /// <summary>
        /// Open Expandable List page
        /// </summary>
        public void OpenDateWidgets()
        {
            page.SingleTapOnElement(selectors.DateWidgets);
        }

        /// <summary>
        /// Scrolls to specified element
        /// </summary>
        /// <param name="elementToScroll"></param>
        public void ScrollTo(string elementToScroll)
        {
            page.ScrollToElement(selectors.ScrollTo(elementToScroll));
        }

        /// <summary>
        /// Open drag n drop page
        /// </summary>
        public void OpenDragAndDrop()
        {
            page.SingleTapOnElement(selectors.DragNDrop);
        }

        /// <summary>
        /// Drag n drop element
        /// </summary>
        public void DragNDrop()
        {
            page.MoveToElement(selectors.ElementToDrag, selectors.ElementToDragTo, selectors.DraggingElement);
        }
    }

    public class ViewsPageSelectors
    {
        public By ClickableElements = MobileBy.AndroidUIAutomator("new UiSelector().clickable(true)");
        public By Expandablelist = MobileBy.AndroidUIAutomator("text(\"Expandable Lists\")");
        public By DateWidgets = MobileBy.AndroidUIAutomator("text(\"Date Widgets\")");
        public By DragNDrop = MobileBy.AndroidUIAutomator("text(\"Drag and Drop\")");
        public By ElementToDrag = MobileBy.Id("io.appium.android.apis:id/drag_dot_1");
        public By ElementToDragTo = MobileBy.Id("io.appium.android.apis:id/drag_dot_3");
        public By DraggingElement = MobileBy.AndroidUIAutomator("text(\"Dragging...\")");
        public By ScrollTo(string elementToScroll) => MobileBy.AndroidUIAutomator($"new UiScrollable(new UiSelector()).scrollIntoView(text(\"{elementToScroll}\"));");
    }

}
