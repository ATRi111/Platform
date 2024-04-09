using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.TableUI;

[RequireComponent(typeof(TableUI))]
public class TableUIHelper : MonoBehaviour
{
    private TableUI tableUI;

    private int startIndex;
    public int StartIndex
    {
        get => startIndex;
        set
        {
            if(startIndex != value)
            {
                startIndex = Mathf.Clamp(value, 0, MaxIndex);
                Refresh();
            }
        }
    }

    private int includingTitle;
    public int countPerPage;
    public int MaxIndex => RowCount() - RowCount() % countPerPage;

    [SerializeField]
    private Button btn_prevPage;
    [SerializeField]
    private Button btn_nextPage;

    public Func<int> RowCount;                  //获取总行数的方法
    public Func<int, List<string>> RowContent;  //生成一行中各项的内容的方法

    private void Awake()
    {
        tableUI = GetComponent<TableUI>();
        tableUI.Rows = 0;
    }

    public void Initialize(Func<int> RowCount, Func<int, List<string>> RowContent, int countPerPage,bool includingTitle = true)
    {
        this.RowCount += RowCount;
        this.RowContent = RowContent;
        this.countPerPage = countPerPage;
        this.includingTitle = includingTitle ? 1 : 0;
        startIndex = 0;
        Refresh();
    }

    public void Refresh()
    {
        int n = Math.Min(countPerPage, RowCount() - startIndex);
        tableUI.Rows = n + includingTitle;
        List<string> temp;
        for (int i = 0; i < n; i++)
        {
            temp = RowContent(i);
            for (int j = 0; j < tableUI.Columns; j++)
            {
                tableUI.GetCell(i + includingTitle, j).text = temp[j];
            }
        }
        btn_prevPage.interactable = startIndex != 0;
        btn_nextPage.interactable = startIndex != MaxIndex;
    }

    public void NextPage()
    {
        StartIndex += countPerPage;
    }
    public void PrevPage()
    {
        StartIndex -= countPerPage;
    }
    public void FirstPage()
    {
        StartIndex = 0;
    }
    public void LastPage()
    {
        StartIndex = RowCount() - RowCount() % countPerPage;
    }
}
