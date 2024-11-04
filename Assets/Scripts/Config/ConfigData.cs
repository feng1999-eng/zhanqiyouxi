using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ��ȡcsv��ʽ���ݱ�
/// </summary>
public class ConfigData
{
    private Dictionary<int, Dictionary<string, string>> datas; //ÿ�����ݱ�洢���ֵ��У�key���ֵ��id��ֵ��ÿһ������
    public string fileName; //���ñ��ļ�����
    public ConfigData(string fileName)
    {
        this.fileName = fileName;
        this.datas = new Dictionary<int, Dictionary<string, string>>();
    }

    public TextAsset LoadFile()
    {
        return Resources.Load<TextAsset>($"Data/{fileName}");
    }
    //��ȡ
    public void Load(string txt)
    {
        string[] dataArr = txt.Split("\n");
        string[] titleArr = dataArr[0].Trim().Split(","); //�����и� ����õ�һ������,��Ϊÿ���������ֵ��key
        //���ݴӵ����п�ʼ��ȡ��(���±�2��ʼ)
        for(int i = 2;i <dataArr.Length;i++)
        {
            string[] tempArr = dataArr[i].Trim().Split(",");
            Dictionary<string, string> tempData = new Dictionary<string, string>();
            for(int j = 0;j<tempArr.Length;j++)
            {
                tempData.Add(titleArr[j], tempArr[j]);
            }
            datas.Add(int.Parse(tempData["Id"]), tempData);
        }
    }

    public Dictionary<string, string> GetDataById(int id)
    {
        if (datas.ContainsKey(id))
        {
            return datas[id];
        }
        return null;
    }

    public Dictionary<int, Dictionary<string, string>> GetLines()
    {
        return datas;
    }
}
