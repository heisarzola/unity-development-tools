using UnityEngine;
using ProtoTurtle.BitmapDrawing;

public class Ball
{
    public Vector2 pos = new Vector2();
    public Vector2 vel = new Vector2();
    public float speed;
    PongGame _pongGame;
    Texture2D texture;
    int radius = 5;

    public Ball(PongGame pongGame)
    {
        this._pongGame = pongGame;
        this.texture = pongGame.texture;
    }

    public bool Update()
    {
        pos += vel * speed;
        if (speed < 1)
        {
            speed += 0.01f;
        }
        if ((pos.x < _pongGame.wallWidth + radius && vel.x < 0) ||
            (pos.x > _pongGame.width - _pongGame.wallWidth - radius && vel.x > 0))
        {
            vel.x *= -1;
        }
        if (_pongGame.TestRacketCollision(pos, vel, radius))
        {
            vel.y *= -1;
            _pongGame.AddScore();
        }
        var offset = 0;
        if (pos.y > _pongGame.height + radius)
        {
            offset = 1;
            this.pos.y = -radius;
        }
        else if (pos.y < -radius)
        {
            offset = -1;
            this.pos.y = _pongGame.height + radius;
        }
        if (offset != 0)
        {
            var np = _pongGame.GetAnother(offset);
            if (np != null)
            {
                np.AddBall(this.pos, this.vel);
            }
            else
            {
                _pongGame.Miss();
            }
            return false;
        }
        texture.DrawFilledCircle((int)pos.x, (int)pos.y, radius, Color.blue);
        return true;
    }
}
