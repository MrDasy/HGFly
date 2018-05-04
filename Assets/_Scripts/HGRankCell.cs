using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Tacticsoft;

public class HGRankCell : TableViewCell {
	public Text rank;
	public Text stat;
	
	public void SetRank(HGRankStat rankstat,int rankord) {
		rank.text = string.Format("第{0}名：",rankord);
		stat.text = string.Format("{0} {1}", rankstat.Username,rankstat.Stat.Score);
	}
}

public class HGRankStat {
	public CharacterStat Stat { set; get; }
	public string Username { set; get; }
	public string Userid { set; get; }
	/*public int CompareTo(HGRankStat tar) {
		if (tar == null) {
			return 1;
		}
		return tar.Stat.Score.CompareTo(this.Stat.Score);
	}*/
}
