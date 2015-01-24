using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PriorityQueue
{
	SortedList<int, object> sortedList = new SortedList<int, object>();

	int cumulativeIdx = 0;

	public PriorityQueue(IList list) {
		foreach (object o in list) {
			sortedList.Add(cumulativeIdx++, o);
		}
	}

	public object GetAndTouchHead() {
		int headKey = sortedList.Keys[0];
		object head;
		sortedList.TryGetValue (headKey, out head);
		sortedList.Remove (headKey);
		sortedList.Add (cumulativeIdx++, head);

		return head;
	}
}

