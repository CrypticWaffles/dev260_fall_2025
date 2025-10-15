Structure	Operation	Big-O (Avg)	One-sentence rationale
Array	Access by index		O(1) because it knows the index and ths can skip straight to it
Array	Search (unsorted)	O(n)	it does not know the index and will have to loop through the array to locate it
List<T>	Add at end		O(1) adding to a list at the end doesnt affect the rest of the list
List<T>	Insert at index		O(n) anything after the added index shifts
Stack<T>	Push / Pop / Peek		O(1) All only affect the top item in the stack
Queue<T>	Enqueue / Dequeue / Peek		O(1) eitehr removing one from the front, adding one to the back, or looking at the next item. No need to go through them all.
Dictionary<K,V>	Add / Lookup / Remove	O(n) i domt really understand dictionaries that well yet to be honest	
HashSet<T>	Add / Contains / Remove		O(1) unordered and each value is unique so you dont have to affect the others
