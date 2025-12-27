using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DiscUtils.Iso9660;
using OplManagerService;
using ZSO;

namespace OPL_Manager;

public static class CommonFuncs
{
	public enum DiscType
	{
		CD,
		DVD
	}

	public enum Modes
	{
		Normal,
		Ethernet,
		USB,
		HDD
	}

	public class ArtSizeClass
	{
		public Size ICO { get; private set; }

		public Size COV { get; private set; }

		public Size COV2 { get; private set; }

		public Size LAB { get; private set; }

		public Size SCR { get; private set; }

		public Size BG { get; private set; }

		public Size LGO { get; private set; }

		public ArtSizeClass(Size _ico, Size _cov, Size _cov2, Size _lab, Size _scr, Size _bg, Size _lgo)
		{
			ICO = _ico;
			COV = _cov;
			COV2 = _cov2;
			LAB = _lab;
			SCR = _scr;
			BG = _bg;
			LGO = _lgo;
		}
	}

	private static OplManagerServiceSoapClient _SoapAPIInst;

	public const string URL_OPLMANAGER_THREAD_PS2HOME = "http://www.ps2-home.com/forum/viewtopic.php?f=13&t=189";

	public const string URL_OPLMANAGER_THREAD_PSXPLACE = "https://www.psx-place.com/threads/opl-manager-tool-to-manage-your-games.19055";

	public const string URL_OPLMANAGER_FACEBOOK = "https://www.facebook.com/OPLMANAGER/";

	public const string URL_OPLMANAGER_HOMEPAGE = "http://oplmanager.com";

	public const string URL_REDUMP_SEARCH = "http://redump.org/discs/quicksearch/";

	public const string OPLM_USER_AGENT = "OPL-Manager/24";

	public static OplManagerServiceSoapClient SoapAPI
	{
		get
		{
			if (_SoapAPIInst == null)
			{
				BasicHttpBinding binding = new BasicHttpBinding
				{
					MaxBufferPoolSize = 2097152L,
					MaxReceivedMessageSize = 2097152L,
					Security = 
					{
						Mode = BasicHttpSecurityMode.Transport
					}
				};
				EndpointAddress remoteAddress = new EndpointAddress("https://oplmanager.com/API/V6/OplManagerService.asmx");
				OplManagerServiceSoapClient oplManagerServiceSoapClient = new OplManagerServiceSoapClient(binding, remoteAddress);
				IEndpointBehavior item = new HttpUserAgentEndpointBehavior("OPL-Manager/24");
				oplManagerServiceSoapClient.Endpoint.EndpointBehaviors.Add(item);
				_SoapAPIInst = oplManagerServiceSoapClient;
			}
			return _SoapAPIInst;
		}
	}

	public static void OpenURL(string url)
	{
		Process.Start(new ProcessStartInfo(url)
		{
			UseShellExecute = true
		});
	}

	public static bool IsNumeric(string input)
	{
		double result;
		return double.TryParse(input, out result);
	}

	public static bool IsDate(string input)
	{
		DateTime result;
		return DateTime.TryParse(input, out result);
	}

	public static string ShowInputDialog(string title, string body, string defaultValue = "")
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Invalid comparison between Unknown and I4
		InputDialog inputDialog = new InputDialog(title, body, defaultValue);
		try
		{
			if ((int)((Form)inputDialog).ShowDialog() == 1)
			{
				return inputDialog.UserInput;
			}
			return null;
		}
		finally
		{
			((IDisposable)inputDialog)?.Dispose();
		}
	}

	public static Stream OpenIsoOrZsoStream(string path)
	{
		if (Path.GetExtension(path).ToLower() == ".zso")
		{
			return new ZSOReadStream(path);
		}
		return new FileStream(path, FileMode.Open);
	}

	public static bool FileExistsAssign(string file, ref string result)
	{
		if (File.Exists(file))
		{
			result = file;
			return true;
		}
		return false;
	}

	public static bool TryArtPngThenJpgIfExistsAssign(string rootPath, string id, string artType, ref string result)
	{
		if (FileExistsAssign(rootPath + id + artType + ".png", ref result))
		{
			return true;
		}
		return FileExistsAssign(rootPath + id + artType + ".jpg", ref result);
	}

	public static string GetMD5(string filePath)
	{
		byte[] hash;
		using (MD5 mD = MD5.Create())
		{
			using (FileStream inputStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 8192))
			{
				mD.ComputeHash(inputStream);
			}
			hash = mD.Hash;
		}
		StringBuilder stringBuilder = new StringBuilder();
		byte[] array = hash;
		foreach (byte b in array)
		{
			stringBuilder.Append($"{b:X2}");
		}
		return stringBuilder.ToString().ToLower();
	}

	public static string FormatFileSize(long byteCount)
	{
		string[] array = new string[7] { " B", " KB", " MB", " GB", " TB", " PB", " EB" };
		if (byteCount == 0L)
		{
			return "0" + array[0];
		}
		long num = Math.Abs(byteCount);
		int num2 = Convert.ToInt32(Math.Floor(Math.Log(num, 1024.0)));
		double num3 = Math.Round((double)num / Math.Pow(1024.0, num2), 1);
		return (double)Math.Sign(byteCount) * num3 + array[num2];
	}

	public static string GetGameIdFromFileName(string input)
	{
		Match match = new Regex("[a-zA-Z0-9][a-zA-Z0-9][a-zA-Z0-9][a-zA-Z0-9]_[0-9][0-9][0-9]\\.[0-9][0-9]").Match(input);
		if (!match.Success)
		{
			return null;
		}
		return match.Value;
	}

	public static string HDL_Dump_GetList(string ipOrHdd)
	{
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		Process process = new Process();
		process.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + "\\lib\\" + OplmSettings.ReadString("HDL_VERSION");
		process.StartInfo.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;
		process.StartInfo.Arguments = "hdl_toc " + ipOrHdd;
		process.StartInfo.UseShellExecute = false;
		process.StartInfo.RedirectStandardOutput = true;
		process.StartInfo.RedirectStandardError = true;
		process.StartInfo.CreateNoWindow = true;
		process.Start();
		string text = process.StandardOutput.ReadToEnd();
		string text2 = process.StandardError.ReadToEnd();
		process.WaitForExit();
		using (StreamWriter streamWriter = new StreamWriter("hdl_log.txt", append: true))
		{
			streamWriter.WriteLine("hdl_log.txt", Environment.NewLine + "==== HDL_Dump_GetList() ====" + Environment.NewLine + text + Environment.NewLine + "==ERROR==" + text2 + Environment.NewLine + "===============" + Environment.NewLine);
		}
		if (!string.IsNullOrEmpty(text2))
		{
			MessageBox.Show(text2.Trim(), "HDL_DUMP.EXE ERROR OUTPUT", (MessageBoxButtons)0, (MessageBoxIcon)16);
			return "";
		}
		return text;
	}

	public static bool IsAddressValid(string addrString)
	{
		return new Regex("^([1]?\\d{1,2}|2[0-4]{1}\\d{1}|25[0-5]{1})(\\.([1]?\\d{1,2}|2[0-4]{1}\\d{1}|25[0-5]{1})){3}$").IsMatch(addrString);
	}

	public static string GetGameIdFromISO(string path)
	{
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			using Stream data = OpenIsoOrZsoStream(path);
			using CDReader cDReader = new CDReader(data, joliet: true);
			if (cDReader.FileExists("SYSTEM.CNF"))
			{
				string input;
				using (StreamReader streamReader = new StreamReader(cDReader.OpenFile("SYSTEM.CNF", FileMode.Open)))
				{
					input = streamReader.ReadToEnd();
				}
				Match match = new Regex("[A-Z][A-Z][A-Z][A-Z]_[0-9][0-9][0-9]\\.[0-9][0-9]").Match(input);
				if (match.Success)
				{
					return match.Value;
				}
			}
			else
			{
				MessageBox.Show("The game:" + Environment.NewLine + path + Environment.NewLine + "doesn't have a SYSTEM.CNF file!!", "Missing SYSTEM.CNF", (MessageBoxButtons)0, (MessageBoxIcon)16);
			}
		}
		catch (Exception)
		{
			MessageBox.Show("This ISO file is invalid/corrupt/in use!" + Environment.NewLine + path, "Bad ISO detected!", (MessageBoxButtons)0, (MessageBoxIcon)16);
			return "";
		}
		return "";
	}

	public static string CRC32StringToHexString(string input)
	{
		uint[] array = new uint[1024];
		uint num = 0u;
		int num2;
		for (int i = 0; i < 256; i++)
		{
			num2 = i << 24;
			for (num = 8u; num != 0; num--)
			{
				num2 = ((num2 >= 0) ? ((num2 << 1) ^ 0x4C11DB7) : (num2 << 1));
			}
			array[255 - i] = (uint)num2;
		}
		num2 = 0;
		byte[] bytes = Encoding.ASCII.GetBytes(input + "\0");
		do
		{
			byte b = bytes[num++];
			num2 = (int)(array[b ^ ((ulong)(num2 >> 24) & 0xFFuL)] ^ ((num2 << 8) & 0xFFFFFF00u));
		}
		while (num < bytes.Length);
		return string.Format($"{num2:X}");
	}

	public static void WriteMemoryToFile(MemoryStream mem, string filename)
	{
		using FileStream fileStream = new FileStream(filename, FileMode.Create, FileAccess.Write);
		byte[] array = new byte[(int)(mem.Length - 1 + 1)];
		mem.Read(array, 0, (int)mem.Length);
		fileStream.Write(array, 0, array.Length);
		mem.Close();
	}

	public static void WriteByteArrayToFile(byte[] bytes, string filename)
	{
		using FileStream fileStream = new FileStream(filename, FileMode.Create, FileAccess.Write);
		fileStream.Write(bytes, 0, bytes.Length);
	}

	public static byte[] HttpGetToByteArray(string url)
	{
		try
		{
			using WebClientOPLM webClientOPLM = new WebClientOPLM();
			return new MemoryStream(webClientOPLM.DownloadData(url)).ToArray();
		}
		catch (Exception)
		{
			return null;
		}
	}

	public static byte[] HttpGetImgToByteArray(string url)
	{
		return ImageCache.GetImageBytes(url);
	}

	public static Image HttpGetImgAsImage(string url)
	{
		return ByteArrayToImage(ImageCache.GetImageBytes(url));
	}

	public static Image ByteArrayToImage(byte[] bytes)
	{
		using MemoryStream memoryStream = new MemoryStream(bytes);
		return Image.FromStream((Stream)memoryStream);
	}

	public static void DeleteFileIfExists(string file)
	{
		if (!string.IsNullOrEmpty(file) && File.Exists(file))
		{
			File.Delete(file);
		}
	}

	public static void DeleteAllArtExtensionsIfExists(string filePath)
	{
		if (string.IsNullOrEmpty(filePath))
		{
			return;
		}
		string directoryName = Path.GetDirectoryName(filePath);
		string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
		string[] array = new string[4] { ".jpg", ".jpeg", ".png", ".bmp" };
		foreach (string text in array)
		{
			string path = Path.Combine(directoryName, fileNameWithoutExtension + text);
			if (File.Exists(path))
			{
				try
				{
					File.Delete(path);
				}
				catch (Exception)
				{
				}
			}
		}
	}

	public static void DeleteFileIfExistsPrompt(string f)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Invalid comparison between Unknown and I4
		if (!string.IsNullOrEmpty(f) && File.Exists(f) && (int)MessageBox.Show("Delete " + f, "", (MessageBoxButtons)4, (MessageBoxIcon)32) == 6)
		{
			File.Delete(f);
		}
	}

	public static string Ps1GameGetID(string vcdFile)
	{
		using (BinaryReader binaryReader = new BinaryReader(File.Open(vcdFile, FileMode.Open)))
		{
			int num = (int)binaryReader.BaseStream.Length;
			if (num > 1100032 && num >= 1104128)
			{
				binaryReader.BaseStream.Seek(1100032L, SeekOrigin.Begin);
				byte[] array = binaryReader.ReadBytes(4096);
				string text = "";
				byte[] array2 = array;
				foreach (byte value in array2)
				{
					text += Convert.ToChar(value);
				}
				Match match = new Regex("[A-Z][A-Z][A-Z][A-Z]_[0-9][0-9][0-9]\\.[0-9][0-9]").Match(text);
				if (match.Success)
				{
					return match.Value;
				}
				Match match2 = new Regex("[A-Z][A-Z][A-Z][A-Z]-[0-9][0-9][0-9]\\.[0-9][0-9]").Match(text);
				if (match2.Success)
				{
					return match2.Value.Replace("-", "_");
				}
			}
		}
		return null;
	}

	public static string BinGetID(string binFile, int maxSearch)
	{
		Regex regex = new Regex("[A-Z][A-Z][A-Z][A-Z]_[0-9][0-9][0-9]\\.[0-9][0-9]");
		Regex regex2 = new Regex("[A-Z][A-Z][A-Z][A-Z]-[0-9][0-9][0-9]\\.[0-9][0-9]");
		using (BinaryReader binaryReader = new BinaryReader(File.Open(binFile, FileMode.Open)))
		{
			int num = (int)binaryReader.BaseStream.Length;
			string text = "";
			while (binaryReader.BaseStream.Position != num && binaryReader.BaseStream.Position < maxSearch * 1000000)
			{
				text += Convert.ToChar(binaryReader.ReadByte());
				if (text.Length > 11)
				{
					text = text.Remove(0, 1);
				}
				if (text.Length >= 11)
				{
					Match match = regex.Match(text);
					if (match.Success)
					{
						return match.Value;
					}
					Match match2 = regex2.Match(text);
					if (match2.Success)
					{
						return match2.Value.Replace("-", "_");
					}
				}
			}
		}
		return null;
	}

	public static bool IsFileOpen(string file)
	{
		try
		{
			File.Open(file, FileMode.Open, FileAccess.Read, FileShare.None).Close();
		}
		catch (Exception ex)
		{
			if (ex is IOException)
			{
				return true;
			}
		}
		return false;
	}

	public static Bitmap ReadImageToBitmap(string file)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Expected O, but got Unknown
		if (File.Exists(file))
		{
			return (Bitmap)Image.FromStream((Stream)new MemoryStream(File.ReadAllBytes(file)));
		}
		return null;
	}

	public static string SanitizeGameTitle(string title, bool removeMultipleSpaces = true)
	{
		title = Regex.Replace(title, "[^\\p{L}\\p{N}\\p{Pd}\\p{Zs}_\\(\\)\\[\\]]+", "");
		if (removeMultipleSpaces)
		{
			title = Regex.Replace(title, "[ ]{2,}", " ");
			title = title.Trim();
		}
		return title;
	}

	public static string GenFakeGameID([Optional] ref List<string> list)
	{
		if (list == null)
		{
			list = new List<string>();
		}
		Random random = new Random();
		string fakeid;
		do
		{
			fakeid = "FAKE_" + random.Next(1, 1000).ToString("000") + "." + random.Next(1, 100).ToString("00");
		}
		while ((GameProvider.GameList.Where((GameInfo x) => (x.ID ?? "") == (fakeid ?? "")).Count() > 0) | list.Contains(fakeid));
		list.Add(fakeid);
		return fakeid;
	}

	public static ArtSizeClass ArtSizes(GameType Plataform)
	{
		Size ico = new Size(128, 128);
		Size cov = new Size(140, 200);
		Size cov2 = new Size(242, 344);
		Size lab = new Size(18, 240);
		Size scr = new Size(250, 188);
		Size bg = new Size(640, 480);
		Size lgo = new Size(300, 125);
		if (Plataform == GameType.POPS)
		{
			ico = new Size(128, 128);
			cov = new Size(200, 200);
			cov2 = new Size(222, 200);
			lab = new Size(12, 200);
		}
		return new ArtSizeClass(ico, cov, cov2, lab, scr, bg, lgo);
	}

	public static string ArtSizeAsTextString(Size size)
	{
		return " (" + size.Width + "x" + size.Height + ")";
	}

	public static void WriteLogFile(string text)
	{
		using StreamWriter streamWriter = new StreamWriter("debug.txt", append: true);
		streamWriter.WriteLine(text);
	}
}
