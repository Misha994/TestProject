using System.Drawing;

public interface IShape
{
    public int speed { get; set; }
    public int point { get; set; }
    public SizeShape size { get; set; }
    public Color color { get; set; }

    public void ChangeSpeed(int speed);
    public void ChangePoint(int point);
    public void ChanheColor(Color color);
}
