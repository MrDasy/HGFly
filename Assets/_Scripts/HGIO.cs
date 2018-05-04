using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class HGIO{
	private static FileStream FS = null;

	public static string PATH_dasdata(string filename) {
		string path = Application.dataPath + "/dasdata";
		Directory.CreateDirectory(path);
		return path + "/" + filename;
	} 

	public static void CLOSE() {
		FS.Flush();
		FS.Close();
	}

	public static string READ(string filepath,FileMode fmode) {
		FS = new FileStream(filepath, fmode, FileAccess.Read);
		int fsLen = (int)FS.Length;
		byte[] bytes = new byte[fsLen];
		char[] data = new char[fsLen];
		FS.Read(bytes, 0, fsLen);
		Decoder dcder = Encoding.UTF8.GetDecoder();
		dcder.GetChars(bytes, 0, fsLen, data, 0);
		return new string(data);
	}

	public static void WRITE(string srcstring,string filepath,FileMode fmode) {
		FS = new FileStream(filepath, fmode, FileAccess.Write);
		byte[] bytes = new byte[srcstring.Length];
		Encoder ecder = Encoding.UTF8.GetEncoder();
		ecder.GetBytes(srcstring.ToCharArray(), 0, srcstring.Length, bytes, 0, true);
		FS.Seek(0, SeekOrigin.Begin);
		FS.Write(bytes, 0, bytes.Length);
	}

	public static void APPEND(string srcstring, string filepath) {
		FS = new FileStream(filepath, FileMode.Append, FileAccess.Write);
		byte[] bytes = new byte[srcstring.Length];
		Encoder ecder = Encoding.UTF8.GetEncoder();
		ecder.GetBytes(srcstring.ToCharArray(), 0, srcstring.Length, bytes, 0, true);
		FS.Seek(0, SeekOrigin.End);
		FS.Write(bytes, 0, bytes.Length);
	}
}
