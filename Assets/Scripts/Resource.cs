using System;

public struct Resource
{
	public int stone;
	public int metal;
	public int chondrule;
	public int crystal;

    public Resource(int stone, int metal, int chondrule, int crystal)
    {
        this.stone = stone;
        this.metal = metal;
        this.chondrule = chondrule;
        this.crystal = crystal;
    }

    public static Resource operator +(Resource a, Resource b)
        => new Resource(a.stone + b.stone, a.metal + b.metal, a.chondrule + b.chondrule, a.crystal + b.crystal);

    public static Resource operator -(Resource a, Resource b)
       => new Resource(a.stone - b.stone, a.metal - b.metal, a.chondrule - b.chondrule, a.crystal - b.crystal);


    public static bool operator >=(Resource a, Resource b)
       => a.stone >= b.stone && a.metal >= b.metal && a.chondrule >= b.chondrule && a.crystal >= b.crystal;

    public static bool operator <=(Resource a, Resource b)
       => a.stone <= b.stone && a.metal <= b.metal && a.chondrule <= b.chondrule && a.crystal <= b.crystal;

    public void ScaleResource(int scale)
    {
        stone *= scale;
        metal *= scale;
        chondrule *= scale;
        crystal *= scale;
    }
}
