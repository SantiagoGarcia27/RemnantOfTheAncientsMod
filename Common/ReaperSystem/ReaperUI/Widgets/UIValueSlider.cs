using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.UI;

/// <summary>
/// A horizontal slider for configuring non-boolean soul values within a range.
/// Draws a track, filled portion, handle, and value label.
/// </summary>
public class UIValueSlider : UIElement
{
    private readonly float _min;
    private readonly float _max;
    private readonly float _increment;
    private float _value;
    private bool _dragging;

    public float Value => _value;
    public event System.Action<float> OnValueChanged;

    private const float LabelHeight = 20f;
    private const float TrackHeight = 6f;
    private const float HandleHeight = 16f;

    public UIValueSlider(float min, float max, float increment, float currentValue)
    {
        _min = min;
        _max = max;
        _increment = increment;
        _value = MathHelper.Clamp(currentValue, min, max);
    }

    public override void LeftMouseDown(UIMouseEvent evt)
    {
        base.LeftMouseDown(evt);
        _dragging = true;
        UpdateValueFromMouse(evt.MousePosition.X);
    }

    public override void LeftMouseUp(UIMouseEvent evt)
    {
        base.LeftMouseUp(evt);
        _dragging = false;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (_dragging)
        {
            if (!Main.mouseLeft)
            {
                _dragging = false;
                return;
            }
            UpdateValueFromMouse(Main.mouseX);
        }
    }

    private void UpdateValueFromMouse(float mouseX)
    {
        CalculatedStyle dims = GetDimensions();
        float trackLeft = dims.X + 8f;
        float trackWidth = dims.Width - 16f;
        if (trackWidth <= 0f)
            return;
        float ratio = MathHelper.Clamp((mouseX - trackLeft) / trackWidth, 0f, 1f);
        float rawValue = _min + ratio * (_max - _min);
        float snapped = (float)System.Math.Round(rawValue / _increment) * _increment;
        snapped = MathHelper.Clamp(snapped, _min, _max);
        if (snapped != _value)
        {
            _value = snapped;
            OnValueChanged?.Invoke(_value);
        }
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        CalculatedStyle dims = GetDimensions();
        float ratio = (_max > _min) ? (_value - _min) / (_max - _min) : 0f;

        // Value label
        string text = $"{(int)_value} / {(int)_max}";
        Utils.DrawBorderString(spriteBatch, text, new Vector2(dims.X + dims.Width / 2f, dims.Y), Color.White, 0.8f, 0.5f);

        // Track
        float trackTop = dims.Y + LabelHeight + (dims.Height - LabelHeight) / 2f - TrackHeight / 2f;
        float trackLeft = dims.X + 8f;
        float trackWidth = dims.Width - 16f;
        Rectangle trackRect = new((int)trackLeft, (int)trackTop, (int)trackWidth, (int)TrackHeight);
        spriteBatch.Draw(TextureAssets.MagicPixel.Value, trackRect, new Color(50, 50, 70, 220));

        // Filled portion
        int fillWidth = (int)(trackWidth * ratio);
        if (fillWidth > 0)
        {
            Rectangle fillRect = new((int)trackLeft, (int)trackTop, fillWidth, (int)TrackHeight);
            spriteBatch.Draw(TextureAssets.MagicPixel.Value, fillRect, new Color(80, 200, 80, 220));
        }

        // Handle
        float handleX = trackLeft + trackWidth * ratio;
        float handleTop = trackTop - (HandleHeight - TrackHeight) / 2f;
        Rectangle handleRect = new((int)(handleX - 5f), (int)handleTop, 10, (int)HandleHeight);
        Color handleColor = _dragging ? new Color(255, 255, 255, 255) : new Color(200, 200, 200, 240);
        spriteBatch.Draw(TextureAssets.MagicPixel.Value, handleRect, handleColor);
    }
}