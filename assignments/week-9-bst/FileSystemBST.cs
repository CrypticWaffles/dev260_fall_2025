using System;
using System.Collections.Generic;
using System.Linq;

namespace FileSystemNavigator
{
    /// <summary>
    /// Binary Search Tree implementation for File System Navigation
    /// 
    /// STUDENT ASSIGNMENT: Implement the TODO methods in this class
    /// This class demonstrates BST concepts through a practical file system simulation
    /// 
    /// Learning Objectives:
    /// - Apply BST operations to hierarchical data
    /// - Implement complex search and filtering operations  
    /// - Practice file system concepts through tree structures
    /// - Build practical navigation and management tools
    /// </summary>
    public class FileSystemBST
    {
        private TreeNode? root;
        private int operationCount;
        private DateTime sessionStart;

        public FileSystemBST()
        {
            root = null;
            operationCount = 0;
            sessionStart = DateTime.Now;
            
            Console.WriteLine("🗂️  File System Navigator Initialized!");
            Console.WriteLine("📁 BST-based file system ready for operations.\n");
        }

        // ============================================
        // 🚀 STUDENT TODO METHODS - IMPLEMENT THESE
        // ============================================

        /// <summary>
        /// TODO #1: Create a new file in the file system
        /// 
        /// Requirements:
        /// - Insert file into BST maintaining proper ordering
        /// - Use file name for BST comparison (case-insensitive)
        /// - Handle duplicate file names (return false if exists)
        /// - Set appropriate file metadata (size, dates, extension)
        /// 
        /// BST Learning: Insertion with custom comparison logic
        /// Real-World: File creation in operating systems
        /// </summary>
        /// <param name="fileName">Name of file to create (e.g., "readme.txt")</param>
        /// <param name="size">File size in bytes (default 1024)</param>
        /// <returns>True if file created successfully, false if already exists</returns>
        public bool CreateFile(string fileName, long size = 1024)
        {
            operationCount++;

            // 3. Handle duplicate file names (return false if exists)
            if (FindFile(fileName) != null)
            {
                return false; 
            }

            // 1. Create FileNode with FileType.File and provided size
            FileNode newFile = new FileNode(fileName, FileType.File, size);

            // 2. Insert into BST using InsertNode helper method
            root = InsertNode(root, newFile);
            // 4. Extension will be automatically extracted in FileNode constructor
            return true; 
        }

        /// <summary>
        /// TODO #2: Create a new directory in the file system
        /// 
        /// Requirements:
        /// - Insert directory into BST with FileType.Directory
        /// - Directories should sort before files with same name
        /// - Set size to 0 for directories (automatic in FileNode constructor)
        /// - Handle duplicate directory names
        /// 
        /// BST Learning: Custom comparison for different node types
        /// Real-World: Directory creation and organization
        /// </summary>
        /// <param name="directoryName">Name of directory to create (e.g., "Documents")</param>
        /// <returns>True if directory created successfully, false if already exists</returns>
        public bool CreateDirectory(string directoryName)
        {
            operationCount++;
            
            if (FindFile(directoryName) != null)
            {
                return false; 
            }

            // 1. Create FileNode with FileType.Directory
            FileNode newDir = new FileNode(directoryName, FileType.Directory);

            // 2. Use same insertion logic as CreateFile but with different type
            root = InsertNode(root, newDir);

            // 3. Directories automatically have size = 0 and no extension
            return true; 
        }

        /// <summary>
        /// TODO #3: Find a specific file by exact name
        /// 
        /// Requirements:
        /// - Search BST efficiently using file name as key
        /// - Case-insensitive search
        /// - Return FileNode if found, null if not found
        /// - Use recursive BST search pattern
        /// 
        /// BST Learning: O(log n) search operations
        /// Real-World: File lookup in operating systems
        /// </summary>
        /// <param name="fileName">Name of file to find (not full path)</param>
        /// <returns>FileNode if found, null otherwise</returns>
        public FileNode? FindFile(string fileName)
        {
            operationCount++;
            
            // 1. Use SearchNode helper method with recursive approach
            // 2. Compare file names case-insensitively
            // 3. Return the FileNode.FileData if found
            
            return SearchNode(root, fileName);
        }

        /// <summary>
        /// TODO #4: Find all files with a specific extension
        /// 
        /// Requirements:
        /// - Traverse entire BST collecting files with matching extension
        /// - Case-insensitive extension comparison (.txt = .TXT)
        /// - Return List of FileNode objects
        /// - Use in-order traversal for consistent ordering
        /// 
        /// BST Learning: Tree traversal with filtering
        /// Real-World: File type searches (find all .cs files)
        /// </summary>
        /// <param name="extension">File extension to search for (.txt, .cs, etc.)</param>
        /// <returns>List of files with matching extension</returns>
        public List<FileNode> FindFilesByExtension(string extension)
        {
            operationCount++;
            List<FileNode> matchingFiles = new List<FileNode>();

            // Clean the search term (remove dot)
            string cleanSearchExt = extension.TrimStart('.');

            TraverseAndCollect(root, matchingFiles, fileNode =>
                fileNode.Type == FileType.File &&
                // Fix: Trim the dot from the fileNode.Extension too
                string.Equals(fileNode.Extension.TrimStart('.'), cleanSearchExt, StringComparison.OrdinalIgnoreCase)
            );

            return matchingFiles;
        }

        /// <summary>
        /// TODO #5: Find all files within a size range
        /// 
        /// Requirements:
        /// - Search for files between minSize and maxSize (inclusive)
        /// - Only include FileType.File items (not directories)
        /// - Return files sorted by name (in-order traversal)
        /// - Handle edge cases (minSize > maxSize)
        /// 
        /// BST Learning: Range queries and filtered traversal
        /// Real-World: Find large files for cleanup, small files for compression
        /// </summary>
        /// <param name="minSize">Minimum file size in bytes</param>
        /// <param name="maxSize">Maximum file size in bytes</param>
        /// <returns>List of files within size range</returns>
        public List<FileNode> FindFilesBySize(long minSize, long maxSize)
        {
            operationCount++;
            
            List<FileNode> sizeFilteredFiles = new List<FileNode>();


            // 1. Validate input parameters (minSize <= maxSize)
            if (minSize > maxSize)
            {
                return sizeFilteredFiles; // Return empty list for invalid range
            }

            // 2. Use TraverseAndCollect with size range filter
            // 3. Only include FileType.File items
            TraverseAndCollect(root, sizeFilteredFiles, fileNode =>
                fileNode.Type == FileType.File &&
                fileNode.Size >= minSize &&
                fileNode.Size <= maxSize
            );

            return sizeFilteredFiles;
        }

        /// <summary>
        /// TODO #6: Find the N largest files in the system
        /// 
        /// Requirements:
        /// - Collect all files and sort by size (descending)
        /// - Return top N largest files
        /// - Handle case where N > total file count
        /// - Only include FileType.File items
        /// 
        /// BST Learning: Tree traversal with post-processing
        /// Real-World: Disk cleanup utilities, storage analysis
        /// </summary>
        /// <param name="count">Number of largest files to return</param>
        /// <returns>List of largest files, sorted by size descending</returns>
        public List<FileNode> FindLargestFiles(int count)
        {
            operationCount++;

            // 1. Collect all files using traversal
            List<FileNode> allFiles = new List<FileNode>();

            TraverseAndCollect(root, allFiles, fileNode => fileNode.Type == FileType.File);

            // 2. Sort by Size property (descending)
            var sortedFiles = allFiles.OrderByDescending(f => f.Size).ToList();

            // 3. Take top 'count' items
            // 4. Handle edge case where count <= 0
            if (count <= 0)
            {
                return new List<FileNode>();
            }

            return sortedFiles.Take(count).ToList();
        }

        /// <summary>
        /// TODO #7: Calculate total size of all files and directories
        /// 
        /// Requirements:
        /// - Traverse entire BST and sum all file sizes
        /// - Include both files and directories in count
        /// - Use recursive traversal approach
        /// - Return total size in bytes
        /// 
        /// BST Learning: Aggregation through tree traversal
        /// Real-World: Disk usage analysis, storage reporting
        /// </summary>
        /// <returns>Total size of all files in bytes</returns>
        public long CalculateTotalSize()
        {
            operationCount++;

            // 1. Use recursive helper method to traverse tree
            // 2. Sum the Size property of all nodes
            // 3. Handle empty tree case (return 0)

            return SumNodes(root);
        }


        /// <summary>
        /// TODO #8: Delete a file or directory from the system
        /// 
        /// Requirements:
        /// - Remove item from BST maintaining tree structure
        /// - Handle all three deletion cases (no children, one child, two children)
        /// - Return true if deleted, false if not found
        /// - Use standard BST deletion algorithm
        /// 
        /// BST Learning: Complex deletion maintaining tree structure
        /// Real-World: File deletion in operating systems
        /// </summary>
        /// <param name="fileName">Name of file or directory to delete</param>
        /// <returns>True if deleted successfully, false if not found</returns>
        public bool DeleteItem(string fileName)
        {
            operationCount++;

            if (FindFile(fileName) == null)
            {
                return false; 
            }

            root = RemoveNode(root, fileName);

            return true; 
        }

        // ============================================
        // 🔧 HELPER METHODS FOR TODO IMPLEMENTATION
        // ============================================

        /// <summary>
        /// Recursive helper to remove a node and fix tree connections
        /// </summary>
        private TreeNode? RemoveNode(TreeNode? node, string fileName)
        {
            // Base case
            if (node == null) return null;

            // 1. Search for the node
            int compare = string.Compare(fileName, node.FileData.Name, StringComparison.OrdinalIgnoreCase);

            if (compare < 0)
            {
                node.Left = RemoveNode(node.Left, fileName);
            }
            else if (compare > 0)
            {
                node.Right = RemoveNode(node.Right, fileName);
            }
            else
            {

                // Case 1 (No children) & Case 2 (One child)
                if (node.Left == null) return node.Right;
                if (node.Right == null) return node.Left;

                // Case 3 (Two children)
                TreeNode successor = GetMinNode(node.Right);

                // Copy the successor's data to the current node
                node.FileData = successor.FileData;

                node.Right = RemoveNode(node.Right, successor.FileData.Name);
            }

            return node;
        }

        /// <summary>
        /// Helper to find the smallest node in a subtree (go left until you can't)
        /// </summary>
        private TreeNode GetMinNode(TreeNode node)
        {
            TreeNode current = node;
            while (current.Left != null)
            {
                current = current.Left;
            }
            return current;
        }

        /// <summary>
        /// Recursive helper to sum the size of a node and all its children
        /// </summary>
        private long SumNodes(TreeNode? node)
        {
            // Base case: If the node is null, it has 0 size
            if (node == null)
            {
                return 0;
            }

            // Recursive Step:
            // Size = My Size + Left Subtree Size + Right Subtree Size
            long currentSize = node.FileData.Size;
            long leftTotal = SumNodes(node.Left);
            long rightTotal = SumNodes(node.Right);

            return currentSize + leftTotal + rightTotal;
        }

        /// <summary>
        /// Helper method for BST insertion
        /// Students should use this in CreateFile and CreateDirectory
        /// </summary>
        private TreeNode? InsertNode(TreeNode? node, FileNode fileData)
        {
            // TODO: Implement recursive BST insertion
            // Base case: if node is null, create new TreeNode
            if (node == null)
            {
                TreeNode newNode = new TreeNode(fileData);
                return newNode;
            }
            // Recursive case: compare names and go left or right
            // Use CompareFileNodes for proper ordering
            if (CompareFileNodes(fileData, node.FileData) < 0)
            {
                node.Left = InsertNode(node.Left, fileData);
            }
            else
            {
                node.Right = InsertNode(node.Right, fileData);
            }

            return node;
        }

        /// <summary>
        /// Helper method for BST searching
        /// Students should use this in FindFile
        /// </summary>
        private FileNode? SearchNode(TreeNode? node, string fileName)
        {
            // Base case: if node is null, return null
            if (node == null)
            {
                return null; // Not found
            }

            // Base case: if names match, return node.FileData
            if (string.Equals(node.FileData.Name, fileName, StringComparison.OrdinalIgnoreCase))
            {
                return node.FileData; // Found
            }

            // Recursive case: compare names and go left or right
            int comparison = string.Compare(fileName, node.FileData.Name, StringComparison.OrdinalIgnoreCase);
            if (comparison < 0)
            {
                return SearchNode(node.Left, fileName);
            }
            else
            {
                return SearchNode(node.Right, fileName);
            }
        }

        /// <summary>
        /// Helper method for collecting nodes during traversal
        /// Students should use this for FindFilesByExtension, FindFilesBySize, etc.
        /// </summary>
        private void TraverseAndCollect(TreeNode? node, List<FileNode> collection, Func<FileNode, bool> filter)
        {
            // TODO: Implement in-order traversal with filtering
            // Base case: if node is null, return
            if (node == null)
            {
                return;
            }
            // Recursive case: traverse left, process current, traverse right
            // Add to collection only if filter returns true
            TraverseAndCollect(node.Left, collection, filter);

            if (filter(node.FileData))
            {
                collection.Add(node.FileData);
            }

            TraverseAndCollect(node.Right, collection, filter);
        }

        /// <summary>
        /// Custom comparison method for file system ordering
        /// Directories come before files, then alphabetical by name
        /// </summary>
        private int CompareFileNodes(FileNode a, FileNode b)
        {
            // Directories sort before files
            if (a.Type != b.Type)
                return a.Type == FileType.Directory ? -1 : 1;
            
            // Then alphabetical by name (case-insensitive)
            return string.Compare(a.Name, b.Name, StringComparison.OrdinalIgnoreCase);
        }

        // ============================================
        // 🎯 PROVIDED UTILITY METHODS
        // ============================================

        /// <summary>
        /// Display the file system tree structure visually
        /// Helps students visualize their BST structure
        /// </summary>
        public void DisplayTree()
        {
            Console.WriteLine("🌳 File System Tree Structure:");
            Console.WriteLine("================================");
            
            if (root == null)
            {
                Console.WriteLine("   (Empty file system)");
                return;
            }
            DisplayTreeEnhanced(root, "", true, true);
            Console.WriteLine("================================\n");
            Console.WriteLine("🌲 Horizontal Level-by-Level View:");
            DisplayTreeByLevels();
        }

        /// <summary>
        /// Enhanced tree display with better visual formatting and clear parent-child relationships
        /// </summary>
        private void DisplayTreeEnhanced(TreeNode? node, string prefix, bool isLast, bool isRoot)
        {
            if (node == null) return;

            // Display current node with enhanced formatting
            string connector = isRoot ? "🌟 " : (isLast ? "└── " : "├── ");
            string nodeInfo = $"{node.FileData.Name}{(node.FileData.Type == FileType.Directory ? "/" : $" ({FormatSize(node.FileData.Size)})")}";
            
            Console.WriteLine(prefix + connector + nodeInfo);

            // Update prefix for children
            string childPrefix = prefix + (isRoot ? "" : (isLast ? "    " : "│   "));

            // Display children with clear Left/Right indicators
            bool hasLeft = node.Left != null;
            bool hasRight = node.Right != null;

            if (hasRight)
            {
                Console.WriteLine(childPrefix + "│");
                Console.WriteLine(childPrefix + "├─(R)─┐");
                DisplayTreeEnhanced(node.Right, childPrefix + "│     ", !hasLeft, false);
            }

            if (hasLeft)
            {
                Console.WriteLine(childPrefix + "│");
                Console.WriteLine(childPrefix + "└─(L)─┐");
                DisplayTreeEnhanced(node.Left, childPrefix + "      ", true, false);
            }
        }

        /// <summary>
        /// Display tree in a horizontal level-by-level format
        /// </summary>
        private void DisplayTreeByLevels()
        {
            if (root == null) return;

            var queue = new Queue<(TreeNode?, int)>();
            queue.Enqueue((root, 0));
            int currentLevel = -1;

            while (queue.Count > 0)
            {
                var (node, level) = queue.Dequeue();
                
                if (level > currentLevel)
                {
                    if (currentLevel >= 0) Console.WriteLine();
                    Console.Write($"Level {level}: ");
                    currentLevel = level;
                }

                if (node != null)
                {
                    Console.Write($"[{node.FileData.Name}{(node.FileData.Type == FileType.Directory ? "/" : "")}] ");
                    queue.Enqueue((node.Left, level + 1));
                    queue.Enqueue((node.Right, level + 1));
                }
                else
                {
                    Console.Write("[null] ");
                }
            }
            Console.WriteLine();
        }


        private string FormatSize(long bytes)
        {
            if (bytes < 1024) return $"{bytes}B";
            if (bytes < 1024 * 1024) return $"{bytes / 1024}KB";
            return $"{bytes / (1024 * 1024)}MB";
        }

        /// <summary>
        /// Get comprehensive statistics about the file system
        /// </summary>
        public FileSystemStats GetStatistics()
        {
            var stats = new FileSystemStats
            {
                TotalOperations = operationCount,
                SessionDuration = DateTime.Now - sessionStart
            };

            if (root != null)
            {
                CalculateStats(root, stats);
            }

            return stats;
        }

        private void CalculateStats(TreeNode? node, FileSystemStats stats)
        {
            if (node == null) return;

            var file = node.FileData;
            if (file.Type == FileType.File)
            {
                stats.TotalFiles++;
                stats.TotalSize += file.Size;
                
                if (file.Size > stats.LargestFileSize)
                {
                    stats.LargestFileSize = file.Size;
                    stats.LargestFile = file.Name;
                }
            }
            else
            {
                stats.TotalDirectories++;
            }

            CalculateStats(node.Left, stats);
            CalculateStats(node.Right, stats);
        }

        /// <summary>
        /// Check if the file system is empty
        /// </summary>
        public bool IsEmpty() => root == null;

        /// <summary>
        /// Load sample data for testing and demonstration
        /// </summary>
        public void LoadSampleData()
        {
            Console.WriteLine("📁 Loading sample file system data...");
            
            // Sample directories
            var sampleDirs = new[]
            {
                "Documents", "Pictures", "Videos", "Music", "Downloads",
                "Projects", "Code", "Images", "Archive"
            };

            // Sample files with extensions and sizes
            var sampleFiles = new[]
            {
                ("readme.txt", 2048L), ("config.json", 1024L), ("app.cs", 5120L),
                ("photo.jpg", 2048000L), ("song.mp3", 4096000L), ("video.mp4", 52428800L),
                ("document.pdf", 1048576L), ("presentation.pptx", 3145728L),
                ("spreadsheet.xlsx", 512000L), ("archive.zip", 10485760L)
            };

            try
            {
                // Create directories
                foreach (var dir in sampleDirs.Take(6))
                {
                    CreateDirectory(dir);
                }

                // Create files
                foreach (var (fileName, size) in sampleFiles.Take(8))
                {
                    CreateFile(fileName, size);
                }

                Console.WriteLine("✅ Sample data loaded successfully!");
            }
            catch (NotImplementedException)
            {
                Console.WriteLine("⚠️  Cannot load sample data - TODO methods not implemented yet");
            }
        }
    }
}