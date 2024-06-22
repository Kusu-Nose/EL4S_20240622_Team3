using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// プレイヤーギズモ描画
/// </summary>
public static class ShapeGizmo
{
	/// <summary>
	/// 形状
	/// </summary>
	private abstract class Shape
	{
		/// <summary>
		/// 色
		/// </summary>
		public Color _color { get; protected set; }

		/// <summary>
		/// 描画
		/// </summary>
		public abstract void Draw();
	}

	/// <summary>
	/// 線
	/// </summary>
	private class Line : Shape
	{
		/// <summary>
		/// 始点
		/// </summary>
		public Vector3 _from { get; private set; }
		/// <summary>
		/// 終点
		/// </summary>
		public Vector3 _to { get; private set; }

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="from">始点</param>
		/// <param name="to">終点</param>
		/// <param name="color">色</param>
		public Line(Vector3 from, Vector3 to, Color color)
		{
			_from = from;
			_to = to;
			_color = color;
		}

		/// <summary>
		/// 描画
		/// </summary>
		public override void Draw()
		{
			Gizmos.color = _color;
			Gizmos.DrawLine(_from, _to);
		}
	}

	/// <summary>
	/// 光線
	/// </summary>
	private class Ray : Shape
	{
		/// <summary>
		/// 始点
		/// </summary>
		public Vector3 _from { get; private set; }
		/// <summary>
		/// 方向
		/// </summary>
		public Vector3 _direction { get; private set; }

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="from">始点</param>
		/// <param name="direction">方向</param>
		/// <param name="color">色</param>
		public Ray(Vector3 from, Vector3 direction, Color color)
		{
			_from = from;
			_direction = direction;
			_color = color;
		}

		/// <summary>
		/// 描画
		/// </summary>
		public override void Draw()
		{
			Gizmos.color = _color;
			Gizmos.DrawRay(_from, _direction);
		}
	}

	/// <summary>
	/// 直方体
	/// </summary>
	private class Cube : Shape
	{
		/// <summary>
		/// 中心
		/// </summary>
		public Vector3 _center { get; private set; }
		/// <summary>
		/// 大きさ
		/// </summary>
		public Vector3 _size { get; private set; }

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="center">中心</param>
		/// <param name="size">大きさ</param>
		/// <param name="color">色</param>
		public Cube(Vector3 center, Vector3 size, Color color)
		{
			_center = center;
			_size = size;
			_color = color;
		}

		/// <summary>
		/// 描画
		/// </summary>
		public override void Draw()
		{
			Gizmos.color = _color;
			Gizmos.DrawWireCube(_center, _size);
		}
	}

	/// <summary>
	/// 球
	/// </summary>
	private class Sphere : Shape
	{
		/// <summary>
		/// 中心
		/// </summary>
		public Vector3 _center { get; private set; }
		/// <summary>
		/// 半径
		/// </summary>
		public float _radius { get; private set; }

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="center">中心</param>
		/// <param name="radius">半径</param>
		/// <param name="color">色</param>
		public Sphere(Vector3 center, float radius, Color color)
		{
			_center = center;
			_radius = radius;
			_color = color;
		}

		/// <summary>
		/// 描画
		/// </summary>
		public override void Draw()
		{
			Gizmos.color = _color;
			Gizmos.DrawWireSphere(_center, _radius);
		}
	}

	/// <summary>
	/// 形状のリスト
	/// </summary>
	private static List<Shape> _shapeList = new List<Shape>();

	/// <summary>
	/// 線を追加
	/// </summary>
	/// <param name="from">始点</param>
	/// <param name="to">終点</param>
	/// <param name="color">色</param>
	[Conditional("UNITY_EDITOR")]
	public static void DrawLine(Vector3 from, Vector3 to, Color color)
	{
		Line line = new Line(from, to, color);
		_shapeList.Add(line);
	}

	/// <summary>
	/// 光線を追加
	/// </summary>
	/// <param name="from">始点</param>
	/// <param name="direction">方向</param>
	/// <param name="color">色</param>
	[Conditional("UNITY_EDITOR")]
	public static void DrawRay(Vector3 from, Vector3 direction, Color color)
	{
		Ray ray = new Ray(from, direction, color);
		_shapeList.Add(ray);
	}

	/// <summary>
	/// 線の直方体を追加
	/// </summary>
	/// <param name="center">中心</param>
	/// <param name="size">大きさ</param>
	/// <param name="color">色</param>
	[Conditional("UNITY_EDITOR")]
	public static void DrawWireCube(Vector3 center, Vector3 size, Color color)
	{
		Cube cube = new Cube(center, size, color);
		_shapeList.Add(cube);
	}

	/// <summary>
	/// 線の2D四角を追加
	/// </summary>
	/// <param name="leftTop">左上</param>
	/// <param name="rightBottom">右下</param>
	/// <param name="color">色</param>
	[Conditional("UNITY_EDITOR")]
	public static void DrawWire2DRect(Vector2 leftTop, Vector2 rightBottom, Color color)
	{
		Vector2 center = leftTop + (leftTop - rightBottom) / 2;
		Vector2 size = rightBottom - leftTop;
		DrawWireCube(center, size, color);
	}

	/// <summary>
	/// 線の球を追加
	/// </summary>
	/// <param name="center">中心</param>
	/// <param name="radius">半径</param>
	/// <param name="color">色</param>
	[Conditional("UNITY_EDITOR")]
	public static void DrawWireSphere(Vector3 center, float radius, Color color)
	{
		Sphere sphere = new Sphere(center, radius, color);
		_shapeList.Add(sphere);
	}

	/// <summary>
	/// 描画
	/// </summary>
	/// <param name="gameObject">ゲームオブジェクト</param>
	/// <param name="gizmoType">ギズモタイプ</param>
#if UNITY_EDITOR
	[DrawGizmo(GizmoType.Selected)]
	private static void Draw(GameObject gameObject, GizmoType gizmoType)
	{
		foreach (var shape in _shapeList)
		{
			shape.Draw();
		}
		_shapeList.Clear();
	}
#endif
}