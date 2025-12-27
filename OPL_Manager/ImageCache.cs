using System.Collections.Generic;
using System.Linq;

namespace OPL_Manager;

public class ImageCache
{
	private static Queue<KeyValuePair<string, byte[]>> imgCache = new Queue<KeyValuePair<string, byte[]>>();

	private static uint totalBytes = 0u;

	private static uint maxBytes = 2000000u;

	public static byte[] GetImageBytes(string url)
	{
		string urlLower = url.ToLower();
		if (imgCache.Any((KeyValuePair<string, byte[]> x) => (x.Key ?? "") == (urlLower ?? "")))
		{
			return imgCache.Single((KeyValuePair<string, byte[]> x) => (x.Key ?? "") == (urlLower ?? "")).Value;
		}
		byte[] array = CommonFuncs.HttpGetToByteArray(url);
		if (array != null)
		{
			imgCache.Enqueue(new KeyValuePair<string, byte[]>(urlLower, array));
			totalBytes = (uint)(totalBytes + array.Length);
		}
		while (totalBytes > maxBytes)
		{
			KeyValuePair<string, byte[]> keyValuePair = imgCache.Dequeue();
			totalBytes = (uint)(totalBytes - keyValuePair.Value.Length);
		}
		return array;
	}
}
