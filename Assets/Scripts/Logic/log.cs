using NUnit.Framework.Interfaces;
[System.Serializable]
public class log
{
    public string massage;
    public float score;
    public log(string massage, float score)
    {
        this.massage = massage;
        this.score = score;
    }
}
