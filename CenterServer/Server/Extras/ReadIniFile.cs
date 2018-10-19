using System.Runtime.InteropServices;
using System.Text;

public class TIniFile
{
    private string FileName;

    [DllImport("kernel32")]
    private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
    [DllImport("kernel32")]
    private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
    //User The Kernel32.dll
    public TIniFile(string _filename)//Read fileName and Path
    {
        FileName = _filename;
    }
    //Using TInifile for my memories of pascal <#3
    public void Write(string section, string key, string value)
    {
        WritePrivateProfileString(section, key, value.ToLower(), FileName);
    }

    public string Read(string section, string key)
    {
        StringBuilder BuildStr = new StringBuilder(255);
        int i = GetPrivateProfileString(section, key, "", BuildStr, 255, FileName);
        return BuildStr.ToString();
    }

    public string FilePath
    {
        get { return FileName; }
        set { FileName = value; }
    }
}