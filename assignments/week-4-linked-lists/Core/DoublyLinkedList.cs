using System;
using System.Collections;
using System.Collections.Generic;

namespace Week4DoublyLinkedLists.Core
{
    /*
     * ========================================
     * ASSIGNMENT 3: DOUBLY LINKED LIST IMPLEMENTATION
     * ========================================
     * 
     * 🎯 IMPLEMENTATION GUIDE:
     * Step 1: Node<T> class (already provided below)
     * Step 2: Basic DoublyLinkedList<T> structure (already provided below)
     * Step 3: Add Methods (AddFirst, AddLast, Insert) - START HERE
     * Step 4: Traversal Methods (DisplayForward, DisplayBackward, ToArray)
     * Step 5: Search Methods (Contains, Find, IndexOf)
     * Step 6: Remove Methods (RemoveFirst, RemoveLast, Remove, RemoveAt)
     * Step 7: Advanced Operations (Clear, Reverse)
     * 
     * 💡 TESTING STRATEGY:
     * - Implement each step completely before moving to the next
     * - Use the CoreListDemo to test each step as you complete it
     * - Focus on pointer manipulation - draw diagrams if helpful
     * - Handle edge cases: empty list, single element, etc.
     * 
     * 📚 KEY RESOURCES:
     * - GeeksforGeeks Doubly Linked List: https://www.geeksforgeeks.org/dsa/doubly-linked-list/
     * - Each TODO comment includes specific reference links
     * 
     * 🚀 START WITH: Step 3 (Add Methods) - look for "STEP 3A" below
     */
    /// <summary>
    /// STEP 1: Node class for doubly linked list (✅ COMPLETED)
    /// Contains data and pointers to next and previous nodes
    /// 📚 Reference: https://www.geeksforgeeks.org/dsa/doubly-linked-list/#node-structure
    /// </summary>
    /// <typeparam name="T">Type of data stored in the node</typeparam>
    public class Node<T>
    {
        /// <summary>
        /// Data stored in this node
        /// </summary>
        public T Data { get; set; }
        
        /// <summary>
        /// Reference to the next node in the list
        /// </summary>
        public Node<T>? Next { get; set; }
        
        /// <summary>
        /// Reference to the previous node in the list
        /// </summary>
        public Node<T>? Previous { get; set; }
        
        /// <summary>
        /// Constructor to create a new node with data
        /// </summary>
        /// <param name="data">Data to store in the node</param>
        public Node(T data)
        {
            Data = data;
            Next = null;
            Previous = null;
        }
        
        /// <summary>
        /// String representation of the node for debugging
        /// </summary>
        /// <returns>String representation of the node's data</returns>
        public override string ToString()
        {
            return Data?.ToString() ?? "null";
        }
    }
    
    /// <summary>
    /// STEP 2: Generic doubly linked list implementation (✅ STRUCTURE COMPLETED)
    /// Supports forward and backward traversal with efficient insertion/deletion
    /// 📚 Reference: https://www.geeksforgeeks.org/dsa/doubly-linked-list/
    /// 
    /// 🎯 YOUR TASK: Implement the methods marked with TODO in Steps 3-7
    /// </summary>
    /// <typeparam name="T">Type of elements stored in the list</typeparam>
    public class DoublyLinkedList<T> : IEnumerable<T>
    {
        #region Private Fields
        
        private Node<T>? head;     // First node in the list
        private Node<T>? tail;     // Last node in the list
        private int count;         // Number of elements in the list
        
        #endregion
        
        #region Public Properties
        
        /// <summary>
        /// Gets the number of elements in the list
        /// </summary>
        public int Count => count;
        
        /// <summary>
        /// Gets whether the list is empty
        /// </summary>
        public bool IsEmpty => count == 0;
        
        /// <summary>
        /// Gets the first node in the list (readonly)
        /// </summary>
        public Node<T>? First => head;
        
        /// <summary>
        /// Gets the last node in the list (readonly)
        /// </summary>
        public Node<T>? Last => tail;
        
        #endregion
        
        #region Constructor
        
        /// <summary>
        /// Initialize an empty doubly linked list
        /// </summary>
        public DoublyLinkedList()
        {
            head = null;
            tail = null;
            count = 0;
        }
        
        #endregion
        
        #region Step 3: Add Methods - TODO: Students implement these step by step
        
        /// <summary>
        /// STEP 3A: Add an item to the beginning of the list
        /// Time Complexity: O(1)
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/introduction-and-insertion-in-a-doubly-linked-list/#insertion-at-the-beginning-in-doubly-linked-list
        /// </summary>
        /// <param name="item">Item to add</param>
        public void AddFirst(T item)
        {
            // TODO: Step 3a - Implement adding to the beginning of the list
            // 📖 See: https://www.geeksforgeeks.org/dsa/introduction-and-insertion-in-a-doubly-linked-list/#insertion-at-the-beginning-in-doubly-linked-list
            // Create a new node with the item
            Node<T> newNode  = new Node<T>(item);

            // If list is empty, set head and tail to new node
            if (IsEmpty)
            {
                head = newNode;
                tail = newNode;
            }
            // If list is not empty
            else
            {
                // Set new node's Next to current head
                newNode.Next = head;

                // Set current head's Previous to new node
                head.Previous = newNode;

                // Update head to new node
                head = newNode;
            }

            // Increment count
            count++;
        }
        
        /// <summary>
        /// STEP 3B: Add an item to the end of the list
        /// Time Complexity: O(1)
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/introduction-and-insertion-in-a-doubly-linked-list/#insertion-at-the-end-in-doubly-linked-list
        /// </summary>
        /// <param name="item">Item to add</param>
        public void AddLast(T item)
        {
            // TODO: Step 3b - Implement adding to the end of the list
            // 📖 See: https://www.geeksforgeeks.org/dsa/introduction-and-insertion-in-a-doubly-linked-list/#insertion-at-the-end-in-doubly-linked-list

            // Create a new node with the item
            Node<T> newNode = new Node<T>(item);

            // If list is empty, set head and tail to new node
            if (IsEmpty)
            {
                head = newNode;
                tail = newNode;
            }
            else
            {
                // Set current tail's Next to new node
                tail.Next = newNode;

                // Set new node's Previous to current tail
                newNode.Previous = tail;

                // Update tail to new node
                tail = newNode;
            }

            // Increment count
            count++;
        }

        /// <summary>
        /// Convenience method - calls AddLast
        /// </summary>
        /// <param name="item">Item to add</param>
        public void Add(T item) => AddLast(item);
        
        /// <summary>
        /// STEP 3C: Insert an item at a specific index
        /// Time Complexity: O(n)
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/introduction-and-insertion-in-a-doubly-linked-list/#insertion-after-a-given-node-in-doubly-linked-list
        /// </summary>
        /// <param name="index">Index to insert at (0-based)</param>
        /// <param name="item">Item to insert</param>
        public void Insert(int index, T item)
        {
            // 1. Validate index range (0 to count inclusive)
            if (index < 0 || index > count)
                throw new ArgumentOutOfRangeException(nameof(index));

            // 2. Handle special cases:
            if (index == 0)
            {
                AddFirst(item);
                return;
            }
            if (index == count)
            {
                AddLast(item);
                return;
            }

            // 3. For middle insertion:
            // - Traverse to the position (use GetNodeAt helper)
            Node<T> current = GetNodeAt(index);

            // - Create new node
            Node<T> newNode = new Node<T>(item);

            // - Update pointers: new node's Next/Previous and surrounding nodes
            newNode.Next = current;
            newNode.Previous = current.Previous;

            if (current.Previous != null)
                current.Previous.Next = newNode;
            current.Previous = newNode;

            // 4. Increment count
            count++;
        }
        
        #endregion
        
        #region Step 4: Traversal and Display Methods - TODO: Students implement these
        
        /// <summary>
        /// STEP 4A: Display the list in forward direction  
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/traversal-in-doubly-linked-list/#forward-traversal
        /// </summary>
        public void DisplayForward()
        {
            // TODO: Step 4a - Implement forward traversal and display
            // 📖 See: https://www.geeksforgeeks.org/dsa/traversal-in-doubly-linked-list/#forward-traversal
            // Start from head node
            Node<T>? current = head;

            // Check if list is empty
            if (IsEmpty)
            {
                Console.WriteLine("The list is empty.");
                return;
            }

            // Traverse using Next pointers until null
            while (current != null)
            {
                // Print each node's data with proper formatting
                if (current.Next == null)
                    Console.Write(current.Data);
                else
                    Console.Write(current.Data + " <-> ");
                current = current.Next;
            }
            Console.WriteLine();

        }

        /// <summary>
        /// STEP 4B: Display the list in backward direction
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/traversal-in-doubly-linked-list/#backward-traversal
        /// </summary>
        public void DisplayBackward()
        {
            // TODO: Step 4b - Implement backward traversal and display
            // This demonstrates the power of doubly linked lists!
            // 📖 See: https://www.geeksforgeeks.org/dsa/traversal-in-doubly-linked-list/#backward-traversal

            // Check if list is empty
            if (IsEmpty)
            {
                Console.WriteLine("The list is empty.");
                return;
            }

            // Start from tail node
            Node<T>? current = tail;

            // Traverse using Previous pointers until null
            while (current != null)
            {
                // Print each node's data with proper formatting
                if (current.Previous == null)
                    Console.Write(current.Data);
                else
                    Console.Write(current.Data + " <-> ");
                current = current.Previous;
            }
            Console.WriteLine();
        }

        /// <summary>
        /// STEP 4C: Convert the list to an array
        /// Time Complexity: O(n)
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/traversal-in-doubly-linked-list/
        /// </summary>
        /// <returns>Array containing all list elements</returns>
        public T[] ToArray()
        {
            // TODO: Step 4c - Implement array conversion
            // 📖 See: https://www.geeksforgeeks.org/dsa/traversal-in-doubly-linked-list/
            // Create array of size count
            T[] Arr = new T[count];

            // Traverse the list and copy elements to array
            Node<T>? current = head;
            while (current != null)
            {
                // Find the first empty position in the array and assign the current data
                Arr[Array.IndexOf(Arr, default(T), 0)] = current.Data;
                current = current.Next;
            }

            // Return the populated array
            return Arr;
        }
        
        #endregion
        
        #region Step 5: Search Methods - TODO: Students implement these
        
        /// <summary>
        /// STEP 5A: Check if the list contains a specific item
        /// Time Complexity: O(n)
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/search-an-element-in-a-doubly-linked-list/
        /// </summary>
        /// <param name="item">Item to check for</param>
        /// <returns>True if item is in the list</returns>
        public bool Contains(T item)
        {
            // TODO: Step 5a - Implement contains check
            // 1. Traverse the list from head to tail
            // 2. Compare each node's data with the item
            // 3. Return true if found, false if not found
            // 📖 See: https://www.geeksforgeeks.org/dsa/search-an-element-in-a-doubly-linked-list/
            
            Node<T>? current = head;
            while (current != null)
            {
                if (current.Data!.Equals(item))
                    return true;
                current = current.Next;
            }
            return false;
        }
        
        /// <summary>
        /// STEP 5B: Find the first node containing the specified item
        /// Time Complexity: O(n)
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/search-an-element-in-a-doubly-linked-list/
        /// </summary>
        /// <param name="item">Item to find</param>
        /// <returns>Node containing the item, or null if not found</returns>
        public Node<T>? Find(T item)
        {
            // TODO: Step 5b - Implement find method
            // 1. Traverse the list from head to tail
            // 2. Compare each node's data with the item
            // 3. Return the node if found, null if not found
            // 📖 See: https://www.geeksforgeeks.org/dsa/search-an-element-in-a-doubly-linked-list/
            
            Node<T>? current = head;
            while (current != null)
            {
                if (current.Data!.Equals(item))
                    return current;
                current = current.Next;
            }
            return null;
        }
        
        /// <summary>
        /// STEP 5C: Find the index of an item
        /// Time Complexity: O(n)
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/search-an-element-in-a-doubly-linked-list/
        /// </summary>
        /// <param name="item">Item to find</param>
        /// <returns>Index of the item, or -1 if not found</returns>
        public int IndexOf(T item)
        {
            // TODO: Step 5c - Implement IndexOf method
            // 1. Traverse the list from head to tail
            // 2. Keep track of current index
            // 3. Compare each node's data with the item
            // 4. Return index if found, -1 if not found
            // 📖 See: https://www.geeksforgeeks.org/dsa/search-an-element-in-a-doubly-linked-list/
            
            Node<T>? current = head;
            int index = 0;
            while (current != null)
            {
                if (current.Data!.Equals(item))
                    return index;
                current = current.Next;
                index++;
            }
            return -1;
        }
        
        #endregion
        
        #region Step 6: Remove Methods - TODO: Students implement these
        
        /// <summary>
        /// STEP 6A: Remove the first item in the list
        /// Time Complexity: O(1)
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/delete-a-node-in-a-doubly-linked-list/#deletion-at-the-beginning-in-doubly-linked-list
        /// </summary>
        /// <returns>The removed item</returns>
        public T RemoveFirst()
        {
            // TODO: Step 6a - Implement remove first
            // 📖 See: https://www.geeksforgeeks.org/dsa/delete-a-node-in-a-doubly-linked-list/#deletion-at-the-beginning-in-doubly-linked-list
            // 1. Check if list is empty (throw exception if empty)
            if (IsEmpty)
                throw new InvalidOperationException("Cannot remove from an empty list.");

            // 2. Store the data to return
            T removedData = head!.Data;

            // 3. Update head to head.Next
            head = head.Next;

            // 4. If new head is not null, set its Previous to null
            if (head != null)
            {
                head.Previous = null;
            }

            // 5. If list becomes empty, set tail to null
            if (IsEmpty)
            {
                tail = null;
            }

            // 6. Decrement count
            count--;

            // 7. Return the stored data
            return removedData;
        }
        
        /// <summary>
        /// STEP 6B: Remove the last item in the list
        /// Time Complexity: O(1)
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/delete-a-node-in-a-doubly-linked-list/#deletion-at-the-end-in-doubly-linked-list
        /// </summary>
        /// <returns>The removed item</returns>
        public T RemoveLast()
        {
            // TODO: Step 6b - Implement remove last
            // 📖 See: https://www.geeksforgeeks.org/dsa/delete-a-node-in-a-doubly-linked-list/#deletion-at-the-end-in-doubly-linked-list
            // 1. Check if list is empty (throw exception if empty)
            if (IsEmpty)
                throw new InvalidOperationException("Cannot remove from an empty list.");

            // 2. Store the data to return
            T removedData = tail!.Data;

            // 3. Update tail to tail.Previous
            tail = tail.Previous;

            // 4. If new tail is not null, set its Next to null
            if (tail != null)
            {
                tail.Next = null;
            }

            // 5. If list becomes empty, set head to null
            if (IsEmpty)
            {
                head = null;
            }

            // 6. Decrement count
            count--;

            // 7. Return the stored data
            return removedData;
        }

        /// <summary>
        /// STEP 6C: Remove the first occurrence of an item
        /// Time Complexity: O(n)
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/delete-a-node-in-a-doubly-linked-list/
        /// </summary>
        /// <param name="item">Item to remove</param>
        /// <returns>True if item was found and removed</returns>
        public bool Remove(T item)
        {
            // TODO: Step 6c - Implement remove by value
            // 📖 See: https://www.geeksforgeeks.org/dsa/delete-a-node-in-a-doubly-linked-list/
            // 1. Find the node containing the item (use Find method or traverse)
            Node<T>? nodeToRemove = Find(item);

            // 2. If not found, return false
            if (nodeToRemove == null)
                return false;

            // 3. If found, call RemoveNode helper method
            RemoveNode(nodeToRemove);

            // 4. Return true
            return true;
        }
        
        /// <summary>
        /// STEP 6D: Remove item at a specific index
        /// Time Complexity: O(n)
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/delete-a-node-in-a-doubly-linked-list/#deletion-at-a-specific-position-in-doubly-linked-list
        /// </summary>
        /// <param name="index">Index to remove (0-based)</param>
        /// <returns>The removed item</returns>
        public T RemoveAt(int index)
        {
            // TODO: Step 6d - Implement remove at index
            // 📖 See: https://www.geeksforgeeks.org/dsa/delete-a-node-in-a-doubly-linked-list/#deletion-at-a-specific-position-in-doubly-linked-list
            // 1. Validate index range (0 to count-1)
            if (index < 0 || index >= count)
                throw new ArgumentOutOfRangeException(nameof(index));

            // 2. Handle special cases:
            //    - If index == 0: call RemoveFirst
            //    - If index == count-1: call RemoveLast
            if (index == 0)
                return RemoveFirst();
            if (index == count - 1)
                return RemoveLast();
            // 3. For middle removal:
            //    - Get the node at index (use GetNodeAt helper)
            //    - Store data to return
            //    - Call RemoveNode helper method
            Node<T> nodeToRemove = GetNodeAt(index);
            T removedData = nodeToRemove.Data;
            RemoveNode(nodeToRemove);
            // 4. Return the stored data
            return removedData;
        }
        
        #endregion
        
        #region Step 7: Advanced Operations - TODO: Students implement these
        
        /// <summary>
        /// STEP 7A: Remove all items from the list
        /// Time Complexity: O(1)
        /// 📚 Reference: https://docs.microsoft.com/en-us/dotnet/standard/collections/
        /// </summary>
        public void Clear()
        {
            // TODO: Step 7a - Implement clear
            // 1. Set head and tail to null
            // 2. Set count to 0
            // Note: In C#, garbage collection will handle memory cleanup
            // 📖 See: https://docs.microsoft.com/en-us/dotnet/standard/collections/
            
            head = null;
            tail = null;
            count = 0;
        }

        /// <summary>
        /// STEP 7B: Reverse the list in-place
        /// Time Complexity: O(n)
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/reverse-a-doubly-linked-list/
        /// </summary>
        public void Reverse()
        {
            // 1. Check if list is empty or has only one element
            if (IsEmpty || count <= 1)
                return;

            // 2. Traverse the list and swap Next and Previous pointers for each node
            Node<T>? current = head;
            Node<T>? temp = null;

            while (current != null)
            {
                // 2a. Temporarily store current.Next into temp (which is the *new* Previous)
                temp = current.Next;

                // 2b. Swap Next and Previous pointers
                current.Next = current.Previous;
                current.Previous = temp;

                // 2c. Move to the next node. 
                // The original Next pointer is now stored in temp.
                current = temp;
            }

            // 3. Swap head and tail pointers
            temp = head;
            head = tail;
            tail = temp;
        }

        #endregion

        #region Helper Methods - TODO: Students may need these for advanced operations

        /// <summary>
        /// Get node at specific index (helper for internal use)
        /// Optimizes traversal by starting from head or tail based on index
        /// Used by Insert, RemoveAt, and other positional operations
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/traversal-in-doubly-linked-list/
        /// </summary>
        /// <param name="index">Index to get node at (0-based)</param>
        /// <returns>Node at the specified index</returns>
        private Node<T> GetNodeAt(int index)
        {
            // TODO: Helper Method - Implement optimized node retrieval
            // 📖 See: https://www.geeksforgeeks.org/dsa/traversal-in-doubly-linked-list/
            // 1. Validate index range (0 to count-1)
            if (index < 0 || index >= count)
                throw new ArgumentOutOfRangeException(nameof(index));

            // 2. Optimize traversal direction:
            //    - If index < count/2: start from head and go forward
            //    - If index >= count/2: start from tail and go backward
            if (index < count / 2)
            {
                // 3. Traverse to the specified index
                // 4. Return the node at that position
                // Hint: This optimization makes operations on large lists more efficient
                Node<T>? current = head;
                for (int i = 0; i < index; i++)
                {
                    current = current!.Next;
                }
                return current!;
            }
            else if (index >= count / 2)
            {
                // 3. Traverse to the specified index
                // 4. Return the node at that position
                // Hint: This optimization makes operations on large lists more efficient
                Node<T>? current = tail;
                for (int i = count - 1; i > index; i--)
                {
                    current = current!.Previous;
                }
                return current!;
            }
            throw new InvalidOperationException("Unreachable code reached in GetNodeAt");
        }
        
        /// <summary>
        /// Remove a specific node from the list (helper method)
        /// Handles all the pointer manipulation for node removal
        /// Used by Remove, RemoveAt, and other removal operations
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/delete-a-node-in-a-doubly-linked-list/
        /// </summary>
        /// <param name="node">Node to remove (must not be null)</param>
        private void RemoveNode(Node<T> node)
        {
            // TODO: Helper Method - Implement node removal logic
            // Handle all cases for removing a node:
            // For each case, update the appropriate pointers:
            // - Update Previous node's Next pointer
            // - Update Next node's Previous pointer
            // - Update head/tail if necessary
            // - Decrement count
            // 📖 See: https://www.geeksforgeeks.org/dsa/delete-a-node-in-a-doubly-linked-list/

            // 1. Only node in list (node == head == tail)
            if (node == head && node == tail)
            {
                head = null;
                tail = null;
            }
            // 2. First node (node == head, but not tail)
            else if (node == head)
            {
                head = head!.Next;
                head!.Previous = null;
            }
            // 3. Last node (node == tail, but not head)
            else if (node == tail)
            {
                tail = tail!.Previous;
                tail!.Next = null;
            }
            // 4. Middle node (node has both Previous and Next)
            else
            {
                node.Previous!.Next = node.Next;
                node.Next!.Previous = node.Previous;
            }
        }
        
        #endregion
        
        #region IEnumerable Implementation
        
        /// <summary>
        /// Get enumerator for foreach support
        /// </summary>
        /// <returns>Enumerator for the list</returns>
        public IEnumerator<T> GetEnumerator()
        {
            Node<T>? current = head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }
        
        /// <summary>
        /// Non-generic enumerator implementation
        /// </summary>
        /// <returns>Non-generic enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        #endregion
        
        #region Display Methods for Testing and Debugging
        
        /// <summary>
        /// Display detailed information about the list structure
        /// Perfect for testing and understanding the list state
        /// </summary>
        public void DisplayInfo()
        {
            Console.WriteLine("=== DOUBLY LINKED LIST STATE ===");
            Console.WriteLine($"Count: {Count}");
            Console.WriteLine($"IsEmpty: {IsEmpty}");
            Console.WriteLine($"First: {(head?.Data?.ToString() ?? "null")}");
            Console.WriteLine($"Last: {(tail?.Data?.ToString() ?? "null")}");
            Console.WriteLine();
            
            // Show both traversal directions
            try
            {
                DisplayForward();
            }
            catch (NotImplementedException)
            {
                Console.WriteLine("Forward:  [TODO: Implement DisplayForward in Step 4a]");
            }
            
            try
            {
                DisplayBackward();
            }
            catch (NotImplementedException)
            {
                Console.WriteLine("Backward: [TODO: Implement DisplayBackward in Step 4b]");
            }
            
            Console.WriteLine();
        }
        
        #endregion
    }
}