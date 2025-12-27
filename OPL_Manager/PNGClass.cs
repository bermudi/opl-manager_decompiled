using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace OPL_Manager;

public class PNGClass
{
	public enum PngColorType : byte
	{
		Grayscale = 0,
		Truecolor = 2,
		Indexed = 3,
		GrayscaleAlpha = 4,
		TruecolorAlpha = 6
	}

	public int Width { get; set; }

	public int Height { get; set; }

	public int Bitdepth { get; set; }

	public int ColorType { get; set; }

	public int Compression { get; set; }

	public int Filter { get; set; }

	public int Interlace { get; set; }

	private bool IsPNG { get; set; }

	public Image Bitmap { get; set; }

	public bool IsPngAnd8Bit
	{
		get
		{
			if (!IsPNG)
			{
				return false;
			}
			if (Bitdepth != 8)
			{
				return false;
			}
			return true;
		}
	}

	public PNGClass(string pngFile)
		: this(File.ReadAllBytes(pngFile))
	{
	}

	public PNGClass(byte[] pngData)
	{
		using MemoryStream memoryStream = new MemoryStream(pngData);
		Bitmap = Image.FromStream((Stream)memoryStream);
		if (!((object)ImageFormat.Png).Equals((object?)Bitmap.RawFormat))
		{
			IsPNG = false;
			return;
		}
		IsPNG = true;
		memoryStream.Position = 0L;
		List<char> buffer = new List<char>(new char[4]);
		while (!IsHeaderInBuffer(buffer))
		{
			ReadIntoBuffer(ref buffer, memoryStream);
		}
		Width = BitConverter.ToInt32(ReadNBytesAsLittleEndian(memoryStream, 4), 0);
		Height = BitConverter.ToInt32(ReadNBytesAsLittleEndian(memoryStream, 4), 0);
		Bitdepth = ReadSingle(memoryStream);
		ColorType = ReadSingle(memoryStream);
		Compression = ReadSingle(memoryStream);
		Filter = ReadSingle(memoryStream);
		Interlace = ReadSingle(memoryStream);
	}

	private int ReadSingle(MemoryStream rawFileStream)
	{
		return rawFileStream.ReadByte();
	}

	private byte[] ReadNBytesAsLittleEndian(MemoryStream rawFileStream, int n)
	{
		byte[] array = new byte[n];
		int i = 0;
		for (int num = n - 1; i <= num; i++)
		{
			int num2 = rawFileStream.ReadByte();
			array[n - i - 1] = (byte)num2;
		}
		return array;
	}

	private void ReadIntoBuffer(ref List<char> buffer, MemoryStream rawFileStream)
	{
		buffer.RemoveAt(0);
		buffer.Add((char)rawFileStream.ReadByte());
	}

	private bool IsHeaderInBuffer(List<char> buffer)
	{
		IEnumerator enumerator = "IHDR".GetEnumerator();
		foreach (char item in buffer)
		{
			enumerator.MoveNext();
			if (item != (char)enumerator.Current)
			{
				return false;
			}
		}
		return true;
	}
}
