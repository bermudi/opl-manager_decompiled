using System.Collections.Generic;

namespace OPL_Manager;

internal static class QueueExtensions
{
	public static IEnumerable<T> DequeueChunk<T>(this Queue<T> queue, int chunkSize)
	{
		for (int i = 0; i < chunkSize; i++)
		{
			if (queue.Count <= 0)
			{
				break;
			}
			yield return queue.Dequeue();
		}
	}
}
