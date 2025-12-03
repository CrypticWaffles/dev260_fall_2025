using System;
using System.Collections.Generic;

namespace Assignment5
{
    /// <summary>
    /// Manages browser navigation state with back and forward stacks
    /// </summary>
    public class BrowserSession
    {
        private Stack<WebPage> backStack;
        private Stack<WebPage> forwardStack;
        private WebPage? currentPage;

        public WebPage? CurrentPage => currentPage;
        public int BackHistoryCount => backStack.Count;
        public int ForwardHistoryCount => forwardStack.Count;
        public bool CanGoBack => backStack.Count > 0;
        public bool CanGoForward => forwardStack.Count > 0;

        public BrowserSession()
        {
            backStack = new Stack<WebPage>();
            forwardStack = new Stack<WebPage>();
            currentPage = null;
        }

        /// <summary>
        /// Navigate to a new URL
        /// </summary>
        public void VisitUrl(string url, string title)
        {
            // - If there's a current page, push it to back stack
            if (currentPage != null)
            {
                backStack.Push(currentPage);
            }

            // - Clear the forward stack (new navigation invalidates forward history
            forwardStack.Clear();

            // - Set the new page as current
            currentPage = new WebPage(url, title);
        }

        /// <summary>
        /// Navigate back to previous page
        /// </summary>
        public bool GoBack()
        {
            // - Check if back navigation is possible
            if (!CanGoBack)
            {
                return false;
            }

            // - Move current page to forward stack
            if (currentPage != null)
            {
                forwardStack.Push(currentPage);
            }

            // - Pop page from back stack and make it current
            currentPage = backStack.Pop();
            return true;
        }

        /// <summary>
        /// Navigate forward to next page
        /// </summary>
        public bool GoForward()
        {
            // - Check if forward navigation is possible
            if (!CanGoForward)
            {
                return false;
            }

            // - Move current page to back stack
            if (currentPage != null)
            {
                backStack.Push(currentPage);
            }

            // - Pop page from forward stack and make it current
            currentPage = forwardStack.Pop();
            return true;
        }

        /// <summary>
        /// Get navigation status information
        /// </summary>
        public string GetNavigationStatus()
        {
            var status = $"📊 Navigation Status:\n";
            status += $"   Back History: {BackHistoryCount} pages\n";
            status += $"   Forward History: {ForwardHistoryCount} pages\n";
            status += $"   Can Go Back: {(CanGoBack ? "✅ Yes" : "❌ No")}\n";
            status += $"   Can Go Forward: {(CanGoForward ? "✅ Yes" : "❌ No")}";
            return status;
        }

        /// <summary>
        /// Display back history (most recent first)
        /// </summary>
        public void DisplayBackHistory()
        {
            // 1. Print header: "📚 Back History (most recent first):"
            Console.WriteLine("📚 Back History (most recent first):");

            // 2. Check if backStack.Count == 0, if so print "   (No back history)" and return
            if (backStack.Count == 0)
            {
                Console.WriteLine("   (No back history)");
                return;
            }

            // 3. Use foreach loop with backStack to display pages
            int position = 1;
            foreach (var page in backStack)
            {
                // 4. Show position number, page title, and URL for each page
                // 5. Format: "   {position}. {page.Title} ({page.Url})"
                Console.WriteLine($"   {position}. {page.Title} ({page.Url})");
                position++;
            }
        }

        /// <summary>
        /// Display forward history (next page first)
        /// </summary>
        public void DisplayForwardHistory()
        {
            // TODO: Implement forward history display
            // 1. Print header: "📖 Forward History (next page first):"
            Console.WriteLine("📖 Forward History (next page first):");

            // 2. Check if forwardStack.Count == 0, if so print "   (No forward history)" and return
            if (forwardStack.Count == 0)
            {
                Console.WriteLine("   (No forward history)");
                return;
            }

            // 3. Use foreach loop with forwardStack to display pages
            int position = 1;
            foreach (var page in forwardStack)
            {
                // 4. Show position number, page title, and URL for each page
                // 5. Format: "   {position}. {page.Title} ({page.Url})"
                Console.WriteLine($"   {position}. {page.Title} ({page.Url})");
                position++;
            }
        }

        /// <summary>
        /// Clear all navigation history
        /// </summary>
        public void ClearHistory()
        {
            // 1. Calculate total pages:
            int totalCleared = backStack.Count + forwardStack.Count;

            // 2. Clear both stacks: backStack.Clear() and forwardStack.Clear()
            backStack.Clear();
            forwardStack.Clear();

            // 3. Print confirmation message with count of cleared pages
            Console.WriteLine($"✅ Cleared {totalCleared} pages from navigation history.");
        }
    }
}