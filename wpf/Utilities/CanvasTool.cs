using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace wpf.Utilities
{
    #region 【Class : CanvasTool】
    public class CanvasTool
    {
        #region ■ Properties

        #region - Surface : 描画対象Canvas
        /// <summary>
        /// 描画対象Canvas
        /// </summary>
        public Canvas Surface { get; private set; }
        #endregion

        #region - Width : 幅(px)
        /// <summary>
        /// 幅(px)
        /// </summary>
        public double Width
        {
            get { return Surface.Width; }
            set
            {
                if (Surface.Width != value)
                    Surface.Width = value;
            }
        }
        #endregion

        #region - Height : 高さ(px)
        /// <summary>
        /// 高さ
        /// </summary>
        public double Heigth
        {
            get { return Surface.Height; }
            set
            {
                if (Surface.Height != value)
                    Surface.Height = value;
            }
        }
        #endregion

        #region - Background : 背景ブラシ
        /// <summary>
        /// 背景ブラシ
        /// </summary>
        public Brush Background
        {
            get { return Surface.Background; }
            set
            {
                if (Surface.Background != value)
                    Surface.Background = value;
            }
        }
        #endregion

        #endregion

        #region ■ Constructor

        #region - Constructor(1)
        /// <summary>
        /// コンストラクタ(1)
        /// </summary>
        public CanvasTool()
        {
            Surface = new Canvas();
        }
        #endregion

        #region - Constructor(2)
        /// <summary>
        /// コンストラクタ(2)
        /// </summary>
        /// <param name="width">double : 幅</param>
        /// <param name="height">double : 高さ</param>
        /// <param name="background">Brush : 背景ブラシ</param>
        public CanvasTool(double width, double height, Brush background = null)
            : this()
        {
            Surface.Width = width;
            Surface.Height = height;
            if (background != null)
                Surface.Background = background;
        }
        #endregion

        #region - Constructor(3)
        /// <summary>
        /// コンストラクタ(3)
        /// </summary>
        /// <param name="canvas">キャンバス</param>
        public CanvasTool(Canvas canvas)
        {
            Surface = canvas;
        }
        #endregion

        #endregion

        #region ■ Methods

        #region - AddTextBlock(1) : TextBlockを追加する(1)
        /// <summary>
        /// TextBlockを追加する(1)
        /// </summary>
        /// <param name="text">文字列</param>
        /// <param name="fontFamily">フォント種別</param>
        /// <param name="fontSize">フォントサイズ</param>
        /// <param name="foreground">フォアグラウンドブラシ</param>
        /// <param name="width">幅</param>
        /// <param name="alignment">テキストアライアメント</param>
        /// <param name="x">X位置</param>
        /// <param name="y">Y位置</param>
        public void AddTextBlock(string text,
                                 FontFamily fontFamily,
                                 double fontSize,
                                 Brush foreground,
                                 double width,
                                 TextAlignment alignment,
                                 double x,
                                 double y)
        {
            var tb = new TextBlock();
            tb.Text = text;
            tb.FontFamily = fontFamily;
            tb.FontSize = fontSize;
            tb.Width = width;
            tb.TextAlignment = alignment;
            Canvas.SetLeft(tb, x);
            Canvas.SetTop(tb, y);
            Surface.Children.Add(tb);
        }
        #endregion

        #region - AddTextBlock(2) : TextBlockを追加する(2)
        /// <summary>
        /// TextBlockを追加する(2)
        /// </summary>
        /// <param name="text">文字列</param>
        /// <param name="x">X位置</param>
        /// <param name="y">Y位置</param>
        /// <param name="config">TextBlock設定用デリゲート</param>
        public void AddTextBlock(string text, double x, double y, Action<TextBlock> config)
        {
            var tb = new TextBlock();
            tb.Text = text;
            config?.Invoke(tb);
            Canvas.SetLeft(tb, x);
            Canvas.SetTop(tb, y);
            Surface.Children.Add(tb);
        }
        #endregion

        #region - AddRecangle(1) : Rectangle を追加する(1)
        /// <summary>
        /// Rectangle を追加する(1)
        /// </summary>
        /// <param name="width">幅</param>
        /// <param name="height">高さ</param>
        /// <param name="stroke">枠線ブラシ</param>
        /// <param name="strokeThickness">枠線幅</param>
        /// <param name="fill">塗りつぶしブラシ</param>
        /// <param name="x">X位置</param>
        /// <param name="y">Y位置</param>
        public void AddRectangle(double width,
                                 double height,
                                 Brush stroke,
                                 double strokeThickness,
                                 Brush fill,
                                 double x,
                                 double y)
        {
            var r = new Rectangle();
            r.Width = width;
            r.Height = height;
            r.Stroke = stroke;
            r.StrokeThickness = strokeThickness;
            r.Fill = fill;
            Canvas.SetLeft(r, x);
            Canvas.SetTop(r, y);
            Surface.Children.Add(r);
        }
        #endregion

        #region - AddRecangle(2) : Rectangle を追加する(2)
        /// <summary>
        /// Rectangle を追加する(2)
        /// </summary>
        /// <param name="width">幅</param>
        /// <param name="height">高さ</param>
        /// <param name="x">X位置</param>
        /// <param name="y">Y位置</param>
        /// <param name="config">Rectangle設定用デリゲート</param>
        public void AddRectangle(double width, double height, double x, double y, Action<Rectangle> config)
        {
            var r = new Rectangle();
            r.Width = width;
            r.Height = height;
            config?.Invoke(r);
            Canvas.SetLeft(r, x);
            Canvas.SetTop(r, y);
            Surface.Children.Add(r);
        }
        #endregion

        #region - AddLine(1) : 直線を追加する(1)
        /// <summary>
        /// 直線を追加する(1)
        /// </summary>
        /// <param name="x1">始点X位置</param>
        /// <param name="y1">始点Y位置</param>
        /// <param name="x2">終点X位置</param>
        /// <param name="y2">終点Y位置</param>
        /// <param name="stroke">線ブラシ</param>
        /// <param name="strokeThickness">線幅</param>
        public void AddLine(double x1, double y1, double x2, double y2, Brush stroke, double strokeThickness)
        {
            var line = new Line();
            line.X1 = 0.0d;
            line.Y1 = 0.0d;
            line.X2 = x2 - x1;
            line.Y2 = y2 - y1;
            line.Stroke = stroke;
            line.StrokeThickness = strokeThickness;
            Canvas.SetLeft(line, x1);
            Canvas.SetTop(line, y1);
            Surface.Children.Add(line);
        }
        #endregion

        #region - AddLine(2) : 直線を追加する(2)
        /// <summary>
        /// 直線を追加する(2)
        /// </summary>
        /// <param name="x1">始点X位置</param>
        /// <param name="y1">始点Y位置</param>
        /// <param name="x2">終点X位置</param>
        /// <param name="y2">終点Y位置</param>
        /// <param name="config">Line設定用デリゲート</param>
        public void AddLine(double x1, double y1, double x2, double y2, Action<Line> config)
        {
            var line = new Line();
            line.X1 = 0.0d;
            line.Y1 = 0.0d;
            line.X2 = x2 - x1;
            line.Y2 = y2 - y1;
            config?.Invoke(line);
            Canvas.SetLeft(line, x1);
            Canvas.SetTop(line, y1);
            Surface.Children.Add(line);
        }
        #endregion

        #region - AddCircle(1) : 円を追加する(1)
        /// <summary>
        /// 円を追加する
        /// </summary>
        /// <param name="r1">半径</param>
        /// <param name="stroke">枠線ブラシ</param>
        /// <param name="strokeThickness">枠線幅</param>
        /// <param name="fill">塗りつぶしブラシ</param>
        /// <param name="x1">中心点X位置</param>
        /// <param name="y1">中心点Y位置</param>
        public void AddCircle(double r, Brush stroke, double strokeThickness, Brush fill, double x1, double y1)
        {
            var e = new Ellipse();
            e.Width = r * 2.0d;
            e.Height = r * 2.0d;
            e.Stroke = stroke;
            e.StrokeThickness = strokeThickness;
            e.Fill = fill;
            Canvas.SetLeft(e, x1 - r);
            Canvas.SetTop(e, y1 - r);
            Surface.Children.Add(e);
        }
        #endregion

        #region - AddCircle(2) : 円を追加する(2)
        /// <summary>
        /// 円を追加する
        /// </summary>
        /// <param name="r1">半径</param>
        /// <param name="x1">中心点X位置</param>
        /// <param name="y1">中心点Y位置</param>
        /// <param name="config">Ellipse設定用デリゲート</param>
        public void AddCircle(double r, double x1, double y1, Action<Ellipse> config)
        {
            var e = new Ellipse();
            e.Width = r * 2.0d;
            e.Height = r * 2.0d;
            config?.Invoke(e);
            Canvas.SetLeft(e, x1 - r);
            Canvas.SetTop(e, y1 - r);
            Surface.Children.Add(e);
        }
        #endregion

        #region - AddEllipse(1) : 楕円を追加する(1)
        /// <summary>
        /// 楕円を追加する(1)
        /// 　（※矩形に内接する楕円）
        /// </summary>
        /// <param name="width">幅</param>
        /// <param name="height">高さ</param>
        /// <param name="stroke">枠線ブラシ</param>
        /// <param name="strokeThickness">枠線幅</param>
        /// <param name="fill">塗りつぶしブラシ</param>
        /// <param name="x">X位置</param>
        /// <param name="y">Y位置</param>
        public void AddEllipse(double width,
                               double height,
                               Brush stroke,
                               double strokeThickness,
                               Brush fill,
                               double x,
                               double y)
        {
            var e = new Ellipse();
            e.Width = width;
            e.Height = height;
            e.Stroke = stroke;
            e.StrokeThickness = strokeThickness;
            e.Fill = fill;
            Canvas.SetLeft(e, x);
            Canvas.SetTop(e, y);
            Surface.Children.Add(e);
        }
        #endregion

        #region - AddEllipse(2) : 楕円を追加する(2)
        /// <summary>
        /// 楕円を追加する(2)
        /// 　（※矩形に内接する楕円）
        /// </summary>
        /// <param name="width">幅</param>
        /// <param name="height">高さ</param>
        /// <param name="x">X位置</param>
        /// <param name="y">Y位置</param>
        /// <param name="config">Ellipse設定用デリゲート</param>
        public void AddEllipse(double width, double height, double x, double y, Action<Ellipse> config)
        {
            var e = new Ellipse();
            e.Width = width;
            e.Height = height;
            config?.Invoke(e);
            Canvas.SetLeft(e, x);
            Canvas.SetTop(e, y);
            Surface.Children.Add(e);
        }
        #endregion

        #region - AddPolygon(1) : Polygonを追加する(1)
        /// <summary>
        /// Polygonを追加する(1)
        /// </summary>
        /// <param name="width">幅</param>
        /// <param name="height">高さ</param>
        /// <param name="pc">頂点のコレクション</param>
        /// <param name="stroke">枠線ブラシ</param>
        /// <param name="strokeThickness">枠線幅</param>
        /// <param name="fill">塗りつぶしブラシ</param>
        /// <param name="x">X位置</param>
        /// <param name="y">Y位置</param>
        public void AddPolygon(double width,
                               double height,
                               PointCollection pc,
                               Brush stroke,
                               double strokeThickness,
                               Brush fill,
                               double x,
                               double y)
        {
            var poly = new Polygon();
            poly.Width = width;
            poly.Height = height;
            poly.Points = pc;
            poly.Stroke = stroke;
            poly.StrokeThickness = strokeThickness;
            poly.Fill = fill;
            Canvas.SetLeft(poly, x);
            Canvas.SetTop(poly, y);
            Surface.Children.Add(poly);
        }
        #endregion

        #region - AddPolygon(2) : Polygonを追加する(2)
        /// <summary>
        /// Polygonを追加する(2)
        /// </summary>
        /// <param name="width">幅</param>
        /// <param name="height">高さ</param>
        /// <param name="pc">頂点のコレクション</param>
        /// <param name="x">X位置</param>
        /// <param name="y">Y位置</param>
        /// <param name="config">Polygon設定用デリゲート</param>
        public void AddPolygon(double width,
                               double height,
                               PointCollection pc,
                               double x,
                               double y,
                               Action<Polygon> config)
        {
            var poly = new Polygon();
            poly.Width = width;
            poly.Height = height;
            poly.Points = pc;
            config?.Invoke(poly);
            Canvas.SetLeft(poly, x);
            Canvas.SetTop(poly, y);
            Surface.Children.Add(poly);
        }
        #endregion

        #endregion
    }
    #endregion
}
