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

    private Transform content;

    private void Awake()
    {
        tableUI = GetComponent<TableUI>();
        content = transform.Find("Content");
    }

    public void Initialize(Func<int> RowCount, Func<int, List<string>> RowContent,bool includingTitle = true)
    {
        this.RowCount += RowCount;
        this.RowContent = RowContent;
        this.includingTitle = includingTitle ? 1 : 0;
        countPerPage = tableUI.Rows - this.includingTitle;
        startIndex = 0;
        Refresh();
    }

    public void Refresh()
    {
        int n = Math.Min(countPerPage, RowCount() - startIndex);
        List<string> temp;
        for (int i = 0; i < n; i++)
        {
            temp = RowContent(i + startIndex);
            for (int j = 0; j < tableUI.Columns; j++)
            {
                tableUI.GetCell(i + includingTitle, j).text = temp[j];
            }
        }
        for (int i = n; i < countPerPage; i++)
        {
            for (int j = 0; j < tableUI.Columns; j++)
            {
                tableUI.GetCell(i + includingTitle, j).text = string.Empty;
            }
        }
        btn_prevPage.gameObject.SetActive(startIndex != 0);
        btn_nextPage.gameObject.SetActive(startIndex != MaxIndex);
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

    public int ClickIndex()
    {
        for (int i = 0; i < tableUI.Rows; i++)
        {
            Transform temp = content.Find($"row{i}");
            if (temp == null)
                continue;
            Bounds bounds = RectTransformUtility.CalculateRelativeRectTransformBounds(temp);
            bounds = new Bounds(bounds.center + temp.position, bounds.size);
            if (bounds.Contains(Input.mousePosition))
                return i - includingTitle;
        }
        return -1;
    }

    private void OnDrawGizmosSelected()
    {
        Transform content = transform.Find("Content");
        for (int i = 0; i < 100; i++)
        {
            Transform temp = content.Find($"row{i}");
            if (temp == null)
                return;
            Bounds bounds = RectTransformUtility.CalculateRelativeRectTransformBounds(temp); 
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(temp.position + bounds.center, bounds.size);
        }
    }
}
