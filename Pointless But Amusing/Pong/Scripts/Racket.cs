using UnityEngine;
using ProtoTurtle.BitmapDrawing;

public class Racket
{
    public bool isValid = false;
    public float x = 0.5f;
    float y;
    PongGame _pongGame;
    Texture2D texture;
    float width = 50;
    float height = 5;
    bool isTop;

    public Racket(PongGame pongGame, bool isTop)
    {
        this._pongGame = pongGame;
        this.texture = pongGame.texture;
        this.isTop = isTop;
        y = (isTop ? pongGame.height * 0.05f : pongGame.height * 0.95f);
    }

    public void Update()
    {
        if (isTop)
        {
            isValid = (_pongGame.GetAnother(-1) == null);
        }
        else
        {
            isValid = (_pongGame.GetAnother(1) == null);
        }
        if (!isValid)
        {
            return;
        }
        var rx = Mathf.Clamp(x * _pongGame.width, width / 2 + 1, _pongGame.width - width / 2 - 1);
        texture.DrawFilledRectangle(new Rect
            (rx - width / 2, y - height / 2, width, height), Color.blue);
    }

    public bool TestCollision(Vector2 p, float radius)
    {
        if (!isValid)
        {
            return false;
        }
        var rx = Mathf.Clamp(x * _pongGame.width, width / 2 + 1, _pongGame.width - width / 2 - 1);
        var w = width + radius * 2;
        var h = height + radius * 2;
        var rect = new Rect(rx - (w / 2), y - (h / 2), w, h);
        return rect.Contains(p);
    }
}
