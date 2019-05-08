public struct Pair 
{
    private int key;
    public int Key
    {
        get
        {
            return key;
        }
        set
        {
            key = value;
        }
    }

    private int pairValue;
    public int PairValue
    {
        get
        {
            return pairValue;
        }
        set
        {
            pairValue = value;
        }
    }

    public Pair(int key, int value)
    {
        this.pairValue = value;
        this.key = key;
    }
}