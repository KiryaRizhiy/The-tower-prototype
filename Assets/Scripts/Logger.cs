using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Logger
{
    private static List<LogContentInfo> LogStorage = new List<LogContentInfo>();

    public static void UpdateContent(UILogDataTypes type, string info, bool isSFC = false)
    {
        if (LogStorage.Find(x => x.dataType == type) == null)
        {
            LogStorage.Add(new LogContentInfo(type, info, isSFC));
        }
        else
        {
            LogStorage.Find(x => x.dataType == type).UpdateContent(info, isSFC);
        }
    }
    public static void AddContent(UILogDataTypes type, string info, bool isSFC = false)
    {
        if (LogStorage.Find(x => x.dataType == type) == null)
        {
            LogStorage.Add(new LogContentInfo(type, info, isSFC));
        }
        else
        {
            LogStorage.Find(x => x.dataType == type).AddContent(info);
        }
    }
    public static string GetAllContent()
    {
        string text = "";
        foreach (UILogDataTypes _dataType in Enum.GetValues(typeof(UILogDataTypes)))
        {
            if (!String.IsNullOrEmpty(text))
                text = text + System.Environment.NewLine;
            text = text + GetContentByType(_dataType);
        }
        return text;
    }
    public static void RemoveAllContent()
    {
        foreach (UILogDataTypes _dataType in Enum.GetValues(typeof(UILogDataTypes)))
            LogStorage.Remove(LogStorage.Find(x => x.dataType == _dataType));
    }
    private static string GetContentByType(UILogDataTypes type)
    {
        LogContentInfo currContent = LogStorage.Find(x => x.dataType == type);
        if (currContent == null)
            return null;
        else
        {
            string info = currContent.content;
            if (currContent.isSingleFrameContent) LogStorage.Remove(currContent);
            return info;
        }
    }
    private class LogContentInfo
    {
        public UILogDataTypes dataType;
        public bool isSingleFrameContent {get; private set;}
        public string content;
        public LogContentInfo(UILogDataTypes type, string info, bool isSFC)
        {
            dataType = type;
            isSingleFrameContent = isSFC;
            content = info;
        }
        public void UpdateContent(string info, bool isSFC)
        {
            content = info;
            isSingleFrameContent = isSFC;
        }
        public void AddContent(string info)
        {
            content = content + " : " + info;
        }
    }
}
public enum UILogDataTypes {SceneData,PressedButton,BallState,GameEvents};
