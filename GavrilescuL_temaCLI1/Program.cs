﻿using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

class InteractiveWindow : GameWindow
{
    private float _x = 0.0f; // Coordonata X a obiectului
    private float _y = 0.0f; // Coordonata Y a obiectului

    public InteractiveWindow()
        : base(800, 600, GraphicsMode.Default, "OpenTK Interactive Example")
    {
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        GL.ClearColor(0.54f, 0.27f, 0.07f, 1.0f);  // Setăm culoarea de fundal la maro
    }

    protected override void OnUpdateFrame(FrameEventArgs e)
    {
        base.OnUpdateFrame(e);

        // Control prin tastatură: W pentru sus și S pentru jos
        if (Keyboard.GetState().IsKeyDown(Key.W))
            _y += 0.05f;  // Mutăm obiectul în sus
        if (Keyboard.GetState().IsKeyDown(Key.S))
            _y -= 0.05f;  // Mutăm obiectul în jos

        if (Keyboard.GetState().IsKeyDown(Key.A))
            _x -= 0.05f;  // Mutăm obiectul la stânga
        if (Keyboard.GetState().IsKeyDown(Key.D))
            _x += 0.05f;  // Mutăm obiectul la dreapta
    }

    protected override void OnMouseMove(MouseMoveEventArgs e)
    {
        base.OnMouseMove(e);

        // Control prin mouse: actualizare poziție bazată pe mișcarea mouse-ului
        _x = (e.X - Width / 2) / (Width / 2.0f); // Mișcăm pe axa X
        _y = -(e.Y - Height / 2) / (Height / 2.0f); // Mișcăm pe axa Y
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
        base.OnRenderFrame(e);

        GL.Clear(ClearBufferMask.ColorBufferBit);

        // Desenăm un cerc
        GL.Begin(PrimitiveType.TriangleFan);
        GL.Color3(0.0f, 0.0f, 1.0f); // Culoarea albastră
        GL.Vertex2(_x, _y); // Centru cerc

        int segments = 100; // Numărul de segmente pentru cerc
        for (int i = 0; i <= segments; i++)
        {
            double angle = i * 2.0 * Math.PI / segments; // Calculăm unghiul
            float x = (float)(Math.Cos(angle) * 0.05); // Raza cercului
            float y = (float)(Math.Sin(angle) * 0.05);
            GL.Vertex2(_x + x, _y + y); // Coordonatele vârfului cercului
        }
        GL.End();

        SwapBuffers();
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);

        GL.Viewport(0, 0, Width, Height);
        GL.MatrixMode(MatrixMode.Projection);
        GL.LoadIdentity();
        GL.Ortho(-1.0, 1.0, -1.0, 1.0, -1.0, 1.0);  // Proiecție ortografică
        GL.MatrixMode(MatrixMode.Modelview);
    }

    protected override void OnKeyDown(KeyboardKeyEventArgs e)
    {
        base.OnKeyDown(e);

        // Închide aplicația când se apasă tasta Escape
        if (e.Key == Key.Escape)
            this.Close();
    }

    static void Main(string[] args)
    {
        using (InteractiveWindow window = new InteractiveWindow())
        {
            window.Run(60.0);  // Rulează aplicația la 60 FPS
        }
    }
}
