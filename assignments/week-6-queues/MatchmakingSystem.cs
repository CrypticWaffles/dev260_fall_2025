using System.Numerics;

namespace Assignment6
{
    /// <summary>
    /// Main matchmaking system managing queues and matches
    /// Students implement the core methods in this class
    /// </summary>
    public class MatchmakingSystem
    {
        // Data structures for managing the matchmaking system
        private Queue<Player> casualQueue = new Queue<Player>();
        private Queue<Player> rankedQueue = new Queue<Player>();
        private Queue<Player> quickPlayQueue = new Queue<Player>();
        private List<Player> allPlayers = new List<Player>();
        private List<Match> matchHistory = new List<Match>();

        // Statistics tracking
        private int totalMatches = 0;
        private DateTime systemStartTime = DateTime.Now;

        /// <summary>
        /// Create a new player and add to the system
        /// </summary>
        public Player CreatePlayer(string username, int skillRating, GameMode preferredMode = GameMode.Casual)
        {
            // Check for duplicate usernames
            if (allPlayers.Any(p => p.Username.Equals(username, StringComparison.OrdinalIgnoreCase)))
            {
                throw new ArgumentException($"Player with username '{username}' already exists");
            }

            var player = new Player(username, skillRating, preferredMode);
            allPlayers.Add(player);
            return player;
        }

        /// <summary>
        /// Get all players in the system
        /// </summary>
        public List<Player> GetAllPlayers() => allPlayers.ToList();

        /// <summary>
        /// Get match history
        /// </summary>
        public List<Match> GetMatchHistory() => matchHistory.ToList();

        /// <summary>
        /// Get system statistics
        /// </summary>
        public string GetSystemStats()
        {
            var uptime = DateTime.Now - systemStartTime;
            var avgMatchQuality = matchHistory.Count > 0 
                ? matchHistory.Average(m => m.SkillDifference) 
                : 0;

            return $"""
                🎮 Matchmaking System Statistics
                ================================
                Total Players: {allPlayers.Count}
                Total Matches: {totalMatches}
                System Uptime: {uptime.ToString("hh\\:mm\\:ss")}
                
                Queue Status:
                - Casual: {casualQueue.Count} players
                - Ranked: {rankedQueue.Count} players  
                - QuickPlay: {quickPlayQueue.Count} players
                
                Match Quality:
                - Average Skill Difference: {avgMatchQuality:F1}
                - Recent Matches: {Math.Min(5, matchHistory.Count)}
                """;
        }

        // ============================================
        // STUDENT IMPLEMENTATION METHODS (TO DO)
        // ============================================

        /// <summary>
        /// Add a player to the appropriate queue based on game mode
        /// 
        /// Requirements:
        /// - Add player to correct queue (casualQueue, rankedQueue, or quickPlayQueue)
        /// - Call player.JoinQueue() to track queue time
        /// - Handle any validation needed
        /// </summary>
        public void AddToQueue(Player player, GameMode mode)
        {
            switch (mode)
            {
                case GameMode.Casual:
                    casualQueue.Enqueue(player);
                    player.JoinQueue();
                    break;
                case GameMode.Ranked:
                    rankedQueue.Enqueue(player);
                    player.JoinQueue();
                    break;
                case GameMode.QuickPlay:
                    quickPlayQueue.Enqueue(player);
                    player.JoinQueue();
                    break;
            }
        }

        /// <summary>
        /// TODO: Try to create a match from the specified queue
        /// 
        /// Requirements:
        /// - Return null if not enough players (need at least 2)
        /// - For Casual: Any two players can match (simple FIFO)
        /// - For Ranked: Only players within ±2 skill levels can match
        /// - For QuickPlay: Prefer skill matching, but allow any match if queue > 4 players
        /// - Remove matched players from queue and call LeaveQueue() on them
        /// - Return new Match object if successful
        /// </summary>
        public Match? TryCreateMatch(GameMode mode)
        {
            var targetQueue = GetQueueByMode(mode);

            // 1. Check for minimum player requirement
            if (targetQueue.Count < 2)
            {
                return null;
            }

            // Dequeue the first player, who will be player1
            Player player1 = targetQueue.Dequeue();
            Player? player2 = null;

            if (mode == GameMode.Casual)
            {
                player2 = targetQueue.Dequeue();
            }
            else if (mode == GameMode.Ranked)
            {
                // Use a temporary list to search the queue without modifying it
                var remainingPlayers = targetQueue.ToList();
                int matchIndex = -1;

                // Find the first player in the remaining queue who can match with player1
                for (int i = 0; i < remainingPlayers.Count; i++)
                {
                    if (CanMatchInRanked(player1, remainingPlayers[i]))
                    {
                        player2 = remainingPlayers[i];
                        matchIndex = i;
                        break;
                    }
                }

                if (player2 != null)
                {
                    // Remove player2 from the queue and rebuild it
                    RemoveFromAllQueues(player2);
                }
                else
                {
                    // No suitable match found, put player1 back in the queue and return null
                    targetQueue.Enqueue(player1);
                    return null;
                }
            }
            else if (mode == GameMode.QuickPlay)
            {
                // Use a temporary list to search the queue without modifying it
                var remainingPlayers = targetQueue.ToList();
                int matchIndex = -1;

                // 1. Attempt skill-based match first
                for (int i = 0; i < remainingPlayers.Count; i++)
                {
                    if (CanMatchInRanked(player1, remainingPlayers[i]))
                    {
                        player2 = remainingPlayers[i];
                        matchIndex = i;
                        RemoveFromAllQueues(player2);
                        break; // Found a good match
                    }
                }

                // 2. Fallback: Match anyone if the queue is large 
                if (player2 == null && (targetQueue.Count + 1) > 4)
                {
                    player2 = targetQueue.Dequeue();
                }

                // 3. Fallback: If still no match and queue is small (Count <= 4), put player1 back and return null
                if (player2 == null)
                {
                    targetQueue.Enqueue(player1);
                    return null;
                }
            }

            // If player2 was found, create and return the match
            if (player2 != null)
            {
                player1.LeaveQueue();
                player2.LeaveQueue();

                var match = new Match(player1, player2, mode);
                return match;
            }

            targetQueue.Enqueue(player1);
            return null;
        }

        /// <summary>
        /// Process a match by simulating outcome and updating statistics
        /// 
        /// Requirements:
        /// - Call match.SimulateOutcome() to determine winner
        /// - Add match to matchHistory
        /// - Increment totalMatches counter
        /// - Display match results to console
        /// </summary>
        public void ProcessMatch(Match match)
        {
            match.SimulateOutcome();
            matchHistory.Add(match);
            totalMatches++;
            Console.WriteLine(match.ToDetailedString());
        }

        /// <summary>
        /// Display current status of all queues with formatting
        /// 
        /// Requirements:
        /// - Show header "Current Queue Status"
        /// - For each queue (Casual, Ranked, QuickPlay):
        ///   - Show queue name and player count
        ///   - List players with position numbers and queue times
        ///   - Handle empty queues gracefully
        /// - Use proper formatting and emojis for readability
        /// </summary>
        public void DisplayQueueStatus()
        {
            DisplaySingleQueueStatus(casualQueue, GameMode.Casual);
            Console.WriteLine(new string('-', 30));
            DisplaySingleQueueStatus(rankedQueue, GameMode.Ranked);
            Console.WriteLine(new string('-', 30));
            DisplaySingleQueueStatus(quickPlayQueue, GameMode.QuickPlay);
            Console.WriteLine(new string('-', 30));
        }

        // Helper for DisplayQueueStatus to reduce repetition
        private void DisplaySingleQueueStatus(Queue<Player> queue, GameMode mode)
        {
            Console.WriteLine($"\n⏳ {mode} Queue ({queue.Count} players)");
            if (queue.Count > 0)
            {
                int position = 1;
                foreach (Player player in queue.ToList())
                {
                    Console.WriteLine($"  #{position}: {player.Username} (Skill: {player.SkillRating}), Waiting: {player.GetQueueTime()}");
                    position++;
                }
            }
            else
            {
                Console.WriteLine("  Queue is currently empty.");
            }
        }

        /// <summary>
        /// Display detailed statistics for a specific player
        /// 
        /// Requirements:
        /// - Use player.ToDetailedString() for basic info
        /// - Add queue status (in queue, estimated wait time)
        /// - Show recent match history for this player (last 3 matches)
        /// - Handle case where player has no matches
        /// </summary>
        public void DisplayPlayerStats(Player player)
        {
            // 1. Basic Player Info
            Console.WriteLine(player.ToDetailedString() );

            // 2. Queue Status and Estimated Wait Time
            var currentQueue = GetQueueByMode(player.PreferredMode);
            bool inQueue = currentQueue.Contains(player);

            if (inQueue)
            {
                var queueTime = player.GetQueueTime();
                Console.WriteLine($"\nQueue Status: **In Queue ({player.PreferredMode})**");
                Console.WriteLine($"Current Wait Time: {queueTime}");
                Console.WriteLine($"Estimated Match Wait: {GetQueueEstimate(player.PreferredMode)}");
            }
            else
            {
                Console.WriteLine("\nQueue Status: **Not In Queue**");
            }

            // 3. Recent Match History 
            Console.WriteLine("\n Recent Match History (Last 3)");
            Console.WriteLine(new string('-', 30));

            var recentMatches = matchHistory
                .Where(m => m.Player1 == player || m.Player2 == player)
                .OrderByDescending(m => m.MatchTime)
                .Take(3)
                .ToList();

            if (recentMatches.Count > 0)
            {
                foreach (var match in recentMatches)
                {
                    string opponent = (match.Player1 == player) ? match.Player2.Username : match.Player1.Username;
                    string outcome = "TIE";
                    if (match.Winner != null)
                    {
                        outcome = (match.Winner == player) ? "WIN" : "LOSS";
                    }

                    Console.WriteLine($"- vs. {opponent} ({match.Mode}), Result: **{outcome}**, Skill Diff: {match.SkillDifference:F1}");
                }
            }
            else
            {
                Console.WriteLine("No match history found for this player.");
            }
        }

        /// <summary>
        /// TODO: Calculate estimated wait time for a queue
        /// 
        /// Requirements:
        /// - Return "No wait" if queue has 2+ players
        /// - Return "Short wait" if queue has 1 player
        /// - Return "Long wait" if queue is empty
        /// - For Ranked: Consider skill distribution (harder to match = longer wait)
        /// </summary>
        public string GetQueueEstimate(GameMode mode)
        {
            var targetQueue = GetQueueByMode(mode);
            int count = targetQueue.Count;

            if (count >= 2)
            {
                return "No wait (Match ready)";
            }
            else if (count == 1)
            {
                // One player waiting
                return "Short wait (Waiting for 1 more)";
            }
            else
            {
                // Empty queue
                if (mode == GameMode.Ranked)
                {
                    // Ranked often requires more waiting due to skill constraints
                    return "Very long wait";
                }
                return "Long wait";
            }
        }

        // ============================================
        // HELPER METHODS (PROVIDED)
        // ============================================

        /// <summary>
        /// Helper: Check if two players can match in Ranked mode (±2 skill levels)
        /// </summary>
        private bool CanMatchInRanked(Player player1, Player player2)
        {
            return Math.Abs(player1.SkillRating - player2.SkillRating) <= 2;
        }

        /// <summary>
        /// Helper: Remove player from all queues (useful for cleanup)
        /// </summary>
        private void RemoveFromAllQueues(Player player)
        {
            // Create temporary lists to avoid modifying collections during iteration
            var casualPlayers = casualQueue.ToList();
            var rankedPlayers = rankedQueue.ToList();
            var quickPlayPlayers = quickPlayQueue.ToList();

            // Clear and rebuild queues without the specified player
            casualQueue.Clear();
            foreach (var p in casualPlayers.Where(p => p != player))
                casualQueue.Enqueue(p);

            rankedQueue.Clear();
            foreach (var p in rankedPlayers.Where(p => p != player))
                rankedQueue.Enqueue(p);

            quickPlayQueue.Clear();
            foreach (var p in quickPlayPlayers.Where(p => p != player))
                quickPlayQueue.Enqueue(p);

            player.LeaveQueue();
        }

        /// <summary>
        /// Helper: Get queue by mode (useful for generic operations)
        /// </summary>
        private Queue<Player> GetQueueByMode(GameMode mode)
        {
            return mode switch
            {
                GameMode.Casual => casualQueue,
                GameMode.Ranked => rankedQueue,
                GameMode.QuickPlay => quickPlayQueue,
                _ => throw new ArgumentException($"Unknown game mode: {mode}")
            };
        }
    }
}