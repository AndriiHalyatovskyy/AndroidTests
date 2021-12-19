using System;
using ClassLibrary1.Common;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using VirtualDevice.Pages;

namespace ClassLibrary1.Pages.HybridAppPages
{
    public class ShopPage : APage<ShopPageSelectors>
    {
        public ShopPage(Page p) : base(p, new ShopPageSelectors())
        {
        }

        /// <summary>
        /// Scrolls to product
        /// </summary>
        /// <param name="productName">Product name</param>
        public void ScrollToProduct(string productName)
        {
            page.ScrollToElement(selectors.ScrollToProduct(productName));
        }

        /// <summary>
        /// Clicks on add to cart button for specified product
        /// </summary>
        /// <param name="productName">Product name</param>
        public void ClickAddToCart(string productName)
        {
            page.SingleTapOnElement(selectors.AddToCartButton(productName));
        }

        /// <summary>
        /// Scrolls to product, and click add to cart
        /// </summary>
        /// <param name="productNames">Product names</param>
        public void AddProductsToCart(params string[] productNames)
        {
            Array.ForEach(productNames, product =>
            {
                ScrollToProduct(product);
                ClickAddToCart(product);
            });
        }

        /// <summary>
        /// Taps on cart button
        /// </summary>
        public void ClickOnCartButton()
        {
            page.SingleTapOnElement(selectors.cartButton);
        }
    }

    public class ShopPageSelectors
    {
        public By cartButton = By.Id("com.androidsample.generalstore:id/appbar_btn_cart");
        public By ScrollToProduct(string productName) => MobileBy.AndroidUIAutomator(CommonSelectors.ScrollToElementSelector("com.androidsample.generalstore:id/rvProductList", productName));
        public By AddToCartButton(string productName) => By.XPath($"//*[@text = '{productName}']/..//*[@text = 'ADD TO CART']");
    }
}
