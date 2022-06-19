using Teuria;

namespace PongPing;

public class Score : Label 
{
    public int ScoreOf 
    {
        get 
        {
            return score;
        }
        set 
        {
            score = value;
            Text = score.ToString();
        }
    }

    private int score;

    public Score(FontText spriteFont, float offset = 0) : base(spriteFont)
    {
        Text = "0";
        Position.X = (ProgramWidth / 2.16f - spriteFont.MeasureStringHalf()) + offset;
        Position.Y = 0;
    }

    public void AddScore(int score) 
    {
        Text = score.ToString();
    }

    public int GetScore() 
    {
        if (!int.TryParse(Text, out int score)) 
        {
            return 0;
        }
        return score;
    }
}