using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tacticsoft;
using System.IO;

public class HGRankListController : MonoBehaviour ,ITableViewDataSource{
	public HGRankCell m_cellPrefab;
	public TableView m_tableView;
	private static int NumOfRow;
	private static List<HGRankStat> list;

	void Start() {
		NumOfRow = 0;
		StartCoroutine("LoadRankList");
	}

	IEnumerator LoadRankList() {
		list = new List<HGRankStat>();
		string path = HGIO.PATH_dasdata("rankstat.das");
		string srcdata = HGIO.READ(path, FileMode.Open);
		string[] srcrows = srcdata.Split('\n');
		string[] srctemp;
		HGRankStat rstatt;
		foreach (string i in srcrows) {
			if (i.Equals("")) break;
			rstatt = HGRankListLoader.GetRec(i);
			list.Add(rstatt);
			NumOfRow++;
		}
		list.Sort(((x, y) => -(x.Stat.Score.CompareTo(y.Stat.Score))));
		m_tableView.dataSource = this;
		yield break;
	}

	public TableViewCell GetCellForRowInTableView(TableView tableView, int row) {
		HGRankCell cell = tableView.GetReusableCell(m_cellPrefab.reuseIdentifier) as HGRankCell;
		if (cell == null) {
			cell = (HGRankCell)GameObject.Instantiate(m_cellPrefab);
			cell.name = "Rank" + row.ToString();
		}
		HGRankStat rstat=list[row];
		cell.SetRank(rstat,row+1);
		return cell;
	}

	public float GetHeightForRowInTableView(TableView tableView, int row) {
		return (m_cellPrefab.transform as RectTransform).rect.height;
	}

	public int GetNumberOfRowsForTableView(TableView tableView) {
		return NumOfRow;
	}
}
