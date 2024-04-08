using System;
using System.Collections.Generic;
using UnityEngine;
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
                startIndex = value;
                Show();
            }
        }
    }

    private int includingTitle;
    public int countPerPage;

    public Func<int> RowCount;                  //获取总行数的方法
    public Func<int, List<string>> RowContent;  //生成一行中各项的内容的方法

    private void Awake()
    {
        tableUI = GetComponent<TableUI>();
    }

    public void Initialize(Func<int> RowCount, Func<int, List<string>> RowContent, int countPerPage,bool includingTitle = true)
    {
        this.RowCount += RowCount;
        this.RowContent = RowContent;
        this.countPerPage = countPerPage;
        this.includingTitle = includingTitle ? 1 : 0;
        startIndex = 0;
        Show();
    }

    public void Show()
    {
        int n = Math.Min(countPerPage, RowCount() - startIndex);
        tableUI.Rows = n + includingTitle;
        List<string> temp;
        for (int i = 0; i < n; i++)
        {
            temp = RowContent(i);
            for (int j = 0; j < tableUI.Columns; j++)
            {
                tableUI.GetCell(i, j + includingTitle).text = temp[j];
            }
        }
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
