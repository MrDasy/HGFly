using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class HGRankListLoader : MonoBehaviour {
	public static void AddRec(HGRankStat rec) {
		string recstr = string.Format("{0}${1}${2}${3}${4}\n", rec.Username, rec.Userid, rec.Stat.Score, rec.Stat.TimeMin, rec.Stat.TimeSec);
		string path = HGIO.PATH_dasdata("rankstat.das");
		HGIO.APPEND(recstr, path);
		HGIO.CLOSE();
	}

	public static HGRankStat GetRec(string target) {
		string[] strtemp=target.Split('$');
		HGRankStat rstat = new HGRankStat();
		rstat.Stat = new CharacterStat();
		rstat.Username = strtemp[0];
		rstat.Userid = strtemp[1];
		rstat.Stat.Score = int.Parse(strtemp[2]);
		rstat.Stat.TimeMin= int.Parse(strtemp[3]);
		rstat.Stat.TimeSec = int.Parse(strtemp[4]);
		return rstat;
	}
}
